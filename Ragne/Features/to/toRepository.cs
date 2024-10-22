using Ragne.Infrastructure; 

namespace Ragne.Features.to; 

    public class toRepository : ItoRepository
    {
        private readonly AppDbContext _context;

        public toRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(toModel toModel)
        {
            try
            {
                _context.to.Add(toModel);
                await _context.SaveChangesAsync();
                return toModel.Id; // Assuming Id is the primary key
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the toModel to the database", ex);
            }
        }

        public async Task<toModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.to.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the toModel from the database", ex);
            }
        }

        public async Task UpdateAsync(Guid id, toModel toModel)
        {
            try
            {
                var existingto = await _context.to.FindAsync(id);
                if (existingto == null)
                {
                    throw new KeyNotFoundException("to not found");
                }
                _context.Entry(existingto).CurrentValues.SetValues(toModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the toModel in the database", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingto = await _context.to.FindAsync(id);
                if (existingto == null)
                {
                    throw new KeyNotFoundException("to not found");
                }
                _context.to.Remove(existingto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the toModel from the database", ex);
            }
        }
    }
