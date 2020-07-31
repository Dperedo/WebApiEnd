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

    }
}
