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

        //[HttpGet]
        //public IActionResult Todos()
        //{
        //    return Ok(repository.GetAll());
        //}

        [HttpGet("{id}")]
        public IActionResult MostrarSolo(Guid id)
        {
            return Ok(repository.GetByIdAsync(id));
        }

        [HttpPost]
        public IActionResult Insertar(Guid id)
        {
            return Ok(repository.InsertAsync(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(Guid id)
        {
            return Ok(repository.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(Guid id)
        {
            return Ok(repository.UpdateAsync(id));
        }

        //Implementar demás métodos CRUD: Get por ID, Update, Insert, Delete.
    }
}
