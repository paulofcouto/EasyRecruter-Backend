using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
