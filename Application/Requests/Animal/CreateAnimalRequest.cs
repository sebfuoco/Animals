using Domain.Interfaces;
using MediatR;
using AnimalObj = Domain.Entities.Animal;

namespace Application.Requests.Animal
{
    public class CreateAnimalRequest() : IRequest
    {
        public required string Tag { get; set; }
    }
    public class CreateAnimalRequestHandler(IAnimalRepository animalRepo) : IRequestHandler<CreateAnimalRequest>
    {
        private readonly IAnimalRepository _animalRepo = animalRepo;

        public async Task Handle(CreateAnimalRequest request, CancellationToken cancellationToken)
        {
            AnimalObj animal = new() 
            {
                Id = Guid.NewGuid(),
                Tag = request.Tag,
                DateOfBirth = DateTime.Now
            };
            _animalRepo.Add(animal);
        }
    }
}
