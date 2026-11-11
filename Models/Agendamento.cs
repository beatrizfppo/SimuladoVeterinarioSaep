namespace SimuladoVeterinarioSaep.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
       
        public int PetId { get; set; }
        public Pet? Pet { get; set; }
    }
}