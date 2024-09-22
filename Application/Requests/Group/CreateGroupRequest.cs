using Domain.Interfaces;
using MediatR;
using GroupObj = Domain.Entities.Group;

namespace Application.Requests.Group
{
    public class CreateGroupRequest() : IRequest
    {
        public required string Name { get; set; }
    }
    public class CreateGroupRequestHandler(IGroupRepository groupRepo) : IRequestHandler<CreateGroupRequest>
    {
        private readonly IGroupRepository _groupRepo = groupRepo;

        public async Task Handle(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            GroupObj group = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                DateCreated = DateTime.Now
            };
            _groupRepo.Add(group);
        }
    }
}
