using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Optativo_Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PersonaController : Controller {
    
    private PersonaService personaServicio;

    public PersonaController()
    {
        personaServicio = new PersonaService("Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=API_NET_Core;");
    }
    
    //Mostra todas las porsona
    //
    [HttpGet]
    public IActionResult obtenerTodasLasPersona()
    {
        return Ok(personaServicio.obtenerTodasLasPersona());
    }
    //
    
    //Mostra porsona por documento
    //
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(personaServicio.obtenerPersonaPorId(id));
    }
    //
    
    //Insertar ciudad por documento - Basico
    /*/
    [HttpPost("RegistrarPersonaBasico")]
    public IActionResult RegistrarPersonaBasico([FromBody] Models.PersonaModel modelo)
    {
        personaServicio.registrarPersona(
            new Infraestructura.Modelos.PersonaModel
            {
                nombre = modelo.nombre,
                apellido = modelo.apellido,
                direccion = modelo.direccion,
                email = modelo.email,
                celular = modelo.celular,
                edad = modelo.edad,
                ciudad  = new Infraestructura.Modelos.CiudadModel
                {
                    idCiudad = modelo.idciudad
                }
            });
        return Ok("Los datos de persona fueron insertados correctamente");
    } 
    /*/
    
    //Modificar PERSONA
    //
    [HttpPut]
    public IActionResult ModificarCiudadAccion([FromBody] Infraestructura.Modelos.PersonaModel modelo) {
        try {
            personaServicio.modificarPersona(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    //
    
    //Eliminar Persona
    //
    [HttpDelete("{id}")]
    public IActionResult EliminarPersona(int id)
    {
        return Ok(personaServicio.EliminarPersona(id));
    }
    //
}