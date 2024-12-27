using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.CadastrarCandidato
{
    public class CadastrarCandidatoCommand : IRequest<Result>
    {
        public string UrlPublica { get; set; }
        public string Nome { get; set; }
        public string DescricaoProfissional { get; set; }
        public string Sobre { get; set; }
        public List<CadastrarExperienciaCommand> Experiencias { get; set; }
        public List<CadastrarFormacaoCommand> Formacoes { get; set; }

        public class CadastrarExperienciaCommand
        {

            public string Empresa { get; set; }

            public string Local { get; set; }

            public List<CadastrarCargoCommand> Cargos { get; set; }
        }

        public class CadastrarCargoCommand
        {
            public string Titulo { get; set; }
            public DateTime DataInicial { get; set; }
            public DateTime DataFinal { get; set; }
            public string Descricao { get; set; }
        }

        public class CadastrarFormacaoCommand
        {
            public string Instituicao { get; set; }
            public string Curso { get; set; }
            public DateTime DataInicial { get; set; }
            public DateTime DataDeConclusao { get; set; }
        }
    }
}
