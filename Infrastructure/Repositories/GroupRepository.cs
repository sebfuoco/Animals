using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupRepository(DataContext dbContext) : IGroupRepository
    {
        private readonly DataContext _dbContext = dbContext;

        public async Task<List<Group>> GetAllAsync() => await _dbContext.Groups.AsNoTracking().ToListAsync();
        public async Task<Group?> GetAsync(Guid id) => await _dbContext.Groups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Group?> GetByIdAsync(Guid id) => await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<List<string>> GetAllByNameAsync(string name) => await _dbContext.Groups.AsNoTracking().Select(x => x.Name).Where(x => x == name).ToListAsync();
        public async Task CreateAsync(Group group)
        {
            await _dbContext.Groups.AddAsync(group);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
