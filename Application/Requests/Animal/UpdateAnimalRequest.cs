using Application.Dtos;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.Animal
{
    public class UpdateAnimalRequest() : IRequest
    {
        public required AnimalDto AnimalDto { get; set; }
    }
    public class UpdateAnimalRequestHandler(AnimalRepository animalRepository) : IRequestHandler<UpdateAnimalRequest>
    {
        private readonly AnimalRepository _animalRepository = animalRepository;

        public async Task Handle(UpdateAnimalRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Animal? animal = await _animalRepository.GetByIdAsync(request.AnimalDto.Id);
            List<string> animalTags = await _animalRepository.GetAllByTagAsync(request.AnimalDto.Tag);

            if (animalTags.Count == 0 && animal is not null)
            {
                animal.Tag = request.AnimalDto.Tag;
                animal.DateOfBirth = request.AnimalDto.DateOfBirth;

                await _animalRepository.UpdateAsync();
            }
        }
    }
}
