using Application.Exceptions;
using Application.Interfaces;
using Application.Outcome.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebSkladPetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OutcomeController : ControllerBase
    {
        private readonly IOutcomeService _service;

        public OutcomeController(IOutcomeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OutcomeDTO>>> Get()
        {
            var outcomeDto = await _service.GetAllAsync();
            return Ok(outcomeDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OutcomeDTO>> Get(Guid id)
        {
            try
            {
                var outcomeDto = await _service.GetByIdAsync(id);
                return outcomeDto;
            }
            catch (OutcomeNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(OutcomeDTO outcomeDto)
        {
            try
            {
                await _service.CreateAsync(outcomeDto);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(OutcomeDTO outcomeDto)
        {
            try
            {
                await _service.UpdateAsync(outcomeDto);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
