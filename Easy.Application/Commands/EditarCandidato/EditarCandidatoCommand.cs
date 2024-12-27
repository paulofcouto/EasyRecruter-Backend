using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.EditarCandidato
{
    public class EditarCandidatoCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public string UrlPublica { get; set; }
        public string Nome { get; set; }
        public string DescricaoProfissional { get; set; }
        public string Sobre { get; set; }
        public List<EditarExperienciaCommand> Experiencias { get; set; }
        public List<EditarFormacaoCommand> Formacoes { get; set; }

        public class EditarExperienciaCommand
        {
            public string Empresa { get; set; }
            public string Local { get; set; }
            public List<EditarCargoCommand> Cargos { get; set; }
        }

        public class EditarCargoCommand
        {
            public string Titulo { get; set; }
            public DateTime DataInicial { get; set; }
            public DateTime DataFinal { get; set; }
            public string Descricao { get; set; }
        }

        public class EditarFormacaoCommand
        {
            public string Instituicao { get; set; }
            public string Curso { get; set; }
            public DateTime DataInicial { get; set; }
            public DateTime DataDeConclusao { get; set; }
        }
    }
}
