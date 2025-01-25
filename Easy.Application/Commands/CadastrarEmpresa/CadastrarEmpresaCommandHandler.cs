using Easy.Core.Entities;
using Easy.Core.Enum;
using Easy.Core.Utils;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.CadastrarEmpresa
{
    public class CadastrarEmpresaCommandHandler : IRequestHandler<CadastrarEmpresaCommand, Result>
    {
        private readonly IEmpresaRepository _empresaRepository;

        public CadastrarEmpresaCommandHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<Result> Handle(CadastrarEmpresaCommand request, CancellationToken cancellationToken)
        {
            if (await _empresaRepository.CnpjJaCadastradoAsync(request.Cnpj))
            {
                return Result.Fail("CNPJ já cadastrado.");
            }

            var estadoEnum = (request.Enredeco?.Estado ?? string.Empty).ParseEnumByDescription<Estado>();

            var endereco = new Endereco(
                request.Enredeco?.Rua ?? string.Empty,
                request.Enredeco?.Numero ?? string.Empty,
                request.Enredeco?.Bairro ?? string.Empty,
                request.Enredeco?.Cidade ?? string.Empty,
                estadoEnum,
                request.Enredeco?.CEP ?? string.Empty,
                request.Enredeco?.Pais ?? string.Empty,
                request.Enredeco?.Complemento ?? string.Empty
            );

            var contato = new Contato(
                request.Contato?.Emails?.Select(e => new Email(e.Endereco ?? string.Empty)).ToList(),
                request.Contato?.Telefones?.Select(t => new Telefone(t.DDD ?? string.Empty, t.Numero ?? string.Empty)).ToList()
            );

            var empresa = new Empresa(
                request.NomeFantasia,
                request.RazaoSocial ?? string.Empty,
                request.Cnpj,
                request.Site ?? string.Empty,
                endereco,
                contato
            );

            await _empresaRepository.CadastrarAssincrono(empresa);

            return Result.Ok();
        }
    }
}
