using Application.Dtos;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.Animal
{
    public class CreateAnimalRequest() : IRequest
    {
        public required AnimalDto AnimalDto { get; set; }
    }
    public class CreateAnimalRequestHandler(AnimalRepository animalRepository) : IRequestHandler<CreateAnimalRequest>
    {
        private readonly AnimalRepository _animalRepository = animalRepository;

        public async Task Handle(CreateAnimalRequest request, CancellationToken cancellationToken)
        {
            List<string> animalTags = await _animalRepository.GetAllByTagAsync(request.AnimalDto.Tag);
            if (animalTags.Count == 0)
            {
                Domain.Entities.Animal createAnimal = new()
                {
                    Id = Guid.NewGuid(),
                    Tag = request.AnimalDto.Tag,
                    DateOfBirth = DateTime.Now,
                };
                if (request.AnimalDto.AnimalGroups is not null)
                {
                    foreach (AnimalGroupDto animalGroupDto in request.AnimalDto.AnimalGroups)
                    {
                        AnimalGroups animalGroup = new()
                        {
                            Id = Guid.NewGuid(),
                            AnimalId = createAnimal.Id,
                            GroupId = animalGroupDto.Group.Id
                        };
                        createAnimal.AnimalGroups ??= new List<AnimalGroups>();
                        createAnimal.AnimalGroups.Add(animalGroup);
                    }
                }

                await _animalRepository.CreateAsync(createAnimal);
            }
        }
    }
}
