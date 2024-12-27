using Easy.Core.Entities;

namespace Easy.Core.Repository
{
    public interface IUsuarioRepository
    {
        Task CadastrarAssincrono(Usuario usuario);
        Task<Usuario> ObterPorEmailAssincrono(string email);
        Task<bool> EmailJaCadastradoAssincrono(string email);
    }
}
