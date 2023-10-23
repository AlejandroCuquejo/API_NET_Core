namespace Infraestructura.Modelos;

public class PersonaModel {

    public int idPersona { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string direccion { get; set; }
    public string email { get; set; }
    public string celular { get; set; }
    public string edad { get; set; }
    public CiudadModel ciudad { get; set; }
    
}