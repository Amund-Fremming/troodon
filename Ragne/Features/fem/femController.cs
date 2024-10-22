using Microsoft.AspNetCore.Mvc; 

namespace Ragne.Features.fem; 

public class femController : ControllerBase
{
    private readonly IfemService _femService;

    public femController(IfemService femService)
    {
        _femService = femService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] femModel femModel)
    {
        try
        {
            await _femService.CreateAsync(femModel);
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
            var result = await _femService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Update(Guid id, [FromBody] femModel femModel)
    {
        try
        {
            if (id != femModel.Id) return BadRequest();
            await _femService.UpdateAsync(id, femModel);
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
            await _femService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }
}
