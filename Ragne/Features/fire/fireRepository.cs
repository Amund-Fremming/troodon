using Ragne.Infrastructure; 

namespace Ragne.Features.fire; 

    public class fireRepository : IfireRepository
    {
        private readonly AppDbContext _context;

        public fireRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(fireModel fireModel)
        {
            try
            {
                _context.fire.Add(fireModel);
                await _context.SaveChangesAsync();
                return fireModel.Id; // Assuming Id is the primary key
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the fireModel to the database", ex);
            }
        }

        public async Task<fireModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.fire.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the fireModel from the database", ex);
            }
        }

        public async Task UpdateAsync(Guid id, fireModel fireModel)
        {
            try
            {
                var existingfire = await _context.fire.FindAsync(id);
                if (existingfire == null)
                {
                    throw new KeyNotFoundException("fire not found");
                }
                _context.Entry(existingfire).CurrentValues.SetValues(fireModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the fireModel in the database", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingfire = await _context.fire.FindAsync(id);
                if (existingfire == null)
                {
                    throw new KeyNotFoundException("fire not found");
                }
                _context.fire.Remove(existingfire);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the fireModel from the database", ex);
            }
        }
    }
