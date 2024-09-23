using Application.Dtos;
using MediatR;
using Infrastructure.Repositories;

namespace Application.Requests.Animal
{
    public class GetGroupByIdRequest() : IRequest<GroupDto>
    {
        public Guid Id { get; set; }
    }

    public class GetGroupByIdRequestHandler(GroupRepository groupRepository) : IRequestHandler<GetGroupByIdRequest, GroupDto>
    {
        private readonly GroupRepository _groupRepository = groupRepository;

        public async Task<GroupDto> Handle(GetGroupByIdRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Group? group = await _groupRepository.GetAsync(request.Id);
            return new()
            {
                Id = group.Id,
                Name = group.Name,
                DateCreated = group.DateCreated,
            };
        }
    }
}
