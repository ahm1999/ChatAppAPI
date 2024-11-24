using MongoDB.Driver;

namespace ChatApp.Data
{
    public class IMongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;

        public IMongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            var mongoUrl = MongoUrl.Create(_configuration.GetConnectionString("mongodb"));
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase("chatApp");
        }


        public IMongoDatabase Database => _database!;




    }
}
