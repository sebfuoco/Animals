using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAnimalRepository
    {
        public Task<List<Animal>> GetAllAsync();
        public Task<Animal?> GetAsync(Guid id);
        public Task<List<string>> GetAllByTagAsync(string tag);
        public Task CreateAsync(Animal animal);
        public Task UpdateAsync();
    }
}
