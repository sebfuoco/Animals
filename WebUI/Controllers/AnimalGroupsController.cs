using Application.Dtos;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalGroupsController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public AnimalGroupsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("getall-animalgroup")]
        public async Task<List<AnimalDto>> GetAllAnimalByAnimalGroupId(MinimalGroupDto groupDto)
        {
            List<AnimalGroups> animalGroups = await _dbContext.AnimalGroups.AsNoTracking().Where(x => x.GroupId == groupDto.Id).ToListAsync();
            List<AnimalDto> animalsDto = new();

            foreach (AnimalGroups animalGroup in animalGroups)
            {
                Animal animal = await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == animalGroup.AnimalId);
                AnimalDto animalDto = new()
                {
                    Id = animal.Id,
                    Tag = animal.Tag
                };

                animalsDto.Add(animalDto);
            }
            return animalsDto;
        }

        [HttpPost("get-animalgroup")]
        public async Task<List<string>> GetAnimalGroup(MinimalAnimalDto animalDto)
        {
            List<AnimalGroups> animalGroups = await _dbContext.AnimalGroups.AsNoTracking().Where(x => x.AnimalId == animalDto.Id).ToListAsync();
            List<string> groups = new();

            foreach (AnimalGroups animalGroup in animalGroups)
            {
                Group group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == animalGroup.GroupId);
                groups.Add(group?.Name);
            }
            return groups;
        }

        [HttpPost("delete")]
        public async Task DeleteAnimalGroup(MinimalAnimalGroupDto animalGroupDto)
        {
            AnimalGroups animalGroup = await _dbContext.AnimalGroups.FirstOrDefaultAsync(x => x.GroupId == animalGroupDto.GroupId && x.AnimalId == animalGroupDto.AnimalId);
            _dbContext.AnimalGroups.Remove(animalGroup);
            await _dbContext.SaveChangesAsync();
        }

        [HttpPost("create")]
        public async Task CreateAnimalGroup(MinimalAnimalGroupDto animalGroupDto)
        {
            AnimalGroups animalGroup = new()
            {
                AnimalId = animalGroupDto.AnimalId,
                GroupId = animalGroupDto.GroupId
            };
            _dbContext.AnimalGroups.Add(animalGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
}
