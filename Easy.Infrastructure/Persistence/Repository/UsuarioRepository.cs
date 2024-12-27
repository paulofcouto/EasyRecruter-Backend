using Easy.Core.Entities;
using Easy.Core.Repository;
using MongoDB.Driver;

namespace Easy.Infrastructure.Persistence.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioRepository(EasyDbContext dbContext)
        {
            _usuarios = dbContext.GetCollection<Usuario>("Usuarios");
        }

        public async Task CadastrarAssincrono(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task<Usuario> ObterPorEmailAssincrono(string email)
        {
            return await _usuarios.Find(u => u.Email == email).SingleOrDefaultAsync();
        }

        public async Task<bool> EmailJaCadastradoAssincrono(string email)
        {
            return await _usuarios.Find(u => u.Email == email).AnyAsync();
        }
    }
}
