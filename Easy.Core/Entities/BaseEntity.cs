using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Easy.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
    }
}
