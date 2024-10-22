using Microsoft.AspNetCore.Mvc; 

namespace Ragne.Features.en; 

public class enController : ControllerBase
{
    private readonly IenService _enService;

    public enController(IenService enService)
    {
        _enService = enService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] enModel enModel)
    {
        try
        {
            await _enService.CreateAsync(enModel);
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
            var result = await _enService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Update(Guid id, [FromBody] enModel enModel)
    {
        try
        {
            if (id != enModel.Id) return BadRequest();
            await _enService.UpdateAsync(id, enModel);
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
            await _enService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }
}
