using Easy.Core.Entities;

namespace Easy.Core.Repository
{
    public interface IEmpresaRepository
    {
        Task<bool> CnpjJaCadastradoAssincrono(string cnpj, string idUsuario);
        Task InserirAssincrono(Empresa empresa);
        Task AtualizarAssincrono(Empresa empresa);
        Task<Empresa> ObterPorUsuarioAssincrono(string id);
    }
}
