using ChatApp.Data.Entities;
using ChatApp.DTOs;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ChatApp.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IUserService _userService;
        private readonly IMessegeService _messegeService;
        private readonly ILogger<ChatHub> _logger;
        private static readonly ConcurrentDictionary<string, string> connectedUsersContextId = new();
        private static readonly List<User> connectedUsers = new();    

        public ChatHub(IUserService userService, ILogger<ChatHub> logger, IMessegeService messegeService)
        {
            _userService = userService;
            _logger = logger;
            _messegeService = messegeService;
        }

        public override async Task OnConnectedAsync()
        {
           await Clients.All.SendAsync("RecieveMessege",$"{Context.ConnectionId} has connected");
        }



        //setting the user
        public async Task SetUser(string UserName) {
            
            User user = await _userService.AddOrReturnUserByName(UserName);
            connectedUsersContextId.TryAdd(user.Id!,Context.ConnectionId);

            connectedUsers.Add(user);
            _logger.LogInformation($"user added {user.Id}");

            await Clients.Others.SendAsync("UserConnections", user);
            await Clients.Caller.SendAsync("UserData", user);        
        }


        //removing a logged out user from the main ds
        public async Task RemoveUser(string UserId) {
            _logger.LogInformation($"user removed {UserId}");
            if (!string.IsNullOrEmpty(UserId))
            {
                connectedUsersContextId.TryRemove(UserId, out _);
                connectedUsers.RemoveAll(u => u.Id == UserId);
            }
            await Clients.Others.SendAsync("UserDisconnected", UserId);
            await Clients.Caller.SendAsync("RecieveMessege", "LoggedOut ");

        }



        //getting a list of connected users 
        public async Task GetConnectedUserIds()
        {
            _logger.LogInformation($"connected users activated");
            await Clients.Caller.SendAsync("ConnectedUsersIds", connectedUsers);
        }

        //Messeging -------------------------------------------------------------------

        public async Task SendMessageToUser(string userId, string MessegeContent, string AddressedUserId) {

            string userContextId = connectedUsersContextId.TryGetValue(AddressedUserId, out userContextId!) ? userContextId: string.Empty;

            if (userContextId == string.Empty) {

                return; 
            }

            var addMessegesDTO = new AddMessegesDTO()
            {
                SenderId = userId,
                RecieverId = AddressedUserId,
                MessageContent =MessegeContent

            };

            Messege messege = await _messegeService.AddMessage(addMessegesDTO);
            await Clients.Client(userContextId).SendAsync("MessegeFromUser",messege);
              
        
        }





        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userId = connectedUsersContextId.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (!string.IsNullOrEmpty(userId))
            {
                connectedUsersContextId.TryRemove(userId, out _);
                connectedUsers.RemoveAll(u => u.Id == userId);
               // await Clients.Others.SendAsync("UserDisconnected", userId);
            }
           
            await Clients.All.SendAsync("RecieveMessege", $"{Context.ConnectionId} has diconnected");
            await Clients.Others.SendAsync("UserDisconnected", userId);

        }

    }
}
