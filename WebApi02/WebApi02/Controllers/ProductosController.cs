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
    }
}
