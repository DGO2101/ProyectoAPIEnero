using MagicVill.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVill.Datos
{
    public class _villaRepo: DbContext 
    {
        public _villaRepo(DbContextOptions<_villaRepo> options): base(options)
        {
            //
        }
        public DbSet<Vill> villa {  get; set; }
    }
}
