using Easy.Application.ViewModel;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Queries.ObterEmpresaDetalhadoPorUsuario
{
    public class ObterEmpresaDetalhadoPorUsuarioQuery : IRequest<Result<EmpresaDetalhadoViewModel>>
    {
    }
}
