using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using SchoolAPI.DTOs;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;

        public SchoolsController(SchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET ALL — retourne une liste de DTOs
        [HttpGet("list-schools")]
        public ActionResult<IEnumerable<SchoolDto>> ListSchools()
        {
            var list = _context.Schools
                .Select(s => _mapper.Map<SchoolDto>(s))
                .ToList();
            return Ok(list);
        }

        // GET BY ID — retourne un DTO
        [HttpGet("get-school-by-id/{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
                return NotFound();

            var schoolDto = _mapper.Map<SchoolDto>(school);
            return Ok(schoolDto);
        }

        // CREATE — à partir d'un DTO
        [HttpPost("add-school")]
        public async Task<ActionResult<SchoolDto>> PostSchool(SchoolDto dto)
        {
            var school = _mapper.Map<School>(dto);
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<SchoolDto>(school);
            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, resultDto);
        }

        // UPDATE — avec un DTO
        [HttpPut("edit-school/{id}")]
        public async Task<IActionResult> PutSchool(int id, SchoolDto dto)
        {
            var existingSchool = await _context.Schools.FindAsync(id);
            if (existingSchool == null)
                return NotFound();

            _mapper.Map(dto, existingSchool); // met à jour seulement les champs mappés
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE — pas besoin de DTO ici
        [HttpDelete("delete-school/{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
                return NotFound();

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // SEARCH — retourne liste de DTOs
        [HttpGet("search-by-name/{name}")]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> SearchByName(string name)
        {
            var result = await _context.Schools
                .Where(s => s.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return Ok(_mapper.Map<List<SchoolDto>>(result));
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.Id == id);
        }
    }
}
