using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MongoDB.DataLayer.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("author")]
        public string Author { get; set; }
        [BsonElement("read")]
        public bool IsRead { get; set; }
        [BsonElement("recommend")]
        public bool IsRecommended { get; set; }
        public string Description { get; set; }

    }
}
