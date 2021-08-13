using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/TipoTarjeta")]

    public class TipoTarjetaController : ApiController
    {
        // GET ID
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            TipoTarjeta tipoTarjeta = new TipoTarjeta();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Descripcion, FechaIngreso, Categoria
                                                             FROM   TipoTarjeta
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        tipoTarjeta.Codigo = sqlDataReader.GetInt32(0);
                        tipoTarjeta.Descripcion = sqlDataReader.GetString(1);
                        tipoTarjeta.FechaIngreso = sqlDataReader.GetDateTime(2);
                        tipoTarjeta.Categoria = sqlDataReader.GetString(3);


                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tipoTarjeta);
        }




        // GET ALL
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<TipoTarjeta> tipoTarjetas = new List<TipoTarjeta>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Descripcion, FechaIngreso, Categoria
                                                            FROM TipoTarjeta", sqlConnection);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        TipoTarjeta tipoTarjeta = new TipoTarjeta();
                        tipoTarjeta.Codigo = sqlDataReader.GetInt32(0);
                        tipoTarjeta.Descripcion = sqlDataReader.GetString(1);
                        tipoTarjeta.FechaIngreso = sqlDataReader.GetDateTime(2);
                        tipoTarjeta.Categoria = sqlDataReader.GetString(3);

                        tipoTarjetas.Add(tipoTarjeta);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(tipoTarjetas);
        }


        // INSERT
        [HttpPost]
        public IHttpActionResult Ingresar(TipoTarjeta tipoTarjeta)
        {
            if (tipoTarjeta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" INSERT INTO TipoTarjeta (Descripcion, FechaIngreso, Categoria) 
                                         VALUES (@Descripcion, @FechaIngreso, @Categoria)",
                                         sqlConnection);


                    sqlCommand.Parameters.AddWithValue("@Descripcion", tipoTarjeta.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@FechaIngreso", tipoTarjeta.FechaIngreso);
                    sqlCommand.Parameters.AddWithValue("@Categoria", tipoTarjeta.Categoria);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tipoTarjeta);
        }




        // Actualizar
        [HttpPut]
        public IHttpActionResult Actualizar(TipoTarjeta tipoTarjeta)
        {
            if (tipoTarjeta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" UPDATE TipoTarjeta 
                                                        SET 
                                                            Descripcion  = @Descripcion,
                                                            FechaIngreso = @FechaIngreso, 
                                                            Categoria    = @Categoria
                                          WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", tipoTarjeta.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", tipoTarjeta.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@FechaIngreso", tipoTarjeta.FechaIngreso);
                    sqlCommand.Parameters.AddWithValue("@Categoria", tipoTarjeta.Categoria);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tipoTarjeta);
        }






        //Eliminar
        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" DELETE TipoTarjeta WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(id);
        }
    }
}
