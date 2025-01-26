using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Easy.Core.Entities
{
    public class Empresa : BaseEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string IdUsuario { get; private set; }
        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string Site { get; private set; }
        public Endereco Endereco { get; private set; }
        public Contato Contato { get; private set; }
        public bool Ativa { get; set; }

        public Empresa(string idUsuario, string nomeFantasia, string razaoSocial, string cnpj, string site, Endereco endereco, Contato contato) 
        { 
            IdUsuario = idUsuario;
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            Site = site;
            Endereco = endereco;
            Contato = contato;
            Ativa = true;
        }
    }
}
