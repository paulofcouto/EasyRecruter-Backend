using Easy.Core.Entities;

namespace Easy.Core.Repository
{
    public interface IEmpresaRepository
    {
        Task<bool> CnpjJaCadastradoAsync(string cnpj);
        Task CadastrarAssincrono(Empresa empresa);
    }
}
