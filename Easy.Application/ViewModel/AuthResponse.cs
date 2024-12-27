namespace Easy.Application.ViewModel
{
    public class AuthResponse
    {
        public AuthResponse(string token, UsuarioViewModel usuario)
        {
            Token = token;
            Usuario = usuario;
        }

        public string Token { get; private set; }
        public UsuarioViewModel Usuario { get; private set; }
    }
}
