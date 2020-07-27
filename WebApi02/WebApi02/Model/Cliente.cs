using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi02.Model
{
    public class Cliente : IModel
    {
        [Key]
        public Guid Id { set; get; }
        [StringLength(20)]
        public string RUT { set; get; }
        [StringLength(250)]
        public string RazonSocial { set; get; }
        

    }
}
