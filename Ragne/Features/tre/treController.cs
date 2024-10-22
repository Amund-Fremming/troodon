using Microsoft.AspNetCore.Mvc; 

namespace Ragne.Features.tre; 

public class treController : ControllerBase
{
    private readonly ItreService _treService;

    public treController(ItreService treService)
    {
        _treService = treService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] treModel treModel)
    {
        try
        {
            await _treService.CreateAsync(treModel);
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
            var result = await _treService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Update(Guid id, [FromBody] treModel treModel)
    {
        try
        {
            if (id != treModel.Id) return BadRequest();
            await _treService.UpdateAsync(id, treModel);
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
            await _treService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }
}
