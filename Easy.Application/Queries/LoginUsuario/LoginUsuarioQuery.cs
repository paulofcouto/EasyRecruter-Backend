using Easy.Application.ViewModel;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Queries.LoginUsuario
{
    public class LoginUsuarioQuery : IRequest<Result<AuthResponse>>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
