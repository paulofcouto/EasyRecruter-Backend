using Easy.Core.Entities;
using Easy.Core.Repository;
using MongoDB.Driver;

namespace Easy.Infrastructure.Persistence.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly IMongoCollection<Empresa> _empresas;

        public EmpresaRepository(EasyDbContext dbContext)
        {
            _empresas = dbContext.GetCollection<Empresa>("Empresas");
        }

        public async Task CadastrarAssincrono(Empresa empresa)
        {
            await _empresas.InsertOneAsync(empresa);
        }

        public async Task<bool> CnpjJaCadastradoAssincrono(string cnpj)
        {
            var empresaExistente = await _empresas.Find(u => u.Cnpj == cnpj).FirstOrDefaultAsync();

            return empresaExistente != null;
        }

        public async Task<Empresa> ObterPorUsuarioAssincrono(string id)
        {
            var empresa = await _empresas.FindAsync(u => u.IdUsuario == id);

            return empresa.FirstOrDefault();
        }
    }
}
