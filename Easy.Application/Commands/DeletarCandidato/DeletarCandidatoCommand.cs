using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.DeletarCandidato
{
    public class DeletarCandidatoCommand : IRequest<Result>
    {
        public DeletarCandidatoCommand(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
