using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly DataContext _dbContext;

        public AnimalRepository(DataContext dbContext) => _dbContext = dbContext;
        public List<Animal> GetAll() => _dbContext.Animals.ToList();
        public Animal Get(Guid id) => _dbContext.Animals.FirstOrDefault(x => x.Id == id);
        public void Add(Animal animal) => _dbContext.Animals.Add(animal);
        public void Update(Animal animal) => _dbContext.Animals.Update(animal);
    }
}
