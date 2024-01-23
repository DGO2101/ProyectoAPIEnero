using MagicVill.Modelos;

namespace MagicVill.Repositorio.IRepositorio
{
    public interface IVillaRepositorio: IRepositorio<Vill>
    {
        Task<Vill> Actualizar(Vill entidad);
    }
}
