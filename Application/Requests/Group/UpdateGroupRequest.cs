using Application.Dtos;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.Group
{
    public class UpdateGroupRequest() : IRequest
    {
        public required GroupDto Group { get; set; }
    }
    public class UpdateGroupRequestHandler(GroupRepository groupRepository) : IRequestHandler<UpdateGroupRequest>
    {
        private readonly GroupRepository _groupRepository = groupRepository;
        public async Task Handle(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Group group = await _groupRepository.GetByIdAsync(request.Group.Id);
            List<string> groupNames = await _groupRepository.GetAllByNameAsync(request.Group.Name);

            if (groupNames.Count == 0)
            {
                group.Name = request.Group.Name;
                await _groupRepository.UpdateAsync();
            }
        }
    }
}
