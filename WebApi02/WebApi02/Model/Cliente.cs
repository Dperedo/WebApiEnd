using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi02.Model
{
    public class Cliente
    {
        public long Id { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string RUT { set; get; }

    }
}
