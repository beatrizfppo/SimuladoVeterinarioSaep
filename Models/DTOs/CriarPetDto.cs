namespace SimuladoVeterinarioSaep.Models.DTOs
{
    public class CriarPetDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public int TutorId { get; set; }
    }
}
