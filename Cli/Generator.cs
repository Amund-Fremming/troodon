namespace troodon.Cli;

public class Generator
{
    public static string Model(string entity, string projectName)
    {
        return $"using System;\n\n" +
               $"namespace {projectName}.Features.{entity}; \n\n" +
               $"public class {entity}Model\n" +
               $"{{\n" +
               $"    public Guid Id {{ get; set; }}\n\n" +
               $"    public {entity}Model()\n" +
               $"    {{\n" +
               $"        Id = Guid.NewGuid();\n" +
               $"    }}\n" +
               $"}}\n";
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
            $"    public async Task<IActionResult> Create([FromBody] {entity}Model {entity.ToLower()}Model)\n" +
            $"    {{\n" +
            $"        try\n" +
            $"        {{\n" +
            $"            await _{entity.ToLower()}Service.CreateAsync({entity.ToLower()}Model);\n" +
            $"            return Ok();\n" +
            $"        }}\n" +
            $"       catch (Exception)\n" +
            $"       {{\n" +
            $"            return StatusCode(500, \"Internal server error occurred.\");\n" +
            $"        }}\n" +
            $"    }}\n\n" +

            // Read
            $"    [HttpGet(\"{{id}}\")] \n" +
            $"    public async Task<IActionResult> GetById(Guid id)\n" +
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
            $"    public async Task<IActionResult> Update(Guid id, [FromBody] {entity}Model {entity.ToLower()}Model)\n" +
            $"    {{\n" +
            $"        try\n" +
            $"        {{\n" +
            $"            if (id != {entity.ToLower()}Model.Id) return BadRequest();\n" +
            $"            await _{entity.ToLower()}Service.UpdateAsync(id, {entity.ToLower()}Model);\n" +
            $"            return NoContent();\n" +
            $"        }}\n" +
            $"        catch (Exception)\n" +
            $"        {{\n" +
            $"            return StatusCode(500, \"Internal server error occurred.\");\n" +
            $"        }}\n" +
            $"    }}\n\n" +

            // Delete
            $"    [HttpDelete(\"{{id}}\")] \n" +
            $"    public async Task<IActionResult> Delete(Guid id)\n" +
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
            $"        public async Task<Guid> CreateAsync({entity}Model {entity.ToLower()}Model)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                return await _{entity.ToLower()}Repository.CreateAsync({entity.ToLower()}Model);\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while creating the {entity.ToLower()}Model\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Read
            $"        public async Task<{entity}Model> GetByIdAsync(Guid id)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                return await _{entity.ToLower()}Repository.GetByIdAsync(id);\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while retrieving the {entity.ToLower()}Model\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Update
            $"        public async Task UpdateAsync(Guid id, {entity}Model {entity.ToLower()}Model)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                var existing{entity} = await _{entity.ToLower()}Repository.GetByIdAsync(id);\n" +
            $"                if (existing{entity} == null)\n" +
            $"                {{\n" +
            $"                    throw new KeyNotFoundException(\"{entity} not found\");\n" +
            $"                }}\n" +
            $"                await _{entity.ToLower()}Repository.UpdateAsync(id, {entity.ToLower()}Model);\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while updating the {entity.ToLower()}Model\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Delete
            $"        public async Task DeleteAsync(Guid id)\n" +
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
            $"                throw new Exception(\"An error occurred while deleting the {entity.ToLower()}Model\", ex);\n" +
            $"            }}\n" +
            $"        }}\n" +
            $"    }}\n";
    }

    public static string Repository(string entity, string projectName)
    {
        return $"using {projectName}.Infrastructure; \n\n" +
            $"namespace {projectName}.Features.{entity}; \n\n" +
            $"    public class {entity}Repository : I{entity}Repository\n" +
            $"    {{\n" +
            $"        private readonly AppDbContext _context;\n\n" +
            $"        public {entity}Repository(AppDbContext context)\n" +
            $"        {{\n" +
            $"            _context = context;\n" +
            $"        }}\n\n" +

            // Create
            $"        public async Task<Guid> CreateAsync({entity}Model {entity.ToLower()}Model)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                _context.{entity}.Add({entity.ToLower()}Model);\n" +
            $"                await _context.SaveChangesAsync();\n" +
            $"                return {entity.ToLower()}Model.Id; // Assuming Id is the primary key\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while adding the {entity.ToLower()}Model to the database\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Read
            $"        public async Task<{entity}Model> GetByIdAsync(Guid id)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                return await _context.{entity}.FindAsync(id);\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while retrieving the {entity.ToLower()}Model from the database\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Update
            $"        public async Task UpdateAsync(Guid id, {entity}Model {entity.ToLower()}Model)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                var existing{entity} = await _context.{entity}.FindAsync(id);\n" +
            $"                if (existing{entity} == null)\n" +
            $"                {{\n" +
            $"                    throw new KeyNotFoundException(\"{entity} not found\");\n" +
            $"                }}\n" +
            $"                _context.Entry(existing{entity}).CurrentValues.SetValues({entity.ToLower()}Model);\n" +
            $"                await _context.SaveChangesAsync();\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while updating the {entity.ToLower()}Model in the database\", ex);\n" +
            $"            }}\n" +
            $"        }}\n\n" +

            // Delete
            $"        public async Task DeleteAsync(Guid id)\n" +
            $"        {{\n" +
            $"            try\n" +
            $"            {{\n" +
            $"                var existing{entity} = await _context.{entity}.FindAsync(id);\n" +
            $"                if (existing{entity} == null)\n" +
            $"                {{\n" +
            $"                    throw new KeyNotFoundException(\"{entity} not found\");\n" +
            $"                }}\n" +
            $"                _context.{entity}.Remove(existing{entity});\n" +
            $"                await _context.SaveChangesAsync();\n" +
            $"            }}\n" +
            $"            catch (Exception ex)\n" +
            $"            {{\n" +
            $"                throw new Exception(\"An error occurred while deleting the {entity.ToLower()}Model from the database\", ex);\n" +
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
               $"    Task<Guid> CreateAsync({entity}Model {entity.ToLower()}Model);\n\n" +

               $"    // Read\n" +
               $"    Task<{entity}Model> GetByIdAsync(Guid id);\n\n" +

               $"    // Update\n" +
               $"    Task UpdateAsync(Guid id, {entity}Model {entity.ToLower()}Model);\n\n" +

               $"    // Delete\n" +
               $"    Task DeleteAsync(Guid id);\n" +
               $"}}\n";
    }

    public static string Program(IEnumerable<string> entities, string projectName)
    {
        var dbRegistration = "builder.Services.AddDbContext<AppDbContext>(options => \n" +
            $"    options.UseNpgsql(builder.Configuration.GetConnectionString(\"DefaultConnection\")));";

        var usings = string.Join("\n", entities.Select(entity =>
                $"using {projectName}.Features.{entity};"));

        var registerServices = string.Join("\n", entities.Select(entity =>
            $"builder.Services.AddScoped<I{entity}Service, {entity}Service>();\n" +
            $"builder.Services.AddScoped<I{entity}Repository, {entity}Repository>();\n"));

        return $"using {projectName}.Infrastructure;\n" +
               $"using Microsoft.EntityFrameworkCore;\n" +
               $"{usings}\n" +
               $"using Microsoft.OpenApi.Models;\n\n" +
               $"var builder = WebApplication.CreateBuilder(args);\n\n" +

               $"{registerServices}" +
               $"builder.Services.AddControllers();\n\n" +
               $"{dbRegistration}\n\n" +

               $"builder.Services.AddEndpointsApiExplorer();\n" +
               $"builder.Services.AddSwaggerGen(c =>\n" +
               $"{{\n" +
               $"    c.SwaggerDoc(\"v1\", new OpenApiInfo {{ Title = \"{projectName} API\", Version = \"v1\" }});\n" +
               $"}});\n\n" +

               $"var app = builder.Build();\n\n" +

               $"if (app.Environment.IsDevelopment())\n" +
               $"{{\n" +
               $"    app.UseSwagger();\n" +
               $"    app.UseSwaggerUI(c => c.SwaggerEndpoint(\"/swagger/v1/swagger.json\", \"{projectName} API v1\"));\n" +
               $"}}\n\n" +

               $"app.UseHttpsRedirection();\n\n" +

               $"app.UseAuthorization();\n\n" +

               $"app.MapControllers();\n\n" +

               $"app.Run();\n";
    }

    public static string DbContext(IEnumerable<string> entities, string projectName)
    {
        var usings = string.Join("\n", entities.Select(entity =>
            $"using {projectName}.Features.{entity};"));

        var dbSets = string.Join("\n", entities.Select(entity =>
            $"    public DbSet<{entity}Model> {entity} {{ get; set; }}\n"));

        var configurePrimaryKeys = string.Join("\n", entities.Select(entity =>
            $"        modelBuilder.Entity<{entity}Model>().HasKey(e => e.Id);\n"));

        return $"using Microsoft.EntityFrameworkCore;\n" +
               $"{usings}\n\n" +
               $"namespace {projectName}.Infrastructure;\n\n" +
               $"public class AppDbContext : DbContext\n" +
               $"{{\n" +
               $"{dbSets}\n" +
               $"    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)\n" +
               $"    {{\n" +
               $"    }}\n\n" +
               $"    protected override void OnModelCreating(ModelBuilder modelBuilder)\n" +
               $"    {{\n" +
               $"{configurePrimaryKeys}" +
               $"    }}\n" +
               $"}}\n";
    }
}