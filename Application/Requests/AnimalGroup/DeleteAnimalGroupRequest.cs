using Application.Dtos;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.AnimalGroup
{
    public class DeleteAnimalGroupRequest : IRequest
    {
        public MinimalAnimalGroupDto AnimalGroup { get; set; }
    }
    public class DeleteAnimalGroupRequestHandler(AnimalGroupRepository animalGroupRepository) : IRequestHandler<DeleteAnimalGroupRequest>
    {
        private readonly AnimalGroupRepository _animalGroupRepository = animalGroupRepository;

        public async Task Handle(DeleteAnimalGroupRequest request, CancellationToken cancellationToken)
        {
            AnimalGroups? animalGroup = await _animalGroupRepository.GetAsync(request.AnimalGroup.AnimalId, request.AnimalGroup.GroupId);
            await _animalGroupRepository.DeleteAsync(animalGroup);
        }
    }
}
