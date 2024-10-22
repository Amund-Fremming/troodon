using Ragne.Infrastructure; 

namespace Ragne.Features.fem; 

    public class femRepository : IfemRepository
    {
        private readonly AppDbContext _context;

        public femRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(femModel femModel)
        {
            try
            {
                _context.fem.Add(femModel);
                await _context.SaveChangesAsync();
                return femModel.Id; // Assuming Id is the primary key
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the femModel to the database", ex);
            }
        }

        public async Task<femModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.fem.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the femModel from the database", ex);
            }
        }

        public async Task UpdateAsync(Guid id, femModel femModel)
        {
            try
            {
                var existingfem = await _context.fem.FindAsync(id);
                if (existingfem == null)
                {
                    throw new KeyNotFoundException("fem not found");
                }
                _context.Entry(existingfem).CurrentValues.SetValues(femModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the femModel in the database", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingfem = await _context.fem.FindAsync(id);
                if (existingfem == null)
                {
                    throw new KeyNotFoundException("fem not found");
                }
                _context.fem.Remove(existingfem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the femModel from the database", ex);
            }
        }
    }
