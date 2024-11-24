
using ChatApp.Data;
using ChatApp.Data.Entities;
using ChatApp.DTOs;
using MongoDB.Driver;

namespace ChatApp.Services
{
    public class MessegeService : IMessegeService
    {
        private readonly IMongoDbService _mongoDbService;
        private readonly IMongoCollection<Messege> _messagesCollection;

        public MessegeService(IMongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
            _messagesCollection = _mongoDbService.Database.GetCollection<Messege>("Messages");
        }
        public async Task<Messege> AddMessage(AddMessegesDTO userData)
        {
            Messege messege = new Messege() {
                SenderId = userData.SenderId,
                RecieverId = userData.RecieverId,
                MessageContent = userData.MessageContent   

            };

            await _messagesCollection.InsertOneAsync(messege);
            return messege;
        }

        public async Task<List<Messege>> GetMessagesBetween(string user1Id, string user2Id)
        {

            //var filter = Builders<Messege>.Filter.Or(
            //         Builders<Messege>.Filter.And(
            //         Builders<Messege>.Filter.Eq(m => m.SenderId, user1Id),
            //         Builders<Messege>.Filter.Eq(m => m.RecieverId, user2Id)
            //          ),
            //         Builders<Messege>.Filter.And(
            //         Builders<Messege>.Filter.Eq(m => m.SenderId, user2Id),
            //         Builders<Messege>.Filter.Eq(m => m.RecieverId, user1Id)
            //        )
            //    );


            return await _messagesCollection.Find(m => (m.SenderId == user1Id && m.RecieverId == user2Id)|| (m.SenderId == user2Id && m.RecieverId == user1Id)).ToListAsync();

        }
    }
}
