using System.Text.Json.Serialization;

namespace SimuladoVeterinarioSaep.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public int TutorId { get; set; }

        [JsonIgnore]
        public Tutor? Tutor { get; set; }
    }

}