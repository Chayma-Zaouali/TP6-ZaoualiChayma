using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;

namespace SchoolAPI.Repositories
{
    public class SchoolRepository : IUniversityRepository
    {
        private readonly SchoolDbContext _context;

        // 🔹 Injection du DbContext dans le constructeur
        public SchoolRepository(SchoolDbContext context)
        {
            _context = context;
        }

        // 🔹 Implémentation des méthodes de l’interface
        public IEnumerable<School> GetSchools()
        {
            return _context.Schools.ToList();
        }

        public School GetSchoolById(int id)
        {
            return _context.Schools.Find(id);
        }

        public IEnumerable<School> GetSchoolsByName(string name)
        {
            return _context.Schools
                .Where(s => s.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public void AddSchool(School school)
        {
            _context.Schools.Add(school);
            _context.SaveChanges();
        }

        public void UpdateSchool(School school)
        {
            _context.Entry(school).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSchool(int id)
        {
            var school = _context.Schools.Find(id);
            if (school != null)
            {
                _context.Schools.Remove(school);
                _context.SaveChanges();
            }
        }
    }
}
