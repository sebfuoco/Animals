using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.AnimalGroup
{
    public class GetAnimalGroupRequest : IRequest<List<string>>
    {
        public Guid Id { get; set; }
    }

    public class GetAnimalGroupRequestHandler(AnimalGroupRepository animalGroupRepository, GroupRepository groupRepository) : IRequestHandler<GetAnimalGroupRequest, List<string>>
    {
        private readonly AnimalGroupRepository _animalGroupRepository = animalGroupRepository;
        private readonly GroupRepository _groupRepository = groupRepository;

        public async Task<List<string>> Handle(GetAnimalGroupRequest request, CancellationToken cancellationToken)
        {
            List<AnimalGroups> animalGroups = await _animalGroupRepository.GetAllByAnimalIdAsync(request.Id);
            List<string> groups = new();

            foreach (AnimalGroups animalGroup in animalGroups)
            {
                Domain.Entities.Group? group = await _groupRepository.GetAsync(animalGroup.GroupId);
                groups.Add(group.Name);
            }
            return groups;
        }
    }
}
