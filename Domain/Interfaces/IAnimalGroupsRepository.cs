using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAnimalGroupsRepository
    {
        public Task<List<AnimalGroups>> GetAllByGroupIdAsync(Guid id);
        public Task<List<AnimalGroups>> GetAllByAnimalIdAsync(Guid id);
        public Task<AnimalGroups?> GetAsync(Guid animalId, Guid groupId);
        public Task CreateAsync(AnimalGroups animalGroup);
        public Task DeleteAsync(AnimalGroups group);
    }
}
