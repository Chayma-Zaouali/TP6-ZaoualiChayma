using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Models
{
    public class SchoolDbContext : DbContext
    {
        // Constructeur qui appelle celui de la super-classe DbContext
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>().HasData(
                new School
                {
                    Id = 1,
                    Name = "ENISo",
                    Sections = "IA, GTE, GMP",
                    Director = "Ali Mouelhi",
                    Rating = 3.5,
                    WebSite = "http://www.eniso.rnu.tn"
                },
                new School
                {
                    Id = 2,
                    Name = "ENIM",
                    Sections = "Mécanique, Électrique, Énergétique",
                    Director = "Sana Khemiri",
                    Rating = 4.2,
                    WebSite = "http://www.enim.rnu.tn"
                },
                new School
                {
                    Id = 3,
                    Name = "INSAT",
                    Sections = "Informatique, Télécommunications, Automatique",
                    Director = "Rania Ben Slimane",
                    Rating = 4.7
                    // Pas de WebSite → ne pas inclure la propriété car elle est nullable
                }
            );
        }


        // DbSet pour représenter la table des écoles
        public DbSet<School> Schools { get; set; }
    }
}
