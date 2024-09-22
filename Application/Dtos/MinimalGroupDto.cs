using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class MinimalGroupDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
