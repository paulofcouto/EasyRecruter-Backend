using Easy.Application.ViewModel;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Easy.Application.Queries.ObterCandidatoPorId
{
    public class ObterCandidatoPorIdQueryHandler : IRequestHandler<ObterCandidatoPorIdQuery, Result<CandidatoDetalhadoViewModel>>
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public ObterCandidatoPorIdQueryHandler(ICandidatoRepository candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
        }

        public async Task<Result<CandidatoDetalhadoViewModel>> Handle(ObterCandidatoPorIdQuery request, CancellationToken cancellationToken)
        {
            var candidato = await _candidatoRepository.ObterPorIdAssincrono(request.Id);

            if (candidato == null)
            {
                return Result<CandidatoDetalhadoViewModel>.Fail("Usuário não encontrado");
            }

            var candidatoViewModel= new CandidatoDetalhadoViewModel(candidato.Id, candidato.UrlPublica, candidato.Nome, candidato.DescricaoProfissional);

            return Result<CandidatoDetalhadoViewModel>.Ok(candidatoViewModel);
        }
    }
}
