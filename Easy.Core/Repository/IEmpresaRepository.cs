using Easy.Core.Entities;

namespace Easy.Core.Repository
{
    public interface IEmpresaRepository
    {
        Task<bool> CnpjJaCadastradoAssincrono(string cnpj);
        Task CadastrarAssincrono(Empresa empresa);
        Task<Empresa> ObterPorUsuarioAssincrono(string id);
    }
}
