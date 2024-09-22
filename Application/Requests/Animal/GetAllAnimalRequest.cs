using Domain.Interfaces;
using MediatR;
using AnimalObj = Domain.Entities.Animal;

namespace Application.Requests.Animal
{
    public class GetAllAnimalRequest() : IRequest<List<AnimalObj>>;
    public class GetAllAnimalRequestHandler(IAnimalRepository animalRepo) : IRequestHandler<GetAllAnimalRequest, List<AnimalObj>>
    {
        private readonly IAnimalRepository _animalRepo = animalRepo;
        public async Task<List<AnimalObj>> Handle(GetAllAnimalRequest request, CancellationToken cancellationToken)
        {
            return _animalRepo.GetAll();
        }
    }
}
