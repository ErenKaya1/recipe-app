using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeApp.Entity.Entities.Base
{
    public class MongoEntity : IBaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public string Id { get; set; }
    }
}