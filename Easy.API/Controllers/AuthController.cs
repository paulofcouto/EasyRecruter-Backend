using Easy.Application.Queries.LoginUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Easy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
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
