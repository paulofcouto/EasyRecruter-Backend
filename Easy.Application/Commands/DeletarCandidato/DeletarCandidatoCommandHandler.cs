using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.DeletarCandidato
{
    public class DeletarCandidatoCommandHandler : IRequestHandler<DeletarCandidatoCommand, Result>
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public DeletarCandidatoCommandHandler(ICandidatoRepository candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
        }

        public async Task<Result> Handle(DeletarCandidatoCommand request, CancellationToken cancellationToken)
        {
            await _candidatoRepository.DeletarPorIdAssincrono(request.Id);

            return Result.Ok();
        }
    }
}
