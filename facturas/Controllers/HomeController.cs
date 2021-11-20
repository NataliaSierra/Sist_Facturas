using facturas.Models;
using facturas.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace facturas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public List<Clientes> getClientes()
        {
            List<Clientes> listaClientes = new List<Clientes>();

            Clientes cliente = new Clientes();

            cliente.id_cliente = 01;
            cliente.nombre = "Valeria";
            cliente.apellido = "Velez";
            cliente.documento = 1193354506;
            listaClientes.Add(cliente);

            List<Facturas> listaFacturas = new List<Facturas>();

            Facturas factura1 = new Facturas();

            factura1.id = 0001;
            factura1.idCliente = 01;
            factura1.codigo = 00001;
            listaFacturas.Add(factura1);


            List<DetallesFac> listaDetallesF = new List<DetallesFac>();

            DetallesFac detalles = new DetallesFac();

            detalles.id = 001;
            detalles.idFactura = 0001;
            detalles.descripcion = "Remington Keratin Therapy";
            detalles.valor = 180000;
            listaDetallesF.Add(detalles);

            factura1.listaDetallesF = listaDetallesF;


            return listaClientes;

        }

        BaseDatos bd = new BaseDatos();
        public string insertarClientes([FromBody] Clientes insertClient)
        {
            string sql = "INSERT INTO cliente(id_cliente, nombre, apellido, documento)VALUES('" + insertClient.id_cliente + "','" + insertClient.nombre + "','" + insertClient.apellido + "','" + insertClient.documento + "')";
            string resultado = bd.executeSQL(sql);
            return resultado;

        }
        public string insertarFacturas([FromBody] Facturas model)
        {
            string sql = "INSERT INTO factura (id_cliente, codigo) values(" + model.idCliente + ", " + model.codigo + ");" + Environment.NewLine;

            sql += "select @@identity as id;" + Environment.NewLine;

            foreach (DetallesFac item in model.listaDetallesF)
            {
                sql += "INSERT INTO detalles_factura (id_invoice, descripcion, valor) values(@@identity, '" + item.descripcion + "','" + item.valor + "'); ";
            }
            string resultado = bd.executeSQL(sql);

            return resultado;
        }

        public List<Clientes> clients()
        {
            List<Clientes> User = new List<Clientes>();
            DataTable data = bd.getClient("SELECT * FROM cliente");
            User = (from DataRow datos in data.Rows
                    select new Clientes()
                    {
                        id_cliente = Convert.ToInt32(datos["id_cliente"]),
                        nombre = Convert.ToString(datos["nombre"]),
                        apellido = Convert.ToString(datos["apellido"]),
                        documento = Convert.ToInt32(datos["documento"])
                    }).ToList();
            return User;
        }

        public List<Clientes> OneClient(int id)
        {
            List<Clientes> User = new List<Clientes>();
            DataTable data = bd.getClient($"SELECT * FROM cliente where id_cliente = {id}");
            User = (from DataRow datos in data.Rows
                    select new Clientes()
                    {
                        id_cliente = Convert.ToInt32(datos["id_cliente"]),
                        nombre = Convert.ToString(datos["nombre"]),
                        apellido = Convert.ToString(datos["apellido"]),
                        documento = Convert.ToInt32(datos["documento"])
                    }).ToList();
            return User;
        }

        public List<Facturas> mostrarFactura(int id)
        {
            List<Facturas> User = new List<Facturas>();
            List<DetallesFac> details = new List<DetallesFac>();

            DataTable data = bd.getClient($"SELECT * FROM factura where id_invoice = {id}");
            User = (from DataRow datos in data.Rows
                    select new Facturas()
                    {
                        id = Convert.ToInt32(datos["id_invoice"]),
                        idCliente = Convert.ToInt32(datos["id_cliente"]),
                        codigo = Convert.ToInt32(datos["codigo"]),
                        listaDetallesF = details
                    }).ToList();

            DataTable data1 = bd.getClient($"SELECT * FROM detalles_factura");
            details = (from DataRow datos in data1.Rows
                    select new DetallesFac()
                    {
                        id = Convert.ToInt32(datos["id_detalles"]),
                        idFactura = Convert.ToInt32(datos["id_invoice"]),
                        descripcion = Convert.ToString(datos["descripcion"]),
                        valor = Convert.ToInt32(datos["valor"])
                    }).ToList();
            return User;
        }




        public IActionResult GestionUser()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
