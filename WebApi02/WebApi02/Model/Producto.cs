using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi02.Model
{
    public class Producto : IModel
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(20)]
        public string Codigo { get; set; }
        [StringLength(200)]
        public string Descripcion { get; set; }
        public float Precio { get; set; }

    }
}
