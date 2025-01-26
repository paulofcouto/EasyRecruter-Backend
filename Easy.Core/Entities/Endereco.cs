using Easy.Core.Enums;

namespace Easy.Core.Entities
{
    public class Endereco
    {
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public Estado Estado { get; private set; }
        public string CEP { get; private set; }
        public string Pais { get; private set; }

        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }

        public Endereco(string rua, string numero, string bairro, string cidade, Estado estado, string cep, string pais = "Brasil", string complemento = "")
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Pais = pais;
        }

        public void AtualizarGeolocalizacao(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
