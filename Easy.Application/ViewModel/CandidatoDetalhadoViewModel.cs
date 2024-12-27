
namespace Easy.Application.ViewModel
{
    public class CandidatoDetalhadoViewModel
    {
        public CandidatoDetalhadoViewModel(string id, string urlPublica, string nome, string cargo)
        {
            Id = id;
            UrlPublica = urlPublica;

            if(nome != null)
            {
                Nome = nome;
            }

            if (nome != null)
            {
                Cargo = cargo;
            }
        }

        public string Id { get; private set; }
        public string UrlPublica { get; private set; }
        public string Nome { get; private set; }
        public string Cargo { get; private set; }
    }
}
