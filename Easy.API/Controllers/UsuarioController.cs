using Easy.Application.Commands.CadastrarUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Easy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("teste")]
        public async Task<IActionResult> Teste()
        {
            return Ok("Funcionou!");
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Usuário cadastrado com sucesso.");
            }
            else
            {
                if (result.Error.Any())
                {
                    return BadRequest(result.Error);
                }

                return StatusCode(500, "Ocorreu um erro inesperado ao cadastrar o usuário.");
            }
        }
    }
}
