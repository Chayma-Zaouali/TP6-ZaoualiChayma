using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;
using SchoolAPI.Repositories;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsRepoController : ControllerBase
    {
        private readonly IUniversityRepository _repository;

        public SchoolsRepoController(IUniversityRepository repository)
        {
            _repository = repository;
        }

        // GET: api/SchoolsRepo/list-schools
        [HttpGet("list-schools")]
        public ActionResult<IEnumerable<School>> ListSchools()
        {
            var schools = _repository.GetSchools();
            return Ok(schools);
        }

        // GET: api/SchoolsRepo/get-school-by-id/5
        [HttpGet("get-school-by-id/{id}")]
        public ActionResult<School> GetSchool(int id)
        {
            var school = _repository.GetSchoolById(id);
            if (school == null)
                return NotFound();

            return Ok(school);
        }

        // POST: api/SchoolsRepo/add-school
        [HttpPost("add-school")]
        public IActionResult AddSchool(School school)
        {
            _repository.AddSchool(school);
            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, school);
        }

        // PUT: api/SchoolsRepo/edit-school/5
        [HttpPut("edit-school/{id}")]
        public IActionResult UpdateSchool(int id, School school)
        {
            var existing = _repository.GetSchoolById(id);
            if (existing == null)
                return NotFound();

            // Met à jour les champs (id conservé)
            existing.Name = school.Name;
            existing.Sections = school.Sections;
            existing.Director = school.Director;
            existing.Rating = school.Rating;
            existing.WebSite = school.WebSite;

            _repository.UpdateSchool(existing);
            return NoContent();
        }

        // DELETE: api/SchoolsRepo/delete-school/5
        [HttpDelete("delete-school/{id}")]
        public IActionResult DeleteSchool(int id)
        {
            var existing = _repository.GetSchoolById(id);
            if (existing == null)
                return NotFound();

            _repository.DeleteSchool(id);
            return NoContent();
        }

        // GET: api/SchoolsRepo/search-by-name/{name}
        [HttpGet("search-by-name/{name}")]
        public ActionResult<IEnumerable<School>> SearchByName(string name)
        {
            var results = _repository.GetSchoolsByName(name);
            return Ok(results);
        }
    }
}
