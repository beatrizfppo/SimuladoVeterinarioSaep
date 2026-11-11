using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoVeterinarioSaep.Data;
using SimuladoVeterinarioSaep.Models;
using SimuladoVeterinarioSaep.Models.DTOs;

namespace SimuladoVeterinarioSaep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TutoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tutores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetTutores()
        {
            return await _context.Tutores.ToListAsync();
            //return await _context.Tutores.Include(v => v.Aluno).Select(v => new TutorDto
            //{
            //    Nome = v.Nome,
            //    Cpf = v.Cpf,
            //    NomeAluno = v.Aluno.Nome
            //}).ToListAsync();
        }

        // GET: api/tutores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TutorDto>> GetTutor(int id)
        {
            var tutor = await _context.Tutores.FirstOrDefaultAsync(p => p.Id == id);

            if (tutor == null)
            {
                return NotFound();
            }

            return new TutorDto
            {
                Nome = tutor.Nome,
                Cpf = tutor.Cpf,
                Telefone = tutor.Telefone
            };
        }

        // POST: api/tutores
        [HttpPost]
        public async Task<ActionResult<Tutor>> CreateTutor(CriarTutorDto criarTutorDto)
        {
            var tutor = new Tutor
            {
                Nome = criarTutorDto.Nome,
                Cpf = criarTutorDto.Cpf,
                Telefone = criarTutorDto.Telefone
            };

            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTutor), new { id = tutor.Id }, tutor);
        }

        // PUT: api/tutores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTutor(int id, CriarTutorDto criarTutorDto)
        {
            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            tutor.Nome = criarTutorDto.Nome;
            tutor.Cpf = criarTutorDto.Cpf;
            tutor.Telefone = criarTutorDto.Telefone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/tutores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            _context.Tutores.Remove(tutor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}