using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Group
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("datecreated")]
        public DateTime DateCreated { get; set; }
    }
}
