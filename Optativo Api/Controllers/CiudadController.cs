using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Optativo_Api.Controllers;

[ApiController]
[Route("[controller]")]

public class CiudadController : ControllerBase {
    
    private const string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=API_NET_Core;";
    private CiudadService servicio;

    public CiudadController() {
        servicio = new CiudadService(connectionString);
    }
    
    //Mostrar ciudad
    
    //Mostra todas las ciudades
    //
    [HttpGet]
    public IActionResult obtenerTodasLasCiudad()
    {
        return Ok(servicio.obtenerTodasLasCiudad());
    }
    //
    
    [HttpGet("por-parametro")]
    public IActionResult ObtenerCiudadAccion2([FromQuery] int id) {
        var ciudad = servicio.obtenerCiudad(id);
        return Ok(ciudad);
    }
    
    //Agregar ciudad

    [HttpPost]
    public IActionResult InsertarCiudadAccion([FromBody] Infraestructura.Modelos.CiudadModel ciudad) {
        servicio.insertarCiudad(ciudad);
        return Created("Se creo con exito", ciudad);
    }
    
    //Modificar ciudad
    
    [HttpPut]
    public IActionResult ModificarCiudadAccion([FromBody] Infraestructura.Modelos.CiudadModel ciudad) {
        try {
            servicio.modificarCiudad(ciudad);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    
    //Eliminar Persona
    //
    [HttpDelete("{id}")]
    public IActionResult EliminarCiudadPorId(int id)
    {
        return Ok(servicio.EliminarCiudadPorId(id));
    }
    //
    
}