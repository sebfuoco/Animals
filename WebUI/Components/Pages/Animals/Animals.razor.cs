using Application.Dtos;
using Domain.Entities;
using System.Text;
using System.Text.Json;

namespace WebUI.Components.Pages.Animals;
public partial class Animals
{
    private List<AnimalDto> _animals = new();
    private List<GroupDto> _groups = new();
    private AnimalDto _animal = new()
    {
        DateOfBirth = DateTime.Now
    };
    private Group _group = new();
    private Group _group2 = new();
    private bool _hasLoaded;
    private string _error = string.Empty;

    private static HttpClient _client = new()
    {
        BaseAddress = new Uri("https://localhost:7032/api/")
    };

    protected override async Task OnInitializedAsync()
    {
        await GetAnimals();
        await GetGroups();
        _hasLoaded = true;
    }

    private async Task CreateAnimal()
    {
        if (_animal.Tag?.Length != 15)
        {
            _error = "tag must be 15 characters";
            return;
        }
        AnimalDto animalDto = new()
        {
            Tag = _animal.Tag,
            DateOfBirth = DateTime.Now
        };
        if (_group.Id != Guid.Empty)
        {
            AnimalGroupDto animalGroup = new()
            {
                Group = new()
                {
                    Id = _group.Id
                }
            };
            animalDto.AnimalGroups ??= new List<AnimalGroupDto>();
            animalDto.AnimalGroups?.Add(animalGroup);
        }
        if (_group2.Id != Guid.Empty)
        {
            AnimalGroupDto animalGroup = new()
            {
                Group = new()
                {
                    Id = _group2.Id
                }
            };
            animalDto.AnimalGroups ??= new List<AnimalGroupDto>();
            animalDto.AnimalGroups?.Add(animalGroup);
        }
        string json = JsonSerializer.Serialize(animalDto);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("animals/create", content);
        if (response.IsSuccessStatusCode)
        {
            StateHasChanged();
            await GetAnimals();
            _error = string.Empty;
        }
    }

    private async Task GetAnimals()
    {
        HttpResponseMessage response = await _client.GetAsync("animals/getall");
        if (response.IsSuccessStatusCode)
        {
            string inputJson = response.Content.ReadAsStringAsync().Result;
            _animals = JsonSerializer.Deserialize<List<AnimalDto>>(inputJson);
            foreach (AnimalDto animal in _animals)
            {
                MinimalAnimalDto animalId = new()
                {
                    Id = animal.Id
                };
                string inputJson2 = JsonSerializer.Serialize(animalId);
                StringContent content = new(inputJson2, Encoding.UTF8, "application/json");
                HttpResponseMessage response2 = await _client.PostAsync("animalgroups/get-animalgroup", content);
                if (response.IsSuccessStatusCode)
                {
                    string json2 = response2.Content.ReadAsStringAsync().Result;
                    List<string> groupNames = JsonSerializer.Deserialize<List<string>>(json2);
                    animal.AnimalGroups ??= new List<AnimalGroupDto>();

                    foreach (string groupName in groupNames)
                    {
                        AnimalGroupDto animalGroup = new()
                        {
                            Group = new()
                            {
                                Name = groupName
                            }
                        };
                        animal.AnimalGroups.Add(animalGroup);
                    }
                }
            }
        }
    }

    private async Task GetGroups()
    {
        HttpResponseMessage response = await _client.GetAsync("groups/getall");
        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            _groups = JsonSerializer.Deserialize<List<GroupDto>>(json);
        }
    }
}
