using Easy.Application.Interfaces;
using Easy.Core.Entities;
using Easy.Core.Enums;
using Easy.Core.Repository;
using Easy.Core.Result;
using Easy.Core.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Easy.Application.Commands.CadastrarEmpresa
{
    public class CadastrarEmpresaCommandHandler : IRequestHandler<CadastrarEmpresaCommand, Result>
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;

        public CadastrarEmpresaCommandHandler(IEmpresaRepository empresaRepository, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _empresaRepository = empresaRepository;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
        }

        public async Task<Result> Handle(CadastrarEmpresaCommand request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Result.Fail("Token não identificado.");
            }

            var idUsuario = _jwtService.ExtrairIdUsuario(authorizationHeader);

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result.Fail("Usuário não identificado no token.");
            }

            if (await _empresaRepository.CnpjJaCadastradoAsync(request.Cnpj))
            {
                return Result.Fail("CNPJ já cadastrado.");
            }

            var estadoEnum = (request.Endereco?.Estado ?? string.Empty).ParseEnumByDescription<Estado>();

            var endereco = new Endereco(
                request.Endereco?.Rua ?? string.Empty,
                request.Endereco?.Numero ?? string.Empty,
                request.Endereco?.Bairro ?? string.Empty,
                request.Endereco?.Cidade ?? string.Empty,
                estadoEnum,
                request.Endereco?.CEP ?? string.Empty,
                request.Endereco?.Pais ?? string.Empty,
                request.Endereco?.Complemento ?? string.Empty
            );

            var contato = new Contato(
                request.Contato?.Emails?.Select(e => new Email(e.Endereco ?? string.Empty)).ToList(),
                request.Contato?.Telefones?.Select(t => new Telefone(t.DDD ?? string.Empty, t.Numero ?? string.Empty)).ToList()
            );

            var empresa = new Empresa(
                idUsuario,
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
