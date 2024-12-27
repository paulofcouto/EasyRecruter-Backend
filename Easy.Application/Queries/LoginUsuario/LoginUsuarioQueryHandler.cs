using Easy.Application.Interfaces;
using Easy.Application.ViewModel;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Queries.LoginUsuario
{
    public class LoginUsuarioQueryHandler : IRequestHandler<LoginUsuarioQuery, Result<AuthResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenGeneratorService _jwtTokenGenerator;

        public LoginUsuarioQueryHandler(IUsuarioRepository usuarioRepository, IJwtTokenGeneratorService jwtTokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<AuthResponse>> Handle(LoginUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAssincrono(request.Email);

            //ToDoMVP = Criptografar as senhas
            if (usuario == null || request.Senha != usuario.SenhaHash)
            {
                return Result<AuthResponse>.Fail("Credenciais inválidas");
            }

            var token = _jwtTokenGenerator.GenerateToken(usuario);
            var usuarioViewModel = new UsuarioViewModel(usuario.Id, usuario.Email);
            var authResponse = new AuthResponse(token, usuarioViewModel);

            return Result<AuthResponse>.Ok(authResponse);
        }
    }
}
