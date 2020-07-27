using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi02.Model
{
    public class Pedido
    {
        public long Id { set; get; }
        public string Codigo { set; get; }
        public string Estado { set; get; }
        public DateTime Fecha { set; get; }
        public List<Producto> ListaProducto { set; get; }
        public int Total { set; get; }

    }
}
