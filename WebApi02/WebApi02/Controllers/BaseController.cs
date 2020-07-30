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
    public class BaseController<T> : ControllerBase where T : class, IModel
    {
        private IRepository<T> repository;

        public BaseController(IRepository<T> _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult Todos()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MostrarSolo(Guid id)
        {
            var item = await repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("no encontrado");
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar(T entity)
        {
            var item = await repository.GetByIdAsync(entity.Id);
            if (item != null)
            {
                if (entity.Id == item.Id)
                {
                    return NotFound("ya existe");
                }
            }
            return Ok(await repository.InsertAsync(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var item = await repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("no encontrado");
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, T entity)
        {
            if(id != entity.Id)
            {
                return BadRequest();
            }
            var item = await repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound("no encontrado");
            }
            await repository.UpdateAsync(entity);
            return Ok("Agregado");
        }

        //Implementar demás métodos CRUD: Get por ID, Update, Insert, Delete.
    }
}
