namespace Easy.Core.Entities
{
    public class Empresa : BaseEntity
    {
        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string Site { get; private set; }
        public Endereco Enredeco { get; private set; }
        public Contato Contato { get; private set; }
        public bool Ativa { get; set; } 

        public Empresa(string nomeFantasia, string razaoSocial, string cnpj, string site, Endereco endereco, Contato contato) 
        { 
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            Site = site;
            Enredeco = endereco;
            Contato = contato;
            Ativa = true;
        }
    }
}
