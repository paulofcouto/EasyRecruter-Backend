namespace Easy.Application.ViewModel
{
    public class EmpresaDetalhadoViewModel
    {
        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string Site { get; private set; }
        public EnderecoViewModel Endereco { get; private set; }
        public ContatoViewModel Contato { get; private set; }

        public EmpresaDetalhadoViewModel(string nomeFantasia, string razaoSocial, string cnpj, string site, EnderecoViewModel endereco, ContatoViewModel contato)
        {
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            Site = site;
            Endereco = endereco;
            Contato = contato;
        }
    }

    public class EnderecoViewModel
    {
        public EnderecoViewModel(string rua, string numero, string complemento, string bairro, string cidade, string estado, string cEP, string pais)
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cEP;
            Pais = pais;
        }

        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        public string Pais { get; private set; }
    }

    public class ContatoViewModel
    {
        public ContatoViewModel(List<EmailViewModel> emails, List<TelefoneViewModel> telefones)
        {
            Emails = emails;
            Telefones = telefones;
        }

        public List<EmailViewModel> Emails { get; private set; }
        public List<TelefoneViewModel> Telefones { get; private set; }
    }

    public class EmailViewModel
    {
        public EmailViewModel(string endereco)
        {
            Endereco = endereco;
        }

        public string Endereco { get; private set; }
    }

    public class TelefoneViewModel
    {
        public TelefoneViewModel(string dDD, string numero)
        {
            DDD = dDD;
            Numero = numero;
        }

        public string DDD { get; private set; }
        public string Numero { get; private set; }
    }
}