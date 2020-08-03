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
    public class PedidosController : BaseController<Pedido>
    {
        private IRepository<Pedido> repository;
        private IRepository<Cliente> repositoryCliente;
        private IRepository<Estado> repositoryEstado;
        private IRepository<DetallePedido> repositoryDetallePedido;
        private IRepository<Producto> repositoryProducto;

        public PedidosController(IRepository<Pedido> _repository, 
            IRepository<Cliente> _repoCliente,
            IRepository<Estado> _repoEstado,
            IRepository<DetallePedido> _repoDetallePedido,
            IRepository<Producto> _repoProducto) : base(_repository)
        {
            repository = _repository;
            repositoryCliente = _repoCliente;
            repositoryEstado = _repoEstado;
            repositoryProducto = _repoProducto;
        }

        [HttpGet]
        public override IActionResult Todos()
        {
            return Ok(repository.GetAll().Include(c => c.Cliente).Include(e => e.Estado).Include(d => d.DetallePedidos).ThenInclude(p =>p.Producto));
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> MostrarSolo(Guid id)
        {
            var item = await repository.GetAll().Include(c => c.Cliente).Include(e => e.Estado).Include(d => d.DetallePedidos).ThenInclude(p => p.Producto).SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound("no encontrado");
            }

            return Ok(item);
        }

        [HttpPost]
        public override async Task<IActionResult> Insertar(Pedido entity)
        {
            var item = await repository.GetByIdAsync(entity.Id);

            if(item != null)
                return Conflict("Ya existe");

            var cliente = await repositoryCliente.GetByIdAsync(entity.Cliente.Id);
            if (cliente != null)
            {
                entity.Cliente = cliente;
            }
            else
                return BadRequest("Cliente no existe");

            var estado = await repositoryEstado.GetByIdAsync(entity.Estado.Id);
            if (estado != null)
            {
                entity.Estado = estado;
            }
            else
                return BadRequest("Estado no existe");

            foreach (DetallePedido detallePedido in entity.DetallePedidos)
            {
                detallePedido.Id = Guid.Empty;

                if (detallePedido.Producto != null)
                {
                    var producto = await repositoryProducto.GetByIdAsync(detallePedido.Producto.Id);
                    if (producto != null)
                    {
                        detallePedido.Producto = producto;
                    }
                    else
                        return BadRequest("Uno de los productos de Detalle del Pedido no existe");
                }
            }

            return Ok(await repository.InsertAsync(entity));
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Eliminar(Guid id)
        {
            

            var pedido =  repository.GetAll().Include(d => d.DetallePedidos).ThenInclude(p => p.Producto).FirstOrDefault(x => x.Id == id);
            if (pedido != null)
            {
                
                repository.Context.Remove(pedido);
                for (int i=0;i<pedido.DetallePedidos.Count ;i++)
                {
                    repository.Context.Remove(pedido.DetallePedidos[i]);
                }
            }
            
            await repository.Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Actualizar(Guid id, Pedido entity)
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

            
            var cliente = await repositoryCliente.GetByIdAsync(entity.Cliente.Id);
            if (cliente != null)
            {
                entity.Cliente = cliente;
            }
            else
                return BadRequest("Cliente no existe");

            var estado = await repositoryEstado.GetByIdAsync(entity.Estado.Id);
            if (estado != null)
            {
                entity.Estado = estado;
            }
            else
                return BadRequest("Estado no existe");

            foreach(var pd in entity.DetallePedidos)
            {
                if(pd.Producto == null)
                {
                    return BadRequest("no especifica Producto");
                }

                var producto = await repositoryProducto.GetByIdAsync(pd.Producto.Id);
                if (producto != null)
                {
                    pd.Producto = producto;
                }
                else
                    return BadRequest("no existe Producto");

            }

            var pedido = repository.Context.Pedidos.Include(c => c.DetallePedidos).FirstOrDefault(g => g.Id == entity.Id);
            
            repository.Context.Entry(pedido).CurrentValues.SetValues(entity);
            
            var pedidoDetalles = pedido.DetallePedidos.ToList();
            foreach (var pedidoDetalle in pedidoDetalles)
            {
                var detalle = entity.DetallePedidos.SingleOrDefault(i => i.Id == pedidoDetalle.Id);
                if (detalle != null)
                    repository.Context.Entry(pedidoDetalle).CurrentValues.SetValues(detalle);
                else
                    repository.Context.Remove(pedidoDetalle);
            }
            
            foreach (var detalle in entity.DetallePedidos)
            {
                if (pedidoDetalles.All(i => i.Id != detalle.Id))
                {
                    pedido.DetallePedidos.Add(detalle);
                }
            }
            repository.Context.SaveChanges();



            return Ok("Agregado");
        }


    }
}
