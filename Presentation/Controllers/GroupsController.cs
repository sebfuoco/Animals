using Application.Dtos;
using Application.Requests.Animal;
using Application.Requests.Group;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<List<GroupDto>> GetAllGroups()
        {
            return await Mediator.Send(new GetAllGroupRequest());
        }

        [HttpPost("get")]
        public async Task<GroupDto> GetGroup(MinimalGroupDto group)
        {
            GetGroupByIdRequest request = new()
            {
                Id = group.Id
            };
            return await Mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task CreateGroup(GroupDto group)
        {
            CreateGroupRequest request = new()
            {
                Name = group.Name
            };
            await Mediator.Send(request);
        }

        [HttpPost("update")]
        public async Task UpdateGroup(GroupDto group)
        {
            UpdateGroupRequest request = new()
            {
                Group = group
            };
            await Mediator.Send(request);
        }
    }
}
