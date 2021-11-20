using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text;
using System.Data;

namespace facturas.Models.Entities
{


    public class BaseDatos
    {
        private MySqlConnection connection;
        public BaseDatos()
        {
            connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=Sena1234;database=sistfacturacion;");
        }

        public string executeSQL(string sql)
        {
            string resultado = "";
            
            connection.Open();

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            int filasResultado = cmd.ExecuteNonQuery();

            if (filasResultado > -1)
            {
                resultado = "Correcto";
            }
            else
            {
                resultado = "Incorrecto";
            }

            connection.Close();


            return resultado;
        }
        public DataTable getClient(string sql)
        {
            DataTable client = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataAdapter item = new MySqlDataAdapter(cmd);
                item.Fill(client);
                connection.Close();
                item.Dispose();
            }
            catch (Exception)
            {
                return null;
            }
            return client;
            //Fill trae la info de la bd datos

        }

    }
}



