using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi02.Model
{
    public class Producto
    {
        public long Id { get; set; }
        public string Item { get; set; }
        public string Codigo { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
    }
}
