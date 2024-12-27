using Easy.Application.ViewModel;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Queries.ObterCandidatoPorId
{
    public class ObterCandidatoPorIdQuery : IRequest<Result<CandidatoDetalhadoViewModel>>
    {
        public ObterCandidatoPorIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
