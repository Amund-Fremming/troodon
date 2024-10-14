namespace troodon.Cli;

public class Generator
{
    public static string Model(string entity, string projectName)
    {
        return $"using System;\n\n" +
               $"namespace {projectName}.Cli\n" +
               $"{{\n" +
               $"    public class {entity}\n" +
               $"    {{\n" +
               $"        public Guid Id {{ get; set; }}\n\n" +
               $"        public {entity}()\n" +
               $"        {{\n" +
               $"            Id = Guid.NewGuid();\n" +
               $"        }}\n" +
               $"    }}\n" +
               $"}}";
    }


    public static string Controller(string entity, string projectName)
    {
        return $"using Microsoft.AspNetCore.Mvc; \n\n" +
            $"namespace {projectName}.Features.{entity}; \n" +
            $"\n" +
            $"public class {entity}Controller : ControllerBase\n" +
            $"{{\n" +
            $"    private readonly I{entity}Service _{entity.ToLower()}Service;\n\n" +
            $"    public {entity}Controller(I{entity}Service {entity.ToLower()}Service)\n" +
            $"    {{\n" +
            $"        _{entity.ToLower()}Service = {entity.ToLower()}Service;\n" +
            $"    }}\n\n" +

            // Create
            $"    [HttpPost]\n" +
            $"    public async Task<IActionResult> Create([FromBody] {entity} {entity.ToLower()})\n" +
            $"    {{\n" +
            $"        try\n" +
            $"        {{\n" +
            $"            await _{entity.ToLower()}Service.CreateAsync({entity.ToLower()});\n" +
            $"            return Ok();\n" +
            $"        }}\n" +
            $"       catch (Exception)\n" +
            $"       {{\n" +
            $"            return StatusCode(500, \"Internal server error occurred.\");\n" +
            $"        }}\n" +
            $"    }}\n\n" +

            // Read
            $"    [HttpGet(\"{{id}}\")] \n" +
            $"    public async Task<IActionResult> GetById(int id)\n" +
            $"    {{\n" +
            $"        try\n" +
            $"        {{\n" +
            $"            var result = await _{entity.ToLower()}Service.GetByIdAsync(id);\n" +
            $"            if (result == null) return NotFound();\n\n" +
            $"            return Ok(result);\n" +
            $"        }}\n" +
            $"        catch (Exception)\n" +
            $"        {{\n" +
            $"            return StatusCode(500, \"Internal server error occurred.\");\n" +
            $"        }}\n" +
            $"    }}\n\n" +

            // Update
            $"    [HttpPut(\"{{id}}\")] \n" +
            $"    public async Task<IActionResult> Update(int id, [FromBody] {entity} {entity.ToLower()})\n" +
            $"    {{\n" +
            $"        try\n" +
            $"        {{\n" +
            $"            if (id != {entity.ToLower()}.Id) return BadRequest();\n" +
            $"            await _{entity.ToLower()}Service.UpdateAsync(id, {entity.ToLower()});\n" +
            $"            return NoContent();\n" +
            $"        }}\n" +
            $"        catch (Exception)\n" +
            $"        {{\n" +
            $"            return StatusCode(500, \"Internal server error occurred.\");\n" +
            $"        }}\n" +
            $"    }}\n\n" +

            // Delete
            $"    [HttpDelete(\"{{id}}\")] \n" +
            $"    public async Task<IActionResult> Delete(int id)\n" +
            $"    {{\n" +
            $"        try\n" +
            $"        {{\n" +
            $"            await _{entity.ToLower()}Service.DeleteAsync(id);\n" +
            $"            return NoContent();\n" +
            $"        }}\n" +
            $"        catch (Exception)\n" +
            $"        {{\n" +
            $"            return StatusCode(500, \"Internal server error occurred.\");\n" +
            $"        }}\n" +
            $"    }}\n" +
            $"}}\n";
    }

    public static string Service(string entity, string projectName)
    {
        return $"namespace {projectName}.Features.{entity}; \n\n" +
            $"    public class {entity}Service : I{entity}Service\n" +
            $"    {{\n" +
            $"        private readonly I{entity}Repository _{entity.ToLower()}Repository;\n\n" +
            $"        public {entity}Service(I{entity}Repository {entity.ToLower()}Repository)\n" +
            $"        {{\n" +
            $"            _{entity.ToLower()}Repository = {entity.ToLower()}Repository;\n" +
            $"        }}\n\n" +

            // Create
            $"        public async Task<int> CreateAsync({entity} {entity.ToLower()})\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                return await _{entity.ToLower()}Repository.CreateAsync({entity.ToLower()});\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while creating the {entity.ToLower()}\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Read
            $"        public async Task<{entity}> GetByIdAsync(int id)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                return await _{entity.ToLower()}Repository.GetByIdAsync(id);\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while retrieving the {entity.ToLower()}\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Update
            $"        public async Task UpdateAsync(int id, {entity} {entity.ToLower()})\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                var existing{entity} = await _{entity.ToLower()}Repository.GetByIdAsync(id);\n" +
            $"                if (existing{entity} == null)\n" +
            $"                {{\n" +
            $"                    throw new KeyNotFoundException(\"{entity} not found\");\n" +
            $"                }}\n" +
            $"                await _{entity.ToLower()}Repository.UpdateAsync(id, {entity.ToLower()});\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while updating the {entity.ToLower()}\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Delete
            $"        public async Task DeleteAsync(int id)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                var existing{entity} = await _{entity.ToLower()}Repository.GetByIdAsync(id);\n" +
            $"                if (existing{entity} == null)\n" +
            $"                {{\n" +
            $"                    throw new KeyNotFoundException(\"{entity} not found\");\n" +
            $"                }}\n" +
            $"                await _{entity.ToLower()}Repository.DeleteAsync(id);\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while deleting the {entity.ToLower()}\", ex);\n" +
            $"            }}\n" +
            $"        }}\n" +
            $"    }}\n";
    }

    public static string Repository(string entity, string projectName)
    {
        return $"namespace {projectName}.Features.{entity}; \n\n" +
            $"    public class {entity}Repository : I{entity}Repository\n" +
            $"    {{\n" +
            $"        private readonly AppDbContext _context;\n\n" +
            $"        public {entity}Repository(AppDbContext context)\n" +
            $"        {{\n" +
            $"            _context = context;\n" +
            $"        }}\n\n" +

            // Create
            $"        public async Task<int> CreateAsync({entity} {entity.ToLower()})\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                _context.{entity}s.Add({entity.ToLower()});\n" +
            $"                await _context.SaveChangesAsync();\n" +
            $"                return {entity.ToLower()}.Id; // Assuming Id is the primary key\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while adding the {entity.ToLower()} to the database\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Read
            $"        public async Task<{entity}> GetByIdAsync(int id)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                return await _context.{entity}s.FindAsync(id);\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while retrieving the {entity.ToLower()} from the database\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Update
            $"        public async Task UpdateAsync(int id, {entity} {entity.ToLower()})\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                var existing{entity} = await _context.{entity}s.FindAsync(id);\n" +
            $"                if (existing{entity} == null)\n" +
            $"                {{\n" +
            $"                    throw new KeyNotFoundException(\"{entity} not found\");\n" +
            $"                }}\n" +
            $"                _context.Entry(existing{entity}).CurrentValues.SetValues({entity.ToLower()});\n" +
            $"                await _context.SaveChangesAsync();\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while updating the {entity.ToLower()} in the database\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Delete
            $"        public async Task DeleteAsync(int id)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                var existing{entity} = await _context.{entity}s.FindAsync(id);\n" +
            $"                if (existing{entity} == null)\n" +
            $"                {{\n" +
            $"                    throw new KeyNotFoundException(\"{entity} not found\");\n" +
            $"                }}\n" +
            $"                _context.{entity}s.Remove(existing{entity});\n" +
            $"                await _context.SaveChangesAsync();\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while deleting the {entity.ToLower()} from the database\", ex);\n" +
            $"            }}\n" +
            $"        }}\n" +
            $"    }}\n";
    }

    public static string Interface(string entity, string projectName, string type)
    {
        var interfaceName = type.Equals("service") ? $"{entity}Service" : $"{entity}Repository";

        return $"namespace {projectName}.Features.{entity};\n\n" +
               $"public interface I{interfaceName}\n" +
               $"{{\n" +
               $"    // Create\n" +
               $"    Task<int> CreateAsync({entity} {entity.ToLower()});\n\n" +

               $"    // Read\n" +
               $"    Task<{entity}> GetByIdAsync(int id);\n\n" +

               $"    // Update\n" +
               $"    Task UpdateAsync(int id, {entity} {entity.ToLower()});\n\n" +

               $"    // Delete\n" +
               $"    Task DeleteAsync(int id);\n" +
               $"}}\n";
    }

    public static string Program(string projectName)
    {
        throw new NotImplementedException();
    }

    public static string DbContext(string projectName)
    {
        throw new NotImplementedException();
    }
}
