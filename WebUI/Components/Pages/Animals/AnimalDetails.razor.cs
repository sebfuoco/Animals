using Application.Dtos;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;

namespace WebUI.Components.Pages.Animals;
public partial class AnimalDetails
{
    [Parameter]
    public Guid id { get; set; }

    private AnimalDto _animal;
    private List<GroupDto> _groups;
    private GroupDto _group = new();
    private bool hasLoaded;
    private static HttpClient _client = new()
    {
        BaseAddress = new Uri("https://localhost:7032/api/")
    };
    protected override async Task OnInitializedAsync()
    {
        await GetAnimal();
        hasLoaded = true;
    }

    private async Task GetAnimal()
    {
        MinimalAnimalDto animalDto = new()
        {
            Id = id
        };
        string inputJson = JsonSerializer.Serialize(animalDto);
        StringContent content = new(inputJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("animals/get", content);
        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            _animal = JsonSerializer.Deserialize<AnimalDto>(json);
            await GetGroups();
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

    private async Task UpdateAnimal()
    {
        string inputJson = JsonSerializer.Serialize(_animal);
        StringContent content = new(inputJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("animals/update", content);

        MinimalAnimalGroupDto animalGroup = new()
        {
            AnimalId = id,
            GroupId = _group.Id
        };

        string inputJson2 = JsonSerializer.Serialize(animalGroup);
        StringContent content2 = new(inputJson2, Encoding.UTF8, "application/json");
        HttpResponseMessage response2 = await _client.PostAsync("animalgroups/create", content2);
    }
}
