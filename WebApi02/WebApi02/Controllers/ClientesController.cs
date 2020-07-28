﻿using System;
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
    public class ClientesController : BaseController<Cliente>
    {
        private IRepository<Cliente> repository;

        public ClientesController(IRepository<Cliente> _repository) : base(_repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult ListarCliente()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult MostrarSoloCliente(Guid id)
        {
            return Ok(repository.GetByIdAsync(id));
        }

        [HttpPost]
        public IActionResult InsertarCliente(Guid id)
        {
            return Ok(repository.InsertAsync(id));
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCliente(Guid id)
        {
            return Ok(repository.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(Guid id)
        {
            return Ok(repository.UpdateAsync(id));
        }
    }
}
