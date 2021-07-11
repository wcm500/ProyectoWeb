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
    [RoutePrefix("api/ServicioCliente")]
    public class ServicioClienteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            ServicioCliente servicioCliente = new ServicioCliente();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT *
                                                             FROM ServicioCliente
                                                             WHERE CodigoServicio = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        servicioCliente.CodigoServicio = sqlDataReader.GetInt32(0);
                        servicioCliente.CodigoUsuario = sqlDataReader.GetInt32(1);
                        servicioCliente.FechaCreacion = sqlDataReader.GetDateTime(2);
                        servicioCliente.Descripcion = sqlDataReader.GetString(3);
                        servicioCliente.TipoAyuda = sqlDataReader.GetString(4);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(servicioCliente);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<ServicioCliente> servicios = new List<ServicioCliente>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT *
                                                             FROM ServicioCliente", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        ServicioCliente servicioCliente = new ServicioCliente();
                        servicioCliente.CodigoServicio = sqlDataReader.GetInt32(0);
                        servicioCliente.CodigoUsuario = sqlDataReader.GetInt32(1);
                        servicioCliente.FechaCreacion = sqlDataReader.GetDateTime(2);
                        servicioCliente.Descripcion = sqlDataReader.GetString(3);
                        servicioCliente.TipoAyuda = sqlDataReader.GetString(4);
                        servicios.Add(servicioCliente);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(servicios);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(ServicioCliente servicioCliente)
        {
            if (servicioCliente == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO ServicioCliente (CodigoUsuario, FechaCreacion, Descripcion, TipoAyuda)
                                                             VALUES (@CodigoUsuario, @FechaCreacion, @Descripcion, @TipoAyuda) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", servicioCliente.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@FechaCreacion", servicioCliente.FechaCreacion);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", servicioCliente.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@TipoAyuda", servicioCliente.TipoAyuda);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(servicioCliente);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(ServicioCliente servicioCliente)
        {
            if (servicioCliente == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE ServicioCliente SET Descripcion = @Descripcion,
                                                                             CodigoUsuario = @CodigoUsuario,
                                                                             FechaCreacion = @FechaCreacion,
                                                                             TipoAyuda=@TipoAyuda
                                                             WHERE CodigoServicio = @CodigoServicio ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoServicio", servicioCliente.CodigoServicio);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", servicioCliente.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", servicioCliente.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@FechaCreacion", servicioCliente.FechaCreacion); 
                    sqlCommand.Parameters.AddWithValue("@TipoAyuda", servicioCliente.TipoAyuda);
                    

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(servicioCliente);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE SERVICIOCLIENTE WHERE CodigoServicio = @Codigo ", sqlConnection);

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
