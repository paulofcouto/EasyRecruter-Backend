using Easy.Application.Interfaces;
using Easy.Application.ViewModel;
using Easy.Core.Repository;
using Easy.Core.Result;
using Easy.Core.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Easy.Application.Queries.ObterEmpresaDetalhadoPorUsuario
{
    public class ObterEmpresaDetalhadoPorUsuarioQueryHandler : IRequestHandler<ObterEmpresaDetalhadoPorUsuarioQuery, Result<EmpresaDetalhadoViewModel>>
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;

        public ObterEmpresaDetalhadoPorUsuarioQueryHandler(IEmpresaRepository empresaRepository, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _empresaRepository = empresaRepository;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
        }

        public async Task<Result<EmpresaDetalhadoViewModel>> Handle(ObterEmpresaDetalhadoPorUsuarioQuery request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Result<EmpresaDetalhadoViewModel>.Fail("Token não identificado.");
            }

            var idUsuario = _jwtService.ExtrairIdUsuario(authorizationHeader);

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result<EmpresaDetalhadoViewModel>.Fail("Usuário não identificado no token.");
            }

            var empresa = await _empresaRepository.ObterPorUsuarioAssincrono(idUsuario);

            if (empresa == null)
            {
                return Result<EmpresaDetalhadoViewModel>.Empty();
            }

            var enderecoViewModel = new EnderecoViewModel(
                empresa.Endereco.Rua,
                empresa.Endereco.Numero,
                empresa.Endereco.Complemento,
                empresa.Endereco.Bairro,
                empresa.Endereco.Cidade,
                empresa.Endereco.Estado.GetEnumDescription(),
                empresa.Endereco.CEP,
                empresa.Endereco.Pais
            );

            var contatoViewModel = new ContatoViewModel(
                empresa.Contato.Emails.Select(email => new EmailViewModel(email.Endereco)).ToList(),
                empresa.Contato.Telefones.Select(telefone => new TelefoneViewModel(telefone.DDD, telefone.Numero)).ToList()
            );

            var empresaDetalhadoViewModel = new EmpresaDetalhadoViewModel(
                empresa.NomeFantasia,
                empresa.RazaoSocial,
                empresa.Cnpj,
                empresa.Site,
                enderecoViewModel,
                contatoViewModel
            );

            return Result<EmpresaDetalhadoViewModel>.Ok(empresaDetalhadoViewModel);
        }
    }
}
