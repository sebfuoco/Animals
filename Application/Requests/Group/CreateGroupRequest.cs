using Infrastructure.Repositories;
using MediatR;

namespace Application.Requests.Group
{
    public class CreateGroupRequest() : IRequest
    {
        public required string Name { get; set; }
    }
    public class CreateGroupRequestHandler(GroupRepository groupRepository) : IRequestHandler<CreateGroupRequest>
    {
        private readonly GroupRepository _groupRepository = groupRepository;
        public async Task Handle(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            List<string> groupNames = await _groupRepository.GetAllByNameAsync(request.Name);
            if (groupNames.Count == 0)
            {
                Domain.Entities.Group group = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    DateCreated = DateTime.Now
                };
                await _groupRepository.CreateAsync(group);
            }
        }
    }
}
