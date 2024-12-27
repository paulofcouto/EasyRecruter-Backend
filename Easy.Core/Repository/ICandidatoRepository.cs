using Easy.Core.Entities;

namespace Easy.Core.Repository
{
    public interface ICandidatoRepository
    {
        Task<Candidato> ObterPorIdAssincrono(string id);
        Task CadastrarAssincrono(Candidato candidato);
        Task EditarAssincrono(Candidato candidato);
        Task DeletarPorIdAssincrono(string id);
        Task<List<Candidato>> ObterTodosAssincrono();
        Task<List<Candidato>> ObterPorIdDoUsuarioAssincrono(string email);
    }
}
