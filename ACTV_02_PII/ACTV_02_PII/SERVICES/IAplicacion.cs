using ACTV_02_PII.Models;

namespace ACTV_02_PII.SERVICES
{
    public interface IAplicacion
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool DeleteArticulo(int id);
        bool UpsertArticulo(Articulo articulo);
    }
}
