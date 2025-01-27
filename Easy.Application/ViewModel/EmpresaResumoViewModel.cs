namespace Easy.Application.ViewModel
{
    public class EmpresaResumoViewModel
    {
        public string NomeFantasia { get; private set; }
        public string CNPJ { get; private set; }
        public bool Ativa { get; private set; }
        public string Site { get; private set; }
        public string Localização { get; private set; }

        public EmpresaResumoViewModel(string nomeFantasia, string cnpj, bool ativa, string site, string localizacao)
        {
            NomeFantasia = nomeFantasia;
            CNPJ = cnpj;
            Ativa = ativa;
            Site = site;
            Localização = localizacao;
        }
    }
}
