using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AnimalRepository(DataContext dbContext) : IAnimalRepository
    {
        private readonly DataContext _dbContext = dbContext;

        public async Task<List<Animal>> GetAllAsync() => await _dbContext.Animals.AsNoTracking().ToListAsync();
        public async Task<Animal?> GetAsync(Guid id) => await _dbContext.Animals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Animal?> GetByIdAsync(Guid id) => await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<List<string>> GetAllByTagAsync(string tag) => await _dbContext.Animals.AsNoTracking().Select(x => x.Tag).Where(x => x == tag).ToListAsync();
        public async Task CreateAsync(Animal animal)
        {
            await _dbContext.Animals.AddAsync(animal);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
