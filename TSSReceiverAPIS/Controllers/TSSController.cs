using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TSSReceiverAPI.Models;

namespace TSSReceiverAPI.Controllers
{
    [ApiController]
    [Route("api/tss")]
    public class TSSController : ControllerBase
    {
        private readonly string connectionString = "Server=CHICITURBO;Database=prueba;Integrated Security=True;";

        [HttpPost("upload")]
        public IActionResult SubirNomina([FromBody] List<EmpleadoDTO> empleados)
        {
            int insertados = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var emp in empleados)
                {
                    // Mostrar en consola
                    Console.WriteLine($"✔️ Recibido: {emp.Cedula}, {emp.Salario}, {emp.Sucursal}");

                    // Insertar en la base de datos
                    string query = @"INSERT INTO TSS_Nomina_Recibida (Cedula, Salario, SucursalCodigo)
                                     VALUES (@Cedula, @Salario, @Sucursal)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", emp.Cedula);
                        cmd.Parameters.AddWithValue("@Salario", emp.Salario);
                        cmd.Parameters.AddWithValue("@Sucursal", emp.Sucursal);
                        cmd.ExecuteNonQuery();
                        insertados++;
                    }
                }
            }

            return Ok(new { mensaje = "Nómina recibida, mostrada en consola y almacenada correctamente", cantidad = insertados });
        }
    }
}
