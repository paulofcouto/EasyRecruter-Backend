using Easy.Application.Commands.CadastrarEmpresa;
using Easy.Application.Queries.ObterEmpresaDetalhadoPorUsuario;
using Easy.Application.Queries.ObterEmpresaPorUsuario;
using Easy.Application.ViewModel;
using Easy.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Easy.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpresaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("EmpresaResumo")]
        public async Task<ActionResult<EmpresaResumoViewModel>> ObterEmpresaResumo()
        {
            var query = new ObterEmpresaPorUsuarioQuery();
        
            var result = await _mediator.Send(query);
        
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
        
            return Ok(result.Value);
        }

        [HttpGet("EmpresaDetalhado")]
        public async Task<ActionResult<EmpresaDetalhadoViewModel>> ObterEmpresaDetalhado()
        {
            var query = new ObterEmpresaDetalhadoPorUsuarioQuery();

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> Post([FromBody] CadastrarEmpresaCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Empresa cadastrado com sucesso.");
            }
            else
            {
                if (result.Error.Any())
                {
                    return BadRequest(result.Error);
                }

                return StatusCode(500, "Ocorreu um erro inesperado ao cadastrar a empresa.");
            }
        }
    }
}
