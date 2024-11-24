using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatApp.DTOs
{
    public class AddMessegesDTO
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; } = string.Empty;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]

        public string RecieverId { get; set; } = string.Empty;
        [BsonRequired]
        public string MessageContent { get; set; } = string.Empty;

    }
}
