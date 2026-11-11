namespace SimuladoVeterinarioSaep.Models.DTOs
{
    public class InfoAgendamentoDto
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeTutor { get; set; }
        public string CpfTutor { get; set; }
        public string NomePet { get; set; }
        public string RacaPet { get; set; }
    }
}
