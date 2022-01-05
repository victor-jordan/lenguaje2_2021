using System;
using MySql.Data.MySqlClient;

namespace webAPIprueba.Controllers
{
    public class Connection
    {
        public MySqlConnection objConexion;

        public bool crearConexion()
        {
            string cadenaConnexion = "server=localhost;database=prueba;uid=root;";

            objConexion = new MySqlConnection(cadenaConnexion);

            try
            {
                objConexion.Open();
                // Console.WriteLine("conectado");
                return true;
            }
            catch (Exception error)
            {
                // Console.WriteLine("error: " + error.Message);
                System.Diagnostics.Debug.WriteLine("No se pudo conectar: " + error.Message);
                // throw;
                return false;
            }
        }

        public bool desConectar()
        {
            if (objConexion.State == System.Data.ConnectionState.Open)
            {
                objConexion.Close();
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ocurrió un problema al tratar de cerrar la conexión");
                return false;
            }
        }
    }
}