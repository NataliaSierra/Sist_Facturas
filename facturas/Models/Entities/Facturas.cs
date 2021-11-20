using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturas.Models.Entities
{
    public class Facturas
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public int codigo { get; set; }

        public List<DetallesFac> listaDetallesF { get; set; }
    }
}
