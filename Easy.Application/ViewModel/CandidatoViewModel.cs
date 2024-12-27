namespace Easy.Application.ViewModel
{
    public class CandidatoViewModel
    {
        public CandidatoViewModel(string id, string urlPublica, string descricaoProfissional, string nome, string resumo, string foto)
        {
            Id = id;
            UrlPublica = urlPublica;
            Nome = nome;
            DescricaoProfissional = descricaoProfissional;
            Resumo = resumo;
            Foto = foto;
        }

        public string Id { get; private set; }
        public string UrlPublica { get; private set; }
        public string DescricaoProfissional { get; private set; }
        public string Nome { get; private set; }
        public string Resumo { get; private set; }
        public string Foto { get; private set; }
    }
}