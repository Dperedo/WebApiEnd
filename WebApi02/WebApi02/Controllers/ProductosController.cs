using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi02.Model;
using WebApi02.Repository;

namespace WebApi02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : BaseController<Producto>
    {
        private IRepository<Producto> repository;

        public ProductosController(IRepository<Producto> _repository) : base(_repository)
        {
            repository = _repository;
        }


        [HttpGet]
        public IActionResult ListarProducto()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult MostrarSoloProducto(Guid id)
        {
            return Ok(repository.GetByIdAsync(id));
        }

        [HttpPost]
        public IActionResult InsertarProducto(Guid id)
        {
            return Ok(repository.InsertAsync(id));
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(Guid id)
        {
            return Ok(repository.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(Guid id)
        {
            return Ok(repository.UpdateAsync(id));
        }
    }
}
