using Microsoft.AspNetCore.Mvc; 

namespace Ragne.Features.to; 

public class toController : ControllerBase
{
    private readonly ItoService _toService;

    public toController(ItoService toService)
    {
        _toService = toService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] toModel toModel)
    {
        try
        {
            await _toService.CreateAsync(toModel);
            return Ok();
        }
       catch (Exception)
       {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpGet("{id}")] 
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _toService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Update(Guid id, [FromBody] toModel toModel)
    {
        try
        {
            if (id != toModel.Id) return BadRequest();
            await _toService.UpdateAsync(id, toModel);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _toService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }
}
