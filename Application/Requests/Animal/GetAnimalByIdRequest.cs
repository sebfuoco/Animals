using Application.Dtos;
using MediatR;
using Infrastructure.Repositories;

namespace Application.Requests.Animal
{
    public class GetAnimalByIdRequest() : IRequest<AnimalDto>
    {
        public Guid Id { get; set; }
    }

    public class GetAnimalByIdRequestHandler(AnimalRepository animalRepository) : IRequestHandler<GetAnimalByIdRequest, AnimalDto>
    {
        private readonly AnimalRepository _animalRepository = animalRepository;

        public async Task<AnimalDto> Handle(GetAnimalByIdRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Animal? animal = await _animalRepository.GetAsync(request.Id);
            return new()
            {
                Id = animal.Id,
                Tag = animal.Tag,
                DateOfBirth = animal.DateOfBirth,
            };
        }
    }
}
