using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class CiudadDatos
    {
        private ConexionDB conexion;

        public CiudadDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void insertarCiudad(CiudadModel ciudad)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO ciudad(idCiudad, nombre)" +
                                                   "VALUES(@idCiudad, @nombre)", conn);
            comando.Parameters.AddWithValue("nombre", ciudad.nombre);
            comando.Parameters.AddWithValue("idCiudad", ciudad.idCiudad);

            comando.ExecuteNonQuery();
        }

        public List<CiudadModel> obtenerTodasLasCiudad()
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("SELECT * FROM ciudad", conn);
            using var reader = comando.ExecuteReader();
            var ciudades = new List<CiudadModel>();

            while (reader.Read())
            {
                ciudades.Add(new CiudadModel
                {
                    idCiudad = reader.GetInt32("idCiudad"),
                    nombre = reader.GetString("nombre"),
                });
            }

            return ciudades;
        }
  
        
        public CiudadModel obtenerCiudadPorId(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select * from ciudad where idCiudad = {id}", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new CiudadModel
                {
                    idCiudad = reader.GetInt32("idCiudad"),
                    nombre = reader.GetString("nombre"),
                };
            }
            return null;
        }

        public void modificarCiudad(CiudadModel ciudad) {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE ciudad SET Nombre = '{ciudad.nombre}' WHERE idCiudad = {ciudad.idCiudad}", conn);

            comando.ExecuteNonQuery();
        }  
        
        public CiudadModel EliminarCiudadPorId(int id) {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM ciudad WHERE idciudad = {id}", conn);
            using var reader = comando.ExecuteReader();
            return null;
        }
        
    }
}