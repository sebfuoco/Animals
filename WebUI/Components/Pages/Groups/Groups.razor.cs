using Application.Dtos;
using System.Text;
using System.Text.Json;

namespace WebUI.Components.Pages.Groups;
public partial class Groups
{
    private List<GroupDto> _groups = new();
    private GroupDto _group = new();
    private bool _hasLoaded;
    private static HttpClient _client = new()
    {
        BaseAddress = new Uri("https://localhost:7032/api/groups/")
    };

    protected override async Task OnInitializedAsync()
    {
        await GetGroups();
        _hasLoaded = true;
    }

    private async Task CreateGroup()
    {
        string json = JsonSerializer.Serialize(_group);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("create", content);
        if (response.IsSuccessStatusCode)
        {
            StateHasChanged();
            await GetGroups();
        }
    }

    private async Task GetGroups()
    {
        HttpResponseMessage response = await _client.GetAsync("getall");
        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            _groups = JsonSerializer.Deserialize<List<GroupDto>>(json);
        }
    }
}
