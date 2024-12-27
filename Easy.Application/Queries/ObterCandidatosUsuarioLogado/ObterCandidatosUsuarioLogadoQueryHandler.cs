using Easy.Application.ViewModel;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Easy.Application.Queries.ObterCandidatosUsuarioLogado
{
    public class ObterCandidatosUsuarioLogadoQueryHandler : IRequestHandler<ObterCandidatosUsuarioLogadoQuery, Result<List<CandidatoViewModel>>>
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ObterCandidatosUsuarioLogadoQueryHandler(ICandidatoRepository candidatoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _candidatoRepository = candidatoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<List<CandidatoViewModel>>> Handle(ObterCandidatosUsuarioLogadoQuery request, CancellationToken cancellationToken)
        {
            var iUsuario = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(iUsuario))
            {
                return Result<List<CandidatoViewModel>>.Fail("Usuário não autenticado.");
            }

            var candidatos = await _candidatoRepository.ObterPorIdDoUsuarioAssincrono(iUsuario);

            if (candidatos == null || !candidatos.Any())
            {
                return Result<List<CandidatoViewModel>>.Fail("Nenhum candidato encontrado para o usuário logado.");
            }

            var candidatosViewModel = candidatos.Select(c => new CandidatoViewModel(
                c.Id,
                c.UrlPublica,
                c.DescricaoProfissional ?? string.Empty,
                c.Nome ?? string.Empty,
                c.Sobre ?? string.Empty,
                c.Foto != null && c.Foto.Length > 0
                ? $"data:image/jpeg;base64,{Convert.ToBase64String(c.Foto)}"
                : string.Empty
            )).ToList();

            return Result<List<CandidatoViewModel>>.Ok(candidatosViewModel);
        }
    }
}