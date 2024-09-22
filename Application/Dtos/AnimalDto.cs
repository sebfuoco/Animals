using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class AnimalDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [MaxLength(15)]
        [MinLength(15)]
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = default!;
        [JsonPropertyName("dateofbirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonPropertyName("animalgroups")]
        public List<AnimalGroupDto>? AnimalGroups { get; set; }
    }

    public class AnimalGroupDto
    {
        [JsonPropertyName("groups")]
        public GroupDto Group { get; set; } = default!;
    }
}
