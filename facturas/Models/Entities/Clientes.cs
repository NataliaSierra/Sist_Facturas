using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturas.Models.Entities
{
    public class Clientes
    {
        public int id_cliente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int documento { get; set; }

        public List<Facturas> listaFacturas { get; set; }
    }
}
