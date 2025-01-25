using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Easy.Core.Entities
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public DateTime Criacao { get; private set; }
        public DateTime Atualizacao { get; private set; }

        protected BaseEntity()
        {
            Id = ObjectId.GenerateNewId().ToString();
            Criacao = DateTime.Now;
            Atualizacao = Criacao;
        }

        public void MarcarAtualizado()
        {
            Atualizacao = DateTime.UtcNow;
        }
    }
}
