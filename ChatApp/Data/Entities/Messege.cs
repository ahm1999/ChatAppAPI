using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatApp.Data.Entities
{
    public class Messege
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.ObjectId)]
        public string RecieverId { get; set; } = string.Empty;
        [BsonElement("MessageContent")]

        public string MessageContent { get; set; } = string.Empty;
    }
}
