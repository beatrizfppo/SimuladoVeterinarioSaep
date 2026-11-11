using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoVeterinarioSaep.Data;
using SimuladoVeterinarioSaep.Models;
using SimuladoVeterinarioSaep.Models.DTOs;

namespace SimuladoVeterinarioSaep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/agendamentos
        [HttpPost]
        public async Task<ActionResult<Agendamento>> CreateAgendamento(CriarAgendamentoDto agendamentoDto)
        {
            var agendamento = new Agendamento
            {
                DataHora = agendamentoDto.DataHora,
                PetId = agendamentoDto.PetId,
            };

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return Ok(agendamento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfoAgendamentoDto>>> GetAgendamentos()
        {
            var agendamentos = await _context.Agendamentos
                .Include(a => a.Pet)
                .ThenInclude(p => p.Tutor)
                .Select(a => new InfoAgendamentoDto
                {
                    Id = a.Id,
                    DataHora = a.DataHora,
                    NomePet = a.Pet.Nome,
                    RacaPet = a.Pet.Raca,
                    NomeTutor = a.Pet.Tutor.Nome,
                    CpfTutor = a.Pet.Tutor.Cpf
                })
                .ToListAsync();

            return Ok(agendamentos);
        }

        // GET: api/agendamentos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoAgendamentoDto>> GetAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos
                .Include(a => a.Pet)
                .ThenInclude(p => p.Tutor)
                .Where(a => a.Id == id)
                .Select(a => new InfoAgendamentoDto
                 {
                     Id = a.Id,
                     DataHora = a.DataHora,
                     NomePet = a.Pet.Nome,
                     RacaPet = a.Pet.Raca,
                     NomeTutor = a.Pet.Tutor.Nome,
                     CpfTutor = a.Pet.Tutor.Cpf
                 })
                .FirstOrDefaultAsync();

                    if (agendamento == null)
                    {
                        return NotFound();
                    }

                    return Ok(agendamento);
                }

        //PUT: api/agendamentos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgendamento(int id, AtualizarAgendamentoDto agendamentoDto)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            agendamento.DataHora = agendamentoDto.DataHora;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/agendamentos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}