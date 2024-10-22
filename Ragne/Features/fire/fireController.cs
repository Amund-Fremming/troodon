using Microsoft.AspNetCore.Mvc; 

namespace Ragne.Features.fire; 

public class fireController : ControllerBase
{
    private readonly IfireService _fireService;

    public fireController(IfireService fireService)
    {
        _fireService = fireService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] fireModel fireModel)
    {
        try
        {
            await _fireService.CreateAsync(fireModel);
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
            var result = await _fireService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Update(Guid id, [FromBody] fireModel fireModel)
    {
        try
        {
            if (id != fireModel.Id) return BadRequest();
            await _fireService.UpdateAsync(id, fireModel);
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
            await _fireService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }
}
