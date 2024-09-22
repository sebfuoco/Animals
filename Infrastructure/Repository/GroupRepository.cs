using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _dbContext;

        public GroupRepository(DataContext dbContext) => _dbContext = dbContext;
        public List<Group> GetAll() => _dbContext.Groups.ToList();
        public Group Get(Guid id) => _dbContext.Groups.FirstOrDefault(x => x.Id == id);
        public void Add(Group group) => _dbContext.Groups.Add(group);
        public void Update(Group group) => _dbContext.Groups.Update(group);
    }
}
