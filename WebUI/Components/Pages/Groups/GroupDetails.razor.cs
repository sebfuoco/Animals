using Application.Dtos;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;

namespace WebUI.Components.Pages.Groups;
public partial class GroupDetails
{
    [Parameter]
    public Guid id { get; set; }

    private GroupDto _group;
    private List<AnimalDto> _animals;
    private bool hasLoaded;
    private static HttpClient _client = new()
    {
        BaseAddress = new Uri("https://localhost:7032/api/")
    };
    protected override async Task OnInitializedAsync()
    {
        await GetGroup();
        hasLoaded = true;
    }

    private async Task GetGroup()
    {
        MinimalGroupDto groupDto = new()
        {
            Id = id
        };
        string inputJson = JsonSerializer.Serialize(groupDto);
        StringContent content = new(inputJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("groups/get", content);
        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            _group = JsonSerializer.Deserialize<GroupDto>(json);
            await GetAnimals();
        }
    }

    private async Task GetAnimals()
    {
        MinimalGroupDto groupDto = new()
        {
            Id = id
        };
        string inputJson = JsonSerializer.Serialize(groupDto);
        StringContent content = new(inputJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("animalgroups/getall", content);
        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            _animals = JsonSerializer.Deserialize<List<AnimalDto>>(json);
        }
    }

    private async Task UpdateGroup()
    {
        string inputJson = JsonSerializer.Serialize(_group);
        StringContent content = new(inputJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("groups/update", content);
        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            _animals = JsonSerializer.Deserialize<List<AnimalDto>>(json);
        }
    }

    private async Task RemoveAnimalFromGroup(AnimalDto animal)
    {
        MinimalAnimalGroupDto animalGroup = new()
        {
            AnimalId = animal.Id,
            GroupId = _group.Id,
        };
        string inputJson = JsonSerializer.Serialize(animalGroup);
        StringContent content = new(inputJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("animalgroups/delete", content);
        if (response.IsSuccessStatusCode)
        {
            StateHasChanged();
            await GetAnimals();
        }
    }
}
