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

        public async Task InserirAssincrono(Empresa empresa)
        {
            await _empresas.InsertOneAsync(empresa);
        }

        public async Task AtualizarAssincrono(Empresa empresa)
        {
            var filtro = Builders<Empresa>.Filter.Eq(e => e.IdUsuario, empresa.IdUsuario);

            var updateDef = Builders<Empresa>.Update
                .Set(e => e.NomeFantasia, empresa.NomeFantasia)
                .Set(e => e.RazaoSocial, empresa.RazaoSocial)
                .Set(e => e.Cnpj, empresa.Cnpj)
                .Set(e => e.Site, empresa.Site)
                .Set(e => e.Endereco, empresa.Endereco)
                .Set(e => e.Contato, empresa.Contato)
                .Set(e => e.Ativa, empresa.Ativa);

            await _empresas.UpdateOneAsync(filtro, updateDef);
        }

        public async Task<bool> CnpjJaCadastradoAssincrono(string cnpj, string idUsuario)
        {
            var empresaExistente = await _empresas.Find(u => u.Cnpj == cnpj && u.IdUsuario != idUsuario).FirstOrDefaultAsync();

            return empresaExistente != null;
        }

        public async Task<Empresa> ObterPorUsuarioAssincrono(string id)
        {
            var empresa = await _empresas.FindAsync(u => u.IdUsuario == id);

            return empresa.FirstOrDefault();
        }
    }
}
