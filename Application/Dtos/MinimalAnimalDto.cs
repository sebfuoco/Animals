using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class MinimalAnimalDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
