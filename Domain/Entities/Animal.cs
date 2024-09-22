using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Animal
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [MaxLength(15)]
        [MinLength(15)]
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = default!;
        [JsonPropertyName("dateofbirth")]
        public DateTime DateOfBirth { get; set; }
        public List<Guid>? AnimalGroupsId { get; set; }
        public virtual ICollection<AnimalGroups>? AnimalGroups { get; set; }
    }
}
