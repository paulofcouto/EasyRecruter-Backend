using Easy.Application.ViewModel;
using Easy.Core.Result;
using MediatR;


namespace Easy.Application.Queries.ObterEstados
{
    namespace Easy.Application.Queries
    {
        public class ObterEnumEstadoQuery : IRequest<Result<List<EstadoViewModel>>>
        {
        }
    }
}