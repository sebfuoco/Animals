using Domain.Interfaces;
using MediatR;
using GroupObj = Domain.Entities.Group;

namespace Application.Requests.Animal
{
    public class GetAllGroupRequest() : IRequest<List<GroupObj>>;

    public class GetAllGroupRequestHandler(IGroupRepository groupRepo) : IRequestHandler<GetAllGroupRequest, List<GroupObj>>
    {
        private readonly IGroupRepository _groupRepo = groupRepo;

        public async Task<List<GroupObj>> Handle(GetAllGroupRequest request, CancellationToken cancellationToken)
        {
           return _groupRepo.GetAll();
        }
    }
}
