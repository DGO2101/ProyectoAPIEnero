using System.Linq.Expressions;

namespace MagicVill.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class //es la interfaz genereica y podemos recibir cualquier tipo de entidad
    {
        Task Crear(T entidad);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);
        Task Remover(T entidad);
        Task Gravar();

    }
}
