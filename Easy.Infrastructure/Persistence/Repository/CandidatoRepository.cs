using Easy.Core.Entities;
using Easy.Core.Repository;
using MongoDB.Driver;

namespace Easy.Infrastructure.Persistence.Repository
{
    public class CandidatoRepository : ICandidatoRepository
    {
        private readonly IMongoCollection<Candidato> _candidatos;

        public CandidatoRepository(EasyDbContext dbContext)
        {
            _candidatos = dbContext.GetCollection<Candidato>("Candidatos");
        }

        public async Task<Candidato> ObterPorIdAssincrono(string id)
        {
            return await _candidatos.Find(u => u.Id == id).SingleOrDefaultAsync();
        }

        public async Task CadastrarAssincrono(Candidato candidato)
        {
            await _candidatos.InsertOneAsync(candidato);
        }

        public async Task EditarAssincrono(Candidato candidato)
        {
            var filter = Builders<Candidato>.Filter.Eq(c => c.Id, candidato.Id);
            var update = Builders<Candidato>.Update
                .Set(c => c.UrlPublica, candidato.UrlPublica)
                .Set(c => c.IdUsuario, candidato.IdUsuario)
                .Set(c => c.Nome, candidato.Nome)
                .Set(c => c.DescricaoProfissional, candidato.DescricaoProfissional);

            await _candidatos.UpdateOneAsync(filter, update);
        }

        public async Task<List<Candidato>> ObterTodosAssincrono()
        {
            return await _candidatos.Find(_ => true).ToListAsync();
        }

        public async Task<List<Candidato>> ObterPorIdDoUsuarioAssincrono(string idUsuario)
        {
            return await _candidatos.Find(t => t.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task DeletarPorIdAssincrono(string id)
        {
            await _candidatos.DeleteOneAsync(u => u.Id == id);
        }
    }
}
