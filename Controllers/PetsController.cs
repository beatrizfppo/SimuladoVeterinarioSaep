using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoVeterinarioSaep.Data;
using SimuladoVeterinarioSaep.Models;
using SimuladoVeterinarioSaep.Models.DTOs;

namespace SimuladoVeterinarioSaep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetPets()
        {
            return await _context.Pets.Select(p => new PetDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Raca = p.Raca,
                TutorId = p.TutorId
            }).ToListAsync();
        }

        // GET: api/pets/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return new PetDto
            {
                Id = pet.Id,
                Nome = pet.Nome,
                Raca = pet.Raca,
                TutorId = pet.TutorId
            };
        }

        // POST: api/pets
        [HttpPost]
        public async Task<ActionResult<PetDto>> CreatePet(PetDto petDto)
        {

            var tutorExiste = await _context.Tutores.AnyAsync(t => t.Id == petDto.TutorId);

            if (!tutorExiste)
            {
                return BadRequest(new { mensagem = $"Não existe nenhum tutor cadastrado com o ID {petDto.TutorId}." });
            }

            var pet = new Pet
            {
                Nome = petDto.Nome,
                Raca = petDto.Raca,
                TutorId = petDto.TutorId
            };

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            petDto.Id = pet.Id;

            return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, petDto);
        }

        //PUT: api/pets/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, PetDto petDto)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            pet.Nome = petDto.Nome;
            pet.Raca = petDto.Raca;
            pet.TutorId = petDto.TutorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/pets/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}