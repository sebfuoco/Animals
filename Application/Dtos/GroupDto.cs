using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class GroupDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("datecreated")]
        public DateTime DateCreated { get; set; }
    }
}
