using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi02.Model
{
    public class Pedido : IModel
    {
        [Key]
        public Guid Id { set; get; }
        public long Secuencial { get; set; }
        public Cliente Cliente { get; set; }
        public Estado Estado { set; get; }
        public DateTime FechaCreado { set; get; }
        public float TotalPedido { set; get; }
        public List<DetallePedido> DetallePedidos { get; set; }
    }
}
