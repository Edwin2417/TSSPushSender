using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TSSPushSender
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=CHICITURBO;Database=prueba;Integrated Security=True;";
            List<EmpleadoDTO> listaEmpleados = new();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Cedula, Salario, SucursalCodigo FROM Empleados";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listaEmpleados.Add(new EmpleadoDTO
                    {
                        Cedula = reader.GetString(0),
                        Salario = reader.GetInt32(1),
                        Sucursal = reader.GetString(2)
                    });
                }
            }

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync("http://localhost:5084/api/tss/upload", listaEmpleados);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Nómina enviada correctamente a la TSS.");
                }
                else
                {
                    Console.WriteLine("❌ Error al enviar la nómina.");
                }
            }
        }

        public class EmpleadoDTO
        {
            public string Cedula { get; set; }
            public int Salario { get; set; }
            public string Sucursal { get; set; }
        }
    }
}
