using MongoDB.Driver;
using ChatApp.Data.Entities;
using ChatApp.Data;

namespace ChatApp.Services
{
    public class UserService:IUserService
    {
        private readonly IMongoDbService _mongoDbService;
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IMongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
            _userCollection = _mongoDbService.Database.GetCollection<User>("User");
        }

        public async Task<User> AddOrReturnUserByName(string name) {
            User user =  await _userCollection.Find(user => user.name == name).FirstOrDefaultAsync();
            if (user is null) {
                User newUser = new User()
                {
                    Id = "",
                    name = name
                };
                await _userCollection.InsertOneAsync(newUser);
                return newUser;
            }
            return user;
            
        
        }

        public async Task AddUser(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
        }
    }
}
