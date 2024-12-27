using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Easy.Core.Entities
{
    public class Candidato : BaseEntity
    {
        public Candidato(string idUsuario, string urlPublica, string? nome, string? descricaoProfissional, byte[]? foto, string? sobre, List<Experiencia>? experiencias, List<Formacao>? formacoesAcademicas)
        {
            IdUsuario = idUsuario;
            UrlPublica = urlPublica;
            Nome = nome;
            DescricaoProfissional = descricaoProfissional;
            Foto = foto;
            Sobre = sobre;
            Experiencias = experiencias;
            Formacoes = formacoesAcademicas;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string IdUsuario { get; private set; }
        public string UrlPublica { get; private set; }
        public string? Nome { get; private set; }
        public string? DescricaoProfissional { get; private set; }
        public string? Sobre { get; private set; }
        public byte[]? Foto { get; private set; }
        public List<Experiencia>? Experiencias { get; private set; }
        public List<Formacao>? Formacoes { get; private set; }

        public void AtualizarInformacoes(string urlPublica, string nome, string descricaoProfissional, string v, List<Experiencia> listaExperiencias, List<Formacao> listaFormacoes)
        {
            throw new NotImplementedException();
        }

        public class Experiencia
        {
            public Experiencia(string? empresa,string? local, List<Cargo>? listaCargos)
            {
                Cargos = listaCargos;
                Empresa = empresa;
                Local = local;
            }
            
            public string? Empresa { get; private set; }
            public string? Local { get; private set; }
            public List<Cargo>? Cargos { get; private set; }
        }

        public class Cargo
        {
            public Cargo(string? titulo, DateTime? dataInicial, DateTime? dataFinal, string? descricao)
            {
                Titulo = titulo;
                DataInicial = dataInicial;
                DataFinal = dataFinal;
                Descricao = descricao;
            }

            public string? Titulo { get; private set; }
            public DateTime? DataInicial { get; private set; }
            public DateTime? DataFinal { get; private set; }
            public string? Descricao { get; private set; }
        }

        public class Formacao
        {
            public Formacao(string? instituicao, string? curso, DateTime? dataInicial, DateTime? dataDeConclusao)
            {
                Instituicao = instituicao;
                Curso = curso;
                DataInicial = dataInicial;
                DataDeConclusao = dataDeConclusao;
            }

            public string? Instituicao { get; private set; }
            public string? Curso { get; private set; }
            public DateTime? DataInicial { get; private set; }
            public DateTime? DataDeConclusao { get; private set; }
        }
    }
}
