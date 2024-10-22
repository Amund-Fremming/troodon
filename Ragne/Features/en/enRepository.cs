using Ragne.Infrastructure; 

namespace Ragne.Features.en; 

    public class enRepository : IenRepository
    {
        private readonly AppDbContext _context;

        public enRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(enModel enModel)
        {
            try
            {
                _context.en.Add(enModel);
                await _context.SaveChangesAsync();
                return enModel.Id; // Assuming Id is the primary key
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the enModel to the database", ex);
            }
        }

        public async Task<enModel> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.en.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the enModel from the database", ex);
            }
        }

        public async Task UpdateAsync(Guid id, enModel enModel)
        {
            try
            {
                var existingen = await _context.en.FindAsync(id);
                if (existingen == null)
                {
                    throw new KeyNotFoundException("en not found");
                }
                _context.Entry(existingen).CurrentValues.SetValues(enModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the enModel in the database", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingen = await _context.en.FindAsync(id);
                if (existingen == null)
                {
                    throw new KeyNotFoundException("en not found");
                }
                _context.en.Remove(existingen);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the enModel from the database", ex);
            }
        }
    }
