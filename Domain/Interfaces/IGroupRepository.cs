using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGroupRepository
    {
        public Task<List<Group>> GetAllAsync();
        public Task<Group?> GetAsync(Guid id);
        public Task<Group?> GetByIdAsync(Guid id);
        public Task<List<string>> GetAllByNameAsync(string name);
        public Task CreateAsync(Group group);
        public Task UpdateAsync();
    }
}
