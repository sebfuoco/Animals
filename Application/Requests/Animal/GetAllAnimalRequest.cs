using Application.Dtos;
using MediatR;
using Infrastructure.Repositories;
namespace Application.Requests.Animal
{
    public class GetAllAnimalRequest() : IRequest<List<AnimalDto>>;
    public class GetAllAnimalRequestHandler(AnimalRepository animalRepository) : IRequestHandler<GetAllAnimalRequest, List<AnimalDto>>
    {
        private readonly AnimalRepository _animalRepository = animalRepository;

        public async Task<List<AnimalDto>> Handle(GetAllAnimalRequest request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Animal> animals = await _animalRepository.GetAllAsync();
            List<AnimalDto> animalsDto = new();
            foreach (Domain.Entities.Animal animal in animals)
            {
                AnimalDto animalDto = new()
                {
                    Id = animal.Id,
                    Tag = animal.Tag,
                    DateOfBirth = animal.DateOfBirth,
                };

                animalsDto.Add(animalDto);
            }
            return animalsDto;
        }
    }
}
