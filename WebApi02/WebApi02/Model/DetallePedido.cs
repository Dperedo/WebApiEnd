using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi02.Model
{
    public class DetallePedido : IModel
    {
        [Key]
        public Guid Id { set; get; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }        
    }
}
