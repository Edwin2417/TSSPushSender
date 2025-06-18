using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using TSSReceiverAPI.Models;

namespace TSSReceiverAPI.Controllers
{
    [ApiController]
    [Route("api/tss")]
    public class TSSController : ControllerBase
    {
        [HttpPost("upload")]
        public IActionResult SubirNomina([FromBody] List<EmpleadoDTO> empleados)
        {
            foreach (var emp in empleados)
            {
                Console.WriteLine($"✔️ Recibido: {emp.Cedula}, {emp.Salario}, {emp.Sucursal}");
                // Aquí puedes guardar en la base de datos simulada TSS si lo deseas
            }

            return Ok(new { status = "Recibido", cantidad = empleados.Count });
        }
    }
}
