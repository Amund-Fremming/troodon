using Ragne.Infrastructure; 

namespace Ragne.Features.seks; 

    public class seksRepository : IseksRepository
    {
        private readonly AppDbContext _context;

        public seksRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(seksModel seksModel)
        {
            try
            {
                _context.seks.Add(seksModel);
                await _context.SaveChangesAsync();
                return seksModel.Id; // Assuming Id is the primary key
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the seksModel to the database", ex);
            }
        }

        public async Task<seksModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.seks.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the seksModel from the database", ex);
            }
        }

        public async Task UpdateAsync(Guid id, seksModel seksModel)
        {
            try
            {
                var existingseks = await _context.seks.FindAsync(id);
                if (existingseks == null)
                {
                    throw new KeyNotFoundException("seks not found");
                }
                _context.Entry(existingseks).CurrentValues.SetValues(seksModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the seksModel in the database", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingseks = await _context.seks.FindAsync(id);
                if (existingseks == null)
                {
                    throw new KeyNotFoundException("seks not found");
                }
                _context.seks.Remove(existingseks);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the seksModel from the database", ex);
            }
        }
    }
