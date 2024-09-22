using Application.Dtos;
using Application.Requests.Group;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        //private readonly IMediator _mediator = mediator;
        private readonly DataContext _dbContext;

        public GroupsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getall")]
        public async Task<List<GroupDto>> GetAllGroups()
        {
            //return await _mediator.Send(request, cancellationToken);
            List<Group> groups = await _dbContext.Groups.AsNoTracking().ToListAsync();
            List<GroupDto> groupDtos = new();
            foreach (Group group in groups)
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

        [HttpPost("get")]
        public async Task<GroupDto> GetGroup(MinimalGroupDto groupParam)
        {
            //return await _mediator.Send(request, cancellationToken);
            Group group = await _dbContext.Groups.AsNoTracking().Where(x => x.Id == groupParam.Id).FirstOrDefaultAsync();
            GroupDto groupDto = new()
            {
                Id = group.Id,
                Name = group.Name,
                DateCreated = group.DateCreated,
            };
            return groupDto;
        }

        [HttpPost("create")]
        public async Task CreateGroup(GroupDto group)
        {
            //await _mediator.Send(request, cancellationToken);
            List<string> groupNames = await _dbContext.Groups.Select(x => x.Name).Where(x => x == group.Name).ToListAsync();
            if (groupNames.Count == 0)
            {
                Group createGroup = new()
                {
                    Id = Guid.NewGuid(),
                    Name = group.Name,
                    DateCreated = DateTime.Now
                };
                await _dbContext.Groups.AddAsync(createGroup);
                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPost("update")]
        public async Task UpdateGroup(GroupDto groupDto)
        {
            Group group = await _dbContext.Groups.Where(x => x.Id == groupDto.Id).FirstOrDefaultAsync();
            List<string> groupNames = await _dbContext.Groups.Select(x => x.Name).Where(x => x == groupDto.Name).ToListAsync();

            if (groupNames.Count == 0)
            {
                group.Name = groupDto.Name;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
