using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi02.Model
{
    public class Estado : IModel
    {
        [Key]
        public Guid Id { set; get; }
        [StringLength(50)]
        public string Nombre { set; get; }
    }
}
