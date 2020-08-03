using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi02.Model;
using WebApi02.Repository;

namespace WebApi02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidosController : BaseController<DetallePedido>
    {
        private IRepository<DetallePedido> repository;
        private IRepository<Producto> repositoryProducto;

        public DetallePedidosController(IRepository<DetallePedido> _repository,
            IRepository<Producto> _repoProducto) : base(_repository)
        {
            repository = _repository;
            repositoryProducto = _repoProducto;
        }

        [HttpGet]
        public override IActionResult Todos()
        {
            return Ok(repository.GetAll().Include(x => x.Producto));
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> MostrarSolo(Guid id)
        {
            var item = await repository.GetAll().Include(p => p.Producto).SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound("no encontrado");
            }
            return Ok(item);
        }

        [HttpPost]
        public override async Task<IActionResult> Insertar(DetallePedido entity)
        {
            var item = await repository.GetByIdAsync(entity.Id);

            if (item != null)
                return Conflict("Ya existe");

            var cliente = await repositoryProducto.GetByIdAsync(entity.Producto.Id);
            if (cliente != null)
            {
                entity.Producto = cliente;
            }
            else
                return BadRequest("Cliente no existe");

            return Ok(await repository.InsertAsync(entity));
        }

        [HttpPut]
        public override async Task<IActionResult> Actualizar(Guid id, DetallePedido entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            var item = await repository.GetNoTrackedByIdAsync(id);
            if (item == null)
            {
                return NotFound("no encontrado");
            }


            repository.Context.SaveChanges();

            return Ok("Agregado");
        }

    }
}
