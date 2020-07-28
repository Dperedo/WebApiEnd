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
    public class EstadosController : BaseController<Estado>
    {
        private IRepository<Estado> repository;

        public EstadosController(IRepository<Estado> _repository) : base(_repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult ListarEstado()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult MostrarSoloEstado(Guid id)
        {
            return Ok(repository.GetByIdAsync(id));
        }

        [HttpPost]
        public IActionResult InsertarEstado(Guid id)
        {
            return Ok(repository.InsertAsync(id));
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarEstado(Guid id)
        {
            return Ok(repository.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarEstado(Guid id)
        {
            return Ok(repository.UpdateAsync(id));
        }
    }
}
