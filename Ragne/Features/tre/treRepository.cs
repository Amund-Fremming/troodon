using Ragne.Infrastructure; 

namespace Ragne.Features.tre; 

    public class treRepository : ItreRepository
    {
        private readonly AppDbContext _context;

        public treRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(treModel treModel)
        {
            try
            {
                _context.tre.Add(treModel);
                await _context.SaveChangesAsync();
                return treModel.Id; // Assuming Id is the primary key
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the treModel to the database", ex);
            }
        }

        public async Task<treModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.tre.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the treModel from the database", ex);
            }
        }

        public async Task UpdateAsync(Guid id, treModel treModel)
        {
            try
            {
                var existingtre = await _context.tre.FindAsync(id);
                if (existingtre == null)
                {
                    throw new KeyNotFoundException("tre not found");
                }
                _context.Entry(existingtre).CurrentValues.SetValues(treModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the treModel in the database", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingtre = await _context.tre.FindAsync(id);
                if (existingtre == null)
                {
                    throw new KeyNotFoundException("tre not found");
                }
                _context.tre.Remove(existingtre);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the treModel from the database", ex);
            }
        }
    }
