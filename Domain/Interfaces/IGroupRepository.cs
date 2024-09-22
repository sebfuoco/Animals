using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGroupRepository
    {
        List<Group> GetAll();
        Group Get(Guid id);
        void Add(Group group);
        void Update(Group group);
    }
}
