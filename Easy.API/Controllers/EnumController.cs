using Easy.Application.Queries.ObterEstados.Easy.Application.Queries;
using Easy.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Easy.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EnumController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnumController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Estados")]
        public async Task<ActionResult<List<EstadoViewModel>>> Get()
        {
            var query = new ObterEnumEstadoQuery();

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
