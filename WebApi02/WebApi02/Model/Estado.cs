using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi02.Model
{
    public class Estado
    {
        public Estado()
        {
            this.EstadoProducto = "Pendiente";
        }
        public long Id { set; get; }
        public string EstadoProducto { set; get; }
    }
}
