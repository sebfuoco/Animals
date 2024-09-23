using Application.Dtos;
using Application.Requests.Animal;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController: BaseApiController
    {
        [HttpGet("getall")]
        public async Task<List<AnimalDto>> GetAllAnimals()
        {
            return await Mediator.Send(new GetAllAnimalRequest());
        }

        [HttpPost("get")]
        public async Task<AnimalDto> GetAnimal(MinimalAnimalDto animalParam)
        {
            GetAnimalByIdRequest request = new()
            {
                Id = animalParam.Id
            };
            return await Mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task CreateAnimals(AnimalDto animalDto)
        {
            CreateAnimalRequest request = new()
            {
                AnimalDto = animalDto
            };
            await Mediator.Send(request);
        }

        [HttpPost("update")]
        public async Task UpdateAnimals(AnimalDto animalDto)
        {
            UpdateAnimalRequest request = new()
            {
                AnimalDto = animalDto
            };
            await Mediator.Send(request);
        }
    }
}
