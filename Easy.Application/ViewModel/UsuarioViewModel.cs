namespace Easy.Application.ViewModel
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel(string id, string email)
        {
            Id = id;
            Email = email;
        }

        public string Id { get; private set; }
        public string Email { get; private set; }
    }
}
