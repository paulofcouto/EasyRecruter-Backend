using Easy.Core.Enums;

namespace Easy.Core.Entities
{
    public class Vaga : BaseEntity
    {
        public string Chave { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Localizacao { get; private set; }
        public decimal? Salario { get; private set; }
        public TipoContrato TipoContrato { get; private set; }
        public TipoJornada Jornada { get; private set; }
        public string EmpresaId { get; private set; }
        public DateTime? DataEncerramento { get; private set; }
        public bool Ativa { get; private set; }

        public Vaga(string titulo, string descricao, string localizacao, decimal? salario, TipoContrato tipoContrato, TipoJornada jornada, string empresaId, DateTime? dataEncerramento = null)
        {
            Titulo = titulo;
            Descricao = descricao;
            Localizacao = localizacao;
            Salario = salario;
            TipoContrato = tipoContrato;
            Jornada = jornada;
            EmpresaId = empresaId;
            DataEncerramento = dataEncerramento;
            Ativa = true;
            Chave = GerarChaveUnica();
        }

        public void Desativar()
        {
            Ativa = false;
            MarcarAtualizado();
        }
        private string GerarChaveUnica()
        {
            return $"{Guid.NewGuid().ToString("N")}_{DateTime.UtcNow:yyyyMMddHHmmssfff}";
        }
    }
}
