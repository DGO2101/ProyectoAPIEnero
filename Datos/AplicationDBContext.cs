using MagicVill.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVill.Datos
{
    public class AplicationDBContext: DbContext 
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options): base(options)
        {
            //
        }
        public DbSet<Vill> villa {  get; set; }
    }
}
