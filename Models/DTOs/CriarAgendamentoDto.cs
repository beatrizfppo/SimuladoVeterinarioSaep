namespace SimuladoVeterinarioSaep.Models.DTOs
{
    public class CriarAgendamentoDto
    {
        public DateTime DataHora { get; set; }
        public int TutorId { get; set; }
        public int PetId { get; set; }

    }
}