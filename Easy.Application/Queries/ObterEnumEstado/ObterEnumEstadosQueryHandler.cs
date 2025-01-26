using Easy.Application.Queries.ObterEstados.Easy.Application.Queries;
using Easy.Application.ViewModel;
using Easy.Core.Enums;
using Easy.Core.Result;
using Easy.Core.Utils;
using MediatR;

namespace Easy.Application.Queries.ObterEstados
{
    public class ObterEnumEstadoQueryHandler : IRequestHandler<ObterEnumEstadoQuery, Result<List<EstadoViewModel>>>
    {
        public Task<Result<List<EstadoViewModel>>> Handle(ObterEnumEstadoQuery request, CancellationToken cancellationToken)
        {
            var estados = Enum.GetValues(typeof(Estado))
                .Cast<Estado>()
                .Where(e => e != Estado.None)
                .Select(e => new EstadoViewModel
                {
                    Id = (int)e,
                    Descricao = e.GetEnumDescription()
                })
                .ToList();

            return Task.FromResult(Result<List<EstadoViewModel>>.Ok(estados));
        }
    }
}