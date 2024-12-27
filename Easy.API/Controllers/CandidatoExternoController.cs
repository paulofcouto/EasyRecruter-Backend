using Easy.Application.Commands.SalvarDadosCandidato;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Easy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CandidatoExternoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatoExternoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/CandidatoExterno/SalvarDados
        [HttpPost("SalvarDados")]
        public async Task<IActionResult> SalvarDados([FromBody] SalvarDadosCandidatoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Candidato cadastrado com sucesso.");
            }
            else
            {
                if (result.Error != null && result.Error.Any())
                {
                    return BadRequest(result.Error);
                }

                return StatusCode(500, "Ocorreu um erro inesperado ao cadastrar o candidato.");
            }
        }
    }
}
