using ACTV_02_PII.Data.Implementations;
using ACTV_02_PII.Data.Interfaces;
using ACTV_02_PII.Models;

namespace ACTV_02_PII.SERVICES
{
    public class ArticuloService : IAplicacion
    {
        private IArticuloRepository _repository;

        public ArticuloService()
        {
            _repository = new ArticuloRepositoryADO();
        }

        public bool DeleteArticulo(int id)
        {
            return _repository.DeleteArticulo(id);
        }

        public List<Articulo> GetAll()
        {
            return _repository.GetAll();
        }

        public Articulo GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool UpsertArticulo(Articulo articulo)
        {
            return _repository.UpsertArticulo(articulo);
        }
    }
}
