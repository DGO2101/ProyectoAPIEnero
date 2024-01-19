using System.Linq.Expressions;

namespace MagicVill.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class // se hace que la interfaz sea generico, se puede recibir cualquier tipo de entidad
    {
        Task crear (T entidad);
        Task<List<T>> ObtenerTodos (Expression<Func<T, bool>>? filtro = null);
        Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true);
        Task<T> Remove(T entidad);
        Task<T> Gravar (T entidad);

        //pedirle a chatgpt que me explique bien que hace esta clase
    }
}
