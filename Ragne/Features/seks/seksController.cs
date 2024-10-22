using Microsoft.AspNetCore.Mvc; 

namespace Ragne.Features.seks; 

public class seksController : ControllerBase
{
    private readonly IseksService _seksService;

    public seksController(IseksService seksService)
    {
        _seksService = seksService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] seksModel seksModel)
    {
        try
        {
            await _seksService.CreateAsync(seksModel);
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
            var result = await _seksService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Update(Guid id, [FromBody] seksModel seksModel)
    {
        try
        {
            if (id != seksModel.Id) return BadRequest();
            await _seksService.UpdateAsync(id, seksModel);
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
            await _seksService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error occurred.");
        }
    }
}
