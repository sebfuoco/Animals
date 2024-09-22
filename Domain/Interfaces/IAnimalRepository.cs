using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAnimalRepository
    {
        List<Animal> GetAll();
        Animal Get(Guid id);
        void Add(Animal animal);
        void Update(Animal animal);
    }
}
