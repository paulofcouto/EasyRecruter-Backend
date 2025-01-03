using Easy.Application.Queries.LoginUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Easy.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AutenticarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutenticarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioQuery query)
        {
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
