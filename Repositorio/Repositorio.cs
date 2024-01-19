using MagicVill.Datos;
using MagicVill.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVill.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly AplicationDBContext _dbContext;
        internal DbSet<T> dbset;
        public Task crear(T entidad)
        {
            throw new NotImplementedException();
        }

        public Task<T> Gravar(T entidad)
        {
            throw new NotImplementedException();
        }

        public Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> Remove(T entidad)
        {
            throw new NotImplementedException();
        }
    }
}
