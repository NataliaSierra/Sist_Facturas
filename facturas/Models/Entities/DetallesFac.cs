using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturas.Models.Entities
{
    public class DetallesFac
    {
        public int id { get; set; }
        public int idFactura { get; set; }
        public string descripcion { get; set; }
        public int valor { get; set; }
    }
}
