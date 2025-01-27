namespace Easy.Core.Entities
{
    public class Contato
    {
        public List<Email> Emails { get; private set; }
        public List<Telefone> Telefones { get; private set; }

        public Contato(List<Email> emails, List<Telefone> telefone)
        {
            Emails = emails;
            Telefones = telefone;
        }

    }

    public class Email
    {
        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            Endereco = endereco;
        }
    }

    public class Telefone
    {
        public string DDD { get; private set; }
        public string Numero { get; private set; }

        public Telefone(string ddd, string numero)
        {
            DDD = ddd;
            Numero = numero;
        }
    }
}