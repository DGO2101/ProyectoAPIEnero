using MagicVill.Datos;
using MagicVill.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVill.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly _villaRepo _db;
        internal DbSet<T> dbset;
        private _villaRepo db;

        public Repositorio(_villaRepo db)
        {
            _db = db;
            this.dbset = _db.Set<T>(); // Asigna el DbSet de la base de datos
        }


        public async Task Crear(T entidad)
        {
            //throw new NotImplementedException();
            await dbset.AddAsync(entidad);
            await Gravar();
        }

        public async Task Gravar()
        {
            await _db.SaveChangesAsync();
        }

        public Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;
            if (!tracked) { query = query.AsNoTracking(); }
            if (filtro != null) { query = query.Where(filtro); }
            return query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbset;
            if (filtro != null) { query = query.Where(filtro); }
            return await query.ToListAsync();
        }

        public async Task Remover(T entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException(nameof(entidad));
            }

            try
            {
                dbset.Remove(entidad);
                await Gravar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar eliminar entidad: {ex.Message}");
                throw; // Lanza la excepción nuevamente después de imprimir el mensaje.
            }
        }

    }
}
