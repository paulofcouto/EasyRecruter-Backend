using static Easy.Core.Entities.Candidato;

namespace Easy.Application.ViewModel
{
    public class CandidatoViewModel
    {
        public CandidatoViewModel(string id, string urlPublica, string descricaoProfissional, string nome, string resumo, string foto, List<Experiencia>? experiencias, List<Formacao>? formacoes)
        {
            Id = id;
            UrlPublica = urlPublica;
            Nome = nome;
            DescricaoProfissional = descricaoProfissional;
            Resumo = resumo;
            Foto = foto;
            Formacoes = formacoes?.Select(f => new FormacaoViewModel
            {
                Instituicao = f.Instituicao,
                Curso = f.Curso,
                Periodo = $"{f.DataInicial?.ToString("MM/yyyy") ?? "Início não informado"} - {f.DataDeConclusao?.ToString("MM/yyyy") ?? "Conclusão não informada"}"
            }).ToList();

            Experiencias = experiencias?.Select(e => new ExperienciaViewModel
            {
                Empresa = e.Empresa,
                Local = e.Local,
                Cargos = e.Cargos?.Select(c => new CargoViewModel
                {
                    Titulo = c.Titulo,
                    Periodo = DeterminarPeriodo(c.DataInicial, c.DataFinal),
                    Descricao = c.Descricao
                }).ToList()
            }).ToList();
        }

        private string DeterminarPeriodo(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataFinal.HasValue)
            {
                return $"{dataInicial?.ToString("MM/yyyy") ?? "Início não informado"} - Atualmente";
            }
            else
            {
                return $"{dataInicial?.ToString("MM/yyyy") ?? "Início não informado"} - {dataFinal?.ToString("MM/yyyy")}";
            }
        }

        public string Id { get; private set; }
        public string UrlPublica { get; private set; }
        public string DescricaoProfissional { get; private set; }
        public string Nome { get; private set; }
        public string Resumo { get; private set; }
        public string Foto { get; private set; }
        public List<ExperienciaViewModel>? Experiencias { get; set; }
        public List<FormacaoViewModel>? Formacoes { get; set; }

        public class ExperienciaViewModel
        {

            public string? Empresa { get; set; }

            public string? Local { get; set; }

            public List<CargoViewModel>? Cargos { get; set; }
        }

        public class CargoViewModel
        {
            public string? Titulo { get; set; }
            public string? Periodo { get; set; }
            public string? Descricao { get; set; }
        }

        public class FormacaoViewModel
        {
            public string? Instituicao { get; set; }
            public string? Curso { get; set; }
            public string? Periodo { get; set; }
        }

    }
}