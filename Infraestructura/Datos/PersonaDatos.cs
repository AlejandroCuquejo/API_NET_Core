using Infraestructura.Modelos;
using Infraestructura.Conexiones;
using System.Data;

namespace Infraestructura.Datos;


public class PersonaDatos
{
    private ConexionDB conexion;

    public PersonaDatos(string cadenaConexion)
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    public List<PersonaModel> obtenerTodasLasPersonas()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("SELECT * FROM persona", conn);
        List<PersonaModel> personas = new List<PersonaModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            personas.Add(new PersonaModel()
            {
                idPersona = reader.GetInt32("idPersona"),
                nombre = reader.GetString("nombre"),
                apellido = reader.GetString("apellido"),
                direccion = reader.GetString("direccion"),
                email = reader.GetString("email"),
                celular = reader.GetString("celular"),
                edad = reader.GetString("edad"),
                ciudad = new CiudadModel()
                {
                    idCiudad = reader.GetInt32("idCiudad"),
                    nombre = reader.GetString("nombre"),
                }
            });
        }
    
        return personas;
    }

    
      public PersonaModel obtenerPersonaPorId(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"SELECT c.*, p.* " +
                                                   $"FROM ciudad c " +
                                                   $"INNER JOIN persona p ON p.idciudad = c.idciudad " +
                                                   $"where p.idPersona = '{id}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new PersonaModel()
                {
                    idPersona = reader.GetInt32("idPersona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    direccion = reader.GetString("direccion"),
                    email = reader.GetString("email"),
                    celular = reader.GetString("celular"),
                    edad = reader.GetString("edad"),
                    ciudad = new CiudadModel()
                    {
                        idCiudad = reader.GetInt32("idCiudad"),
                        nombre = reader.GetString("nombre"),
                    }
                };
            }
            return null;
        }
      
      public void insertarPersona(PersonaModel persona)
      {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand("INSERT INTO public.persona (nombre, apellido, direccion, email, celular, edad, idciudad) " +
                                                 "VALUES (@nombre, @apellido, @direccion, @email, @celular, @edad, @idciudad)", conn);

          // Agregar parámetros
          comando.Parameters.AddWithValue("nombre", persona.nombre);
          comando.Parameters.AddWithValue("apellido", persona.apellido);
          comando.Parameters.AddWithValue("direccion", persona.direccion);
          comando.Parameters.AddWithValue("email",  persona.email);
          comando.Parameters.AddWithValue("celular",  persona.celular);
          comando.Parameters.AddWithValue("edad",  persona.edad);
          comando.Parameters.AddWithValue("idciudad", persona.ciudad.idCiudad);

          comando.ExecuteNonQuery();
      }
      /*/
      /*/
      public void modificarPersona(PersonaModel persona) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"UPDATE persona " +
                                                 $"SET nombre = '{persona.nombre}', " +
                                                 $"apellido = '{persona.apellido}', " +
                                                 $"direccion = '{persona.direccion}', " +
                                                 $"email = '{persona.email}', " +
                                                 $"celular = '{persona.celular}', " +
                                                 $"edad = {persona.edad}, " +
                                                 $"idciudad = {persona.ciudad.idCiudad} " +
                                                 $"WHERE idPersona = {persona.idPersona}", conn);
          comando.ExecuteNonQuery();
      }
      
      public CiudadModel EliminarPersnaPorId(int id) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"DELETE FROM persona WHERE idpersona = {id}", conn);
          using var reader = comando.ExecuteReader();
          return null;
      }

}