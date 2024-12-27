using Easy.Core.Entities;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, Result>
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Result> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {

            if(await _usuarioRepository.EmailJaCadastradoAssincrono(request.Email))
            {
                return Result.Fail("Esse e-mail já está cadastrado.");
            }
            
            var usuario = new Usuario(request.Email, request.Senha);
                        
            try
            {
                await _usuarioRepository.CadastrarAssincrono(usuario);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Ocorreu um erro inesperado ao cadastrar o usuário: {ex.Message}");
            }
        }
    }
}
