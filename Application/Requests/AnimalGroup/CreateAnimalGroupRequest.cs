using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.AnimalGroup
{
    public class CreateAnimalGroupRequest : IRequest
    {
        public AnimalGroups AnimalGroup { get; set; }
    }
    public class CreateAnimalGroupRequestHandler(AnimalGroupRepository animalGroupRepository) : IRequestHandler<CreateAnimalGroupRequest>
    {
        private readonly AnimalGroupRepository _animalGroupRepository = animalGroupRepository;

        public async Task Handle(CreateAnimalGroupRequest request, CancellationToken cancellationToken)
        {
            await _animalGroupRepository.CreateAsync(request.AnimalGroup);
        }
    }
}
