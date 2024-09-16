using ACTV_02_PII.Data.Implementations;
using ACTV_02_PII.Data.Interfaces;
using ACTV_02_PII.Models;
using ACTV_02_PII.SERVICES;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ACTV_02_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IAplicacion _aplicacion;

        public ArticuloController()
        {
            _aplicacion = new ArticuloService();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
           return Ok(_aplicacion.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorID(int id)
        {
            return Ok(_aplicacion.GetById(id));
        }

        [HttpPost]
        public IActionResult UpSertArticulo(Articulo articulo)
        {
            bool result = _aplicacion.UpsertArticulo(articulo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArticulo(int id)
        {
            bool res = _aplicacion.DeleteArticulo(id);
            return Ok(res);
        }
    }
}
