using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class AnimalGroupRepository(DataContext dbContext) : IAnimalGroupsRepository
    {
        private readonly DataContext _dbContext = dbContext;

        public async Task<List<AnimalGroups>> GetAllByGroupIdAsync(Guid id) => await _dbContext.AnimalGroups.AsNoTracking().Where(x => x.GroupId == id).ToListAsync();
        public async Task<List<AnimalGroups>> GetAllByAnimalIdAsync(Guid id) => await _dbContext.AnimalGroups.AsNoTracking().Where(x => x.AnimalId == id).ToListAsync();
        public async Task<AnimalGroups?> GetAsync(Guid animalId, Guid groupId) => await _dbContext.AnimalGroups.FirstOrDefaultAsync(x => x.GroupId == groupId && x.AnimalId == animalId);
        public async Task CreateAsync(AnimalGroups animalGroup)
        {
            await _dbContext.AnimalGroups.AddAsync(animalGroup);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(AnimalGroups animalGroup)
        {
            _dbContext.AnimalGroups.Remove(animalGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
}
