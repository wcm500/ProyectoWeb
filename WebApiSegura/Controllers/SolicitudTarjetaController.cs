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
    [RoutePrefix("api/SolicitudTarjeta")]


    public class SolicitudTarjetaController : ApiController 



    {
        // GET ID
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            SolicitidTarjeta solicitidTarjeta = new SolicitidTarjeta();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoCliente,FechaSolicitud,CondicionLaboral,IngresoMensual,CodigoTipoTarjeta 
                                                             FROM   SolicitidTarjeta
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        solicitidTarjeta.Codigo = sqlDataReader.GetInt32(0);
                        solicitidTarjeta.CodigoCliente = sqlDataReader.GetInt32(1);
                        solicitidTarjeta.FechaSolicitud = sqlDataReader.GetDateTime(2);
                        solicitidTarjeta.CondicionLaboral = sqlDataReader.GetString(3);
                        solicitidTarjeta.IngresoMensual = sqlDataReader.GetDecimal(4);
                        solicitidTarjeta.CodigoTipoTarjeta = sqlDataReader.GetInt32(5);          
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(solicitidTarjeta);
        }


        // GET ALL
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<SolicitidTarjeta> solicitidTarjetas = new List<SolicitidTarjeta>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT *
                                                            FROM SolicitidTarjeta", sqlConnection);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        SolicitidTarjeta solicitidTarjeta = new SolicitidTarjeta();
                        solicitidTarjeta.Codigo = sqlDataReader.GetInt32(0);
                        solicitidTarjeta.CodigoCliente = sqlDataReader.GetInt32(1);
                        solicitidTarjeta.FechaSolicitud = sqlDataReader.GetDateTime(2);
                        solicitidTarjeta.CondicionLaboral = sqlDataReader.GetString(3);
                        solicitidTarjeta.IngresoMensual = sqlDataReader.GetDecimal(4);
                        solicitidTarjeta.CodigoTipoTarjeta = sqlDataReader.GetInt32(5);

                        solicitidTarjetas.Add(solicitidTarjeta);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(solicitidTarjetas);
        }








        // INSERT
        [HttpPost]
        public IHttpActionResult Ingresar(SolicitidTarjeta solicitidTarjeta)
        {
            if (solicitidTarjeta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" INSERT INTO SolicitidTarjeta (CodigoCliente, FechaSolicitud, CondicionLaboral, IngresoMensual, CodigoTipoTarjeta) 
                                         VALUES (@CodigoCliente, @FechaSolicitud, @CondicionLaboral, @IngresoMensual, @CodigoTipoTarjeta)",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoCliente", solicitidTarjeta.CodigoCliente);
                    sqlCommand.Parameters.AddWithValue("@FechaSolicitud", solicitidTarjeta.FechaSolicitud);
                    sqlCommand.Parameters.AddWithValue("@CondicionLaboral", solicitidTarjeta.CondicionLaboral);
                    sqlCommand.Parameters.AddWithValue("@IngresoMensual", solicitidTarjeta.IngresoMensual);
                    sqlCommand.Parameters.AddWithValue("@CodigoTipoTarjeta", solicitidTarjeta.CodigoTipoTarjeta);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(solicitidTarjeta);
        }







        // Actualizar
        [HttpPut]
        public IHttpActionResult Actualizar(SolicitidTarjeta solicitidTarjeta)
        {
            if (solicitidTarjeta == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@" UPDATE SolicitidTarjeta 
                                                        SET                                                          
                                                    CodigoCliente = @CodigoCliente,
                                                    FechaSolicitud = @FechaSolicitud,
                                                    CondicionLaboral = @CondicionLaboral,
                                                    IngresoMensual=  @IngresoMensual,
                                                    CodigoTipoTarjeta = @CodigoTipoTarjeta
                                          WHERE Codigo = @Codigo",
                                         sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", solicitidTarjeta.Codigo); 
                    sqlCommand.Parameters.AddWithValue("@CodigoCliente", solicitidTarjeta.CodigoCliente);
                    sqlCommand.Parameters.AddWithValue("@FechaSolicitud", solicitidTarjeta.FechaSolicitud);
                    sqlCommand.Parameters.AddWithValue("@CondicionLaboral", solicitidTarjeta.CondicionLaboral);
                    sqlCommand.Parameters.AddWithValue("@IngresoMensual", solicitidTarjeta.IngresoMensual);
                    sqlCommand.Parameters.AddWithValue("@CodigoTipoTarjeta", solicitidTarjeta.CodigoTipoTarjeta);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(solicitidTarjeta);
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
                        new SqlCommand(@" DELETE SolicitidTarjeta WHERE Codigo = @Codigo",
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
