using Application.Dtos;
using Application.Requests.AnimalGroup;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalGroupsController : BaseApiController
    {
        [HttpPost("getall")]
        public async Task<List<AnimalDto>> GetAllAnimalByAnimalGroupId(MinimalGroupDto group)
        {
            GetAllAnimalByAnimalGroupIdRequest request = new()
            {
                Id = group.Id
            };
            return await Mediator.Send(request);
        }

        [HttpPost("get")]
        public async Task<List<string>> GetAnimalGroup(MinimalAnimalDto animal)
        {
            GetAnimalGroupRequest request = new()
            {
                Id = animal.Id
            };
            return await Mediator.Send(request);
        }

        [HttpPost("delete")]
        public async Task DeleteAnimalGroup(MinimalAnimalGroupDto animalGroup)
        {
            DeleteAnimalGroupRequest request = new()
            {
                AnimalGroup = animalGroup
            };
            await Mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task CreateAnimalGroup(MinimalAnimalGroupDto animalGroupDto)
        {
            AnimalGroups animalGroups = new()
            {
                AnimalId = animalGroupDto.AnimalId,
                GroupId = animalGroupDto.GroupId
            };
            CreateAnimalGroupRequest request = new()
            {
                AnimalGroup = animalGroups
            };
            await Mediator.Send(request);
        }
    }
}
