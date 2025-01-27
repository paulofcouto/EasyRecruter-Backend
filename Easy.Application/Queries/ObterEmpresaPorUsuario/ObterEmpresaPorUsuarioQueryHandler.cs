using Easy.Application.Interfaces;
using Easy.Application.ViewModel;
using Easy.Core.Entities;
using Easy.Core.Repository;
using Easy.Core.Result;
using Easy.Core.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Easy.Application.Queries.ObterEmpresaPorUsuario
{
    public class ObterEmpresaPorUsuarioQueryHandler : IRequestHandler<ObterEmpresaPorUsuarioQuery, Result<EmpresaResumoViewModel>>
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;


        public ObterEmpresaPorUsuarioQueryHandler(IEmpresaRepository empresaRepository, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _empresaRepository = empresaRepository;
            _httpContextAccessor = httpContextAccessor;      
            _jwtService = jwtService;
        }

        public async Task<Result<EmpresaResumoViewModel>> Handle(ObterEmpresaPorUsuarioQuery request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Result<EmpresaResumoViewModel>.Fail("Token não identificado.");
            }

            var idUsuario = _jwtService.ExtrairIdUsuario(authorizationHeader);

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result<EmpresaResumoViewModel>.Fail("Usuário não identificado no token.");
            }

            var empresa = await _empresaRepository.ObterPorUsuarioAssincrono(idUsuario);

            if(empresa == null)
            {
                return Result<EmpresaResumoViewModel>.Empty();
            }

            var localizacao = ObterLocalizacao(empresa.Endereco);

            var empresaResumoViewModel = new EmpresaResumoViewModel(empresa.NomeFantasia, empresa.Cnpj, empresa.Ativa, empresa.Site, localizacao);

            return Result<EmpresaResumoViewModel>.Ok(empresaResumoViewModel);
        }

        private string ObterLocalizacao(Endereco endereco)
        {
            if (!string.IsNullOrEmpty(endereco.Cidade) && endereco.Estado != Core.Enums.Estado.None)
            {
                return $"{endereco.Cidade}/{endereco.Estado.GetEnumDescription()}";
            }
                
            if (!string.IsNullOrEmpty(endereco.Cidade))
            {
                return endereco.Cidade;
            }
                
            if (endereco.Estado != Core.Enums.Estado.None)
            {
                return endereco.Estado.GetEnumDescription();
            }

            return string.Empty;
        }
    }
}