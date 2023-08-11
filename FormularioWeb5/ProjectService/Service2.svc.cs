using System;
/*using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;*/
using System.Data.SqlClient; //agregar AHORA

namespace ProjectService
{
        public class Service2 : IService2
        {
            public void GuardarInformacion(string Nombre, string Apellidos, string Sexo, string Email, string Direccion, string Ciudad, 
                string Requerimiento)
            {
                string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=DBWeb1;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "INSERT INTO TablaInformacion2 (Nombre, Apellidos, Sexo, Email, Direccion, Ciudad, Requerimiento) " +
                                       "VALUES (@Nombre, @Apellidos, @Sexo, @Email, @Direccion, @Ciudad, @Requerimiento)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Nombre", Nombre);
                            command.Parameters.AddWithValue("@Apellidos", Apellidos);
                            command.Parameters.AddWithValue("@Sexo", Sexo);
                            command.Parameters.AddWithValue("@Email", Email);
                            command.Parameters.AddWithValue("@Direccion", Direccion);
                            command.Parameters.AddWithValue("@Ciudad", Ciudad);
                            command.Parameters.AddWithValue("@Requerimiento", Requerimiento);
                            command.ExecuteNonQuery();
                        }

                    }
                    catch (Exception ex)
                    { Console.WriteLine("Error al guardar la información en la base de datos: " + ex.Message); }                                           // Manejo del error en caso de que no se pueda guardar la información

                     finally
                    { connection.Close(); }
            }
            }
        }
    }




