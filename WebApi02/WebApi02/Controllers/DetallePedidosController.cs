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
    public class DetallePedidosController : BaseController<DetallePedido>
    {
        private IRepository<DetallePedido> repository;

        public DetallePedidosController(IRepository<DetallePedido> _repository) : base(_repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult ListarDetallePedido()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult MostrarSoloDetallePedido(Guid id)
        {
            return Ok(repository.GetByIdAsync(id));
        }

        [HttpPost]
        public IActionResult InsertarDetallePedido(Guid id)
        {
            return Ok(repository.InsertAsync(id));
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarDetallePedido(Guid id)
        {
            return Ok(repository.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarDetallePedido(Guid id)
        {
            return Ok(repository.UpdateAsync(id));
        }
    }
}
