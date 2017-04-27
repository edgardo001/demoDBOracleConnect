using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoDBOracleConnect
{
    class Program
    {
        private static string HostNameBD = "192.168.1.1";//La ip del server ORACLE o el hostname
        private static string PuertoBD = "1521";//En una instaacion de ORACLE por defecto, es el puerto 1521
        private static string NombreBD = "tuDB";//El nombre que le has dado a tu DB ORACLE
        private static string UsuarioBD = "tuUsuarioDB";//El nombre que le has dado a tu usuario ORACLE (Fijarse que este usuario tenga los permisos necesarios)
        private static string ClaveBD = "passw0rdDB";//La clave del usuario que corresponda

        static void Main(string[] args)
        {
            Program p = new Program();//Se realiza una instancia de esta misma clase "Program", esto es por q el main es "static" y no puede llamar metodos no estaticos de la clase.
            p.testOracleDB();
        }

        /// <summary>
        /// Testea la conexion a la base de datos oracle
        /// </summary>
        private void testOracleDB()
        {
            string oradb = "Data Source=(DESCRIPTION="
                            + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + HostNameBD + ")(PORT=" + PuertoBD + ")))"
                            + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + NombreBD + ")));"
                            + "User Id=" + UsuarioBD + ";Password=" + ClaveBD + ";";
            try
            {
                OracleConnection connection = new OracleConnection(oradb);
                connection.Open();

                Console.WriteLine("conexion OK");

                //En este lugar debe ir la logica de consultas SQL
                string stmtSelect = "SELECT * FROM tuTabla";
                OracleCommand comando = new OracleCommand(stmtSelect,connection);//creo un comando y le paso la conexion
                OracleDataReader reader = comando.ExecuteReader();//Ejecuto el comando 

                //Consulto si tiene filas
                if (reader.HasRows)
                {
                    //Entro a leer los datos contenidos por cada fila
                    while (reader.Read())
                    {
                        //Muestro lo leido, suponiendo que el primero corresponde al ID y el otro corresponde a un dato en varchar 
                        Console.WriteLine("{0}\t{1}", reader.GetInt32(0), reader.GetString(1));
                    }
                }
                connection.Close();                
            }
            catch (OracleException ex1)
            {
                Console.WriteLine("Error en la conexion: " + ex1.Message + " - " + ex1.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexion: " + ex.Message + " - " + ex.StackTrace);
            }
        }
    }
}
