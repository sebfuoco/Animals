using Application.Dtos;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.Animal
{
    public class GetAllGroupRequest() : IRequest<List<GroupDto>>;

    public class GetAllGroupRequestHandler(GroupRepository groupRepository) : IRequestHandler<GetAllGroupRequest, List<GroupDto>>
    {
        private readonly GroupRepository _groupRepository = groupRepository;

        public async Task<List<GroupDto>> Handle(GetAllGroupRequest request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Group> groups = await _groupRepository.GetAllAsync();
            List<GroupDto> groupDtos = new();
            foreach (Domain.Entities.Group group in groups)
            {
                GroupDto groupDto = new()
                {
                    Id = group.Id,
                    Name = group.Name,
                    DateCreated = group.DateCreated,
                };

                groupDtos.Add(groupDto);
            }
            return groupDtos;
        }
    }
}
