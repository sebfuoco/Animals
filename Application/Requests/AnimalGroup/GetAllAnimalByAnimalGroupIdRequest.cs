using Application.Dtos;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.AnimalGroup
{
    public class GetAllAnimalByAnimalGroupIdRequest() : IRequest<List<AnimalDto>>
    {
        public Guid Id { get; set; }
    }
    public class GetAllAnimalByAnimalGroupIdRequestHandler(AnimalGroupRepository animalGroupRepository, AnimalRepository animalRepository) 
        : IRequestHandler<GetAllAnimalByAnimalGroupIdRequest, List<AnimalDto>>
    {
        private readonly AnimalGroupRepository _animalGroupRepository = animalGroupRepository;
        private readonly AnimalRepository _animalRepository = animalRepository;

        public async Task<List<AnimalDto>> Handle(GetAllAnimalByAnimalGroupIdRequest request, CancellationToken cancellationToken)
        {
            List<AnimalGroups> animalGroups = await _animalGroupRepository.GetAllByGroupIdAsync(request.Id);
            List<AnimalDto> animalsDto = new();

            foreach (AnimalGroups animalGroup in animalGroups)
            {
                Domain.Entities.Animal? animal = await _animalRepository.GetAsync(animalGroup.AnimalId);
                AnimalDto animalDto = new()
                {
                    Id = animal.Id,
                    Tag = animal.Tag
                };

                animalsDto.Add(animalDto);
            }
            return animalsDto;
        }
    }
}
