using MagicVill.Datos;
using MagicVill.Modelos;
using MagicVill.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MagicVill.Repositorio
{
    public class VillaRepositorio : Repositorio<Vill>, IVillaRepositorio
    {
        private readonly _villaRepo _db;
        public VillaRepositorio(_villaRepo db): base(db)
        {
            _db = db;
        }

        public async Task<Vill> Actualizar(Vill entidad)
        {
            entidad.fechaActualizacion = DateTime.Now;
            _db.villa.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
