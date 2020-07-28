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
    public class PedidosController : BaseController<Pedido>
    {
        private IRepository<Pedido> repository;

        public PedidosController(IRepository<Pedido> _repository) : base(_repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult ListarPedido()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult MostrarSoloPedido(Guid id)
        {
            return Ok(repository.GetByIdAsync(id));
        }

        [HttpPost]
        public IActionResult InsertarPedido(Guid id)
        {
            return Ok(repository.InsertAsync(id));
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPedido(Guid id)
        {
            return Ok(repository.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarPedido(Guid id)
        {
            return Ok(repository.UpdateAsync(id));
        }
    }
}
