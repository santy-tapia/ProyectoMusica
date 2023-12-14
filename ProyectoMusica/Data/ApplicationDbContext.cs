using Microsoft.EntityFrameworkCore;
using ProyectoMusica.Models;

namespace ProyectoMusica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Grupo> Grupos  { get; set; }
    }
}
