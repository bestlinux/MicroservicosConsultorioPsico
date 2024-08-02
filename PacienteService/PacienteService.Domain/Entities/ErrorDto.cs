using System.Text.Json.Serialization;

namespace PacienteService.Domain.Entities
{
    public class ErrorDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("errors")]
        public List<ErrorItem> Errors { get; set; } = new();
    }
}
