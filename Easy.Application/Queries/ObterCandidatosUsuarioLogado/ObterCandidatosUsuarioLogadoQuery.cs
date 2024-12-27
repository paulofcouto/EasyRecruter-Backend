using Easy.Application.ViewModel;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Queries.ObterCandidatosUsuarioLogado
{
    public class ObterCandidatosUsuarioLogadoQuery : IRequest<Result<List<CandidatoViewModel>>>
    {
    }
}
