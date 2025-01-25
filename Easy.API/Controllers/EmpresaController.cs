using Easy.Application.Commands.CadastrarEmpresa;
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

        //[HttpGet("{id}")]
        //public async Task<ActionResult<EmpresaDetalhadoViewModel>> Get([FromRoute] string id)
        //{
        //    var query = new ObterEmpresaPorIdQuery(id);
        //
        //    var result = await _mediator.Send(query);
        //
        //    if (!result.IsSuccess)
        //    {
        //        return NotFound(result.Error);
        //    }
        //
        //    return Ok(result.Value);
        //}

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
