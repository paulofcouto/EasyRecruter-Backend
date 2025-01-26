using Easy.Core.Entities;
using Easy.Core.Enums;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.CadastrarEmpresa
{
    public class CadastrarEmpresaCommand : IRequest<Result>
    {
        public required string NomeFantasia { get; set; }
        public string? RazaoSocial { get; set; }
        public required string Cnpj { get; set; }
        public string? Site { get; set; }
        public EnderecoCommand? Endereco { get; set; }
        public ContatoCommand? Contato { get; set; }
    }

    public class EnderecoCommand
    {
        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }
        public string? Pais { get; set; }
    }

    public class ContatoCommand
    {
        public List<EmailCommand>? Emails { get; set; }
        public List<TelefoneCommand>? Telefones { get; set; }
    }

    public class EmailCommand
    {
        public string? Endereco { get; set; }
    }

    public class TelefoneCommand
    {
        public string? DDD { get; set; }
        public string? Numero { get; set; }

    }
}
