using CursoUdemyWebAPI.DTO;
using CursoUdemyWebAPI.Modelo;
using CursoUdemyWebAPI.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoUdemyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpleadoController : ControllerBase
    {
        private readonly IServicioEmpleadoSQL _servicioEmpleado;
        public EmpleadoController(IServicioEmpleadoSQL servicioEmpleado)
        {
            _servicioEmpleado = servicioEmpleado;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<EmpleadoDTO>> DameEmpleados()
        {
            var listaEmpleados = (await _servicioEmpleado.DameEmpleados()).Select(e => e.convertirDTO());

            return listaEmpleados;
        }

        [HttpGet("{codEmpleado}")]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> DameEmpleado(string codEmpleado)
        {
            var empleado = (await _servicioEmpleado.DameEmpleado(codEmpleado)).convertirDTO();

            if (empleado is null)
            {
                return NotFound("Empleado no encontrado para el código solicitado");
            }

            return empleado;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> NuevoEmpleado(EmpleadoDTO e)
        {
            Empleado empleado = new Empleado
            {
                CodEmpleado = e.CodEmpleado,
                Nombre = e.Nombre,
                Email = e.Email,
                Edad = e.Edad,
                FechaAlta = DateTime.Now
            };

            await _servicioEmpleado.NuevoEmpleado(empleado);

            return empleado.convertirDTO();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> ModificarEmpleado(EmpleadoDTO e)
        {
            var empleadoAux = await _servicioEmpleado.DameEmpleado(e.CodEmpleado);
            if (empleadoAux is null)
            {
                return NotFound();
            }

            empleadoAux.CodEmpleado = e.CodEmpleado;
            empleadoAux.Nombre = e.Nombre;
            empleadoAux.Email = e.Email;
            empleadoAux.Edad = e.Edad;

            await _servicioEmpleado.ModificarEmpleado(empleadoAux);

            return e;
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> BorrarEmpleado(string codEmpleado)
        {
            var empleadoAux = await _servicioEmpleado.DameEmpleado(codEmpleado);
            if (empleadoAux is null)
            {
                return NotFound();
            }

            await _servicioEmpleado.BajaEmpleado(codEmpleado);

            return Ok();
        }
    }
}
