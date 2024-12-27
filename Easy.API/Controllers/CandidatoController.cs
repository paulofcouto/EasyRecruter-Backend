using Easy.Application.Commands.CadastrarCandidato;
using Easy.Application.Commands.DeletarCandidato;
using Easy.Application.Commands.EditarCandidato;
using Easy.Application.Queries.ObterCandidatoPorId;
using Easy.Application.Queries.ObterCandidatosUsuarioLogado;
using Easy.Application.ViewModel;
using Easy.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Easy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CandidatoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: api/candidatos
        [HttpGet("candidatos")]
        public async Task<ActionResult<List<CandidatoViewModel>>> Get()
        {
            var query = new ObterCandidatosUsuarioLogadoQuery();

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidatoDetalhadoViewModel>> Get([FromRoute] string id)
        {
            var query = new ObterCandidatoPorIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<Candidato>> Post([FromBody] CadastrarCandidatoCommand command)
        {
            var result = await _mediator.Send(command);
        
            if (result.IsSuccess)
            {
                return Ok("Candidato cadastrado com sucesso.");
            }
            else
            {
                if (result.Error.Any())
                {
                    return BadRequest(result.Error);
                }
        
                return StatusCode(500, "Ocorreu um erro inesperado ao cadastrar o candidato.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Candidato>> Put([FromRoute] string id, [FromBody] EditarCandidatoCommand command)
        {
            command.Id = id;
            
            var result = await _mediator.Send(command);
        
            if (result.IsSuccess)
            {
                return Ok("Candidato alterado com sucesso.");
            }
            else
            {
                if (result.Error.Any())
                {
                    return BadRequest(result.Error);
                }
        
                return StatusCode(500, "Ocorreu um erro inesperado ao editar o candidato.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var result = await _mediator.Send(new DeletarCandidatoCommand(id));
        
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
        
            return NoContent();
        }
    }
}


