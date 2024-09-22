using Application.Dtos;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DataContext _dbContext;

        public AnimalsController(IMediator mediator, DataContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [HttpGet("getall")]
        public async Task<List<AnimalDto>> GetAllAnimals()
        {
            //return await _mediator.Send(new GetAllAnimalRequest());
            List<Animal> animals = await _dbContext.Animals.ToListAsync();
            List<AnimalDto> animalsDto = new();
            foreach (Animal animal in animals)
            {
                AnimalDto animalDto = new()
                {
                    Id = animal.Id,
                    Tag = animal.Tag,
                    DateOfBirth = animal.DateOfBirth,
                };

                animalsDto.Add(animalDto);
            }
            return animalsDto;
        }

        [HttpPost("get")]
        public async Task<AnimalDto> GetAnimal(MinimalAnimalDto animalParam)
        {
            Animal animal = await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == animalParam.Id);
            AnimalDto animalDto = new()
            {
                Id = animal.Id,
                Tag = animal.Tag,
                DateOfBirth = animal.DateOfBirth,
            };
            return animalDto;
        }

        [HttpPost("create")]
        public async Task CreateAnimals(AnimalDto animalDto)
        {
            List<string> animalTags = await _dbContext.Animals.Select(x => x.Tag).ToListAsync();
            if (!animalTags.Contains(animalDto.Tag))
            {
                Animal createAnimal = new()
                {
                    Id = Guid.NewGuid(),
                    Tag = animalDto.Tag,
                    DateOfBirth = DateTime.Now,
                };
                if (animalDto.AnimalGroups is not null)
                {
                    foreach (AnimalGroupDto animalGroupDto in animalDto.AnimalGroups)
                    {
                        AnimalGroups animalGroup = new()
                        {
                            Id = Guid.NewGuid(),
                            AnimalId = createAnimal.Id,
                            GroupId = animalGroupDto.Group.Id
                        };
                        createAnimal.AnimalGroups ??= new List<AnimalGroups>();
                        createAnimal.AnimalGroups.Add(animalGroup);
                    }
                }

                await _dbContext.Animals.AddAsync(createAnimal);
                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPost("update")]
        public async Task UpdateAnimals(AnimalDto animalDto)
        {
            Animal animal = await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == animalDto.Id);
            List<string> animalTags = await _dbContext.Animals.Select(x => x.Tag).ToListAsync();

            if (!animalTags.Contains(animalDto.Tag))
            {
                animal.Tag = animalDto.Tag;
                animal.DateOfBirth = animalDto.DateOfBirth;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
