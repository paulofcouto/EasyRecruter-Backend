using Easy.Application.ViewModel;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Queries.ObterEmpresaPorUsuario
{
    public class ObterEmpresaPorUsuarioQuery : IRequest<Result<EmpresaResumoViewModel>>
    {

    }
}
