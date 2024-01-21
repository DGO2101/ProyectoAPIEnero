using MagicVill.Datos;
using MagicVill.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace MagicVill.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly AplicationDBContext _dbContext;//se inyecta el DBContext para trabajar desde ahí y no desde el controlador
        internal DbSet<T> dbset;
        public Repositorio(AplicationDBContext db)
        {
            _dbContext = db;
            this.dbset = _dbContext.Set<T>();
        }
        public async Task crear(T entidad)
        {
            await dbset.AddAsync(entidad);
            await Gravar();
        }

        public async Task Gravar()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task<T> Gravar(T entidad)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;
            if(!tracked) { query.AsNoTracking(); }
            if(filtro != null) {  query = query.Where(filtro); }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbset;
            if (filtro != null) { query = query.Where(filtro); }
            return await query.ToListAsync();
        }

        public async Task Remove(T entidad)
        {
            dbset.Remove(entidad);
            await Gravar();
        }

        Task<T> IRepositorio<T>.Remove(T entidad)
        {
            throw new NotImplementedException();
        }
    }
}
