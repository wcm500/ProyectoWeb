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
    //[AllowAnonymous]
    [RoutePrefix("api/Prestamos")]
    public class PrestamosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Prestamos prestamos = new Prestamos();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT *
                                                             FROM Prestamos
                                                             WHERE Codigo= @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        prestamos.Codigo = sqlDataReader.GetInt32(0);
                        prestamos.FechaLimite = sqlDataReader.GetDateTime(1);
                        prestamos.FechaInicio = sqlDataReader.GetDateTime(2);
                        prestamos.MontoPago = sqlDataReader.GetDecimal(3);
                        prestamos.TipoPrestamo = sqlDataReader.GetInt32(4);
                        prestamos.Interes = sqlDataReader.GetDecimal(5);
                        prestamos.CodigoCuenta = sqlDataReader.GetInt32(6);
                        prestamos.CodigoSucursal = sqlDataReader.GetInt32(7);
                     
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(prestamos);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Prestamos> prestamos1 = new List<Prestamos>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT *
                                                             FROM Prestamos", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Prestamos prestamos = new Prestamos();
                        prestamos.Codigo = sqlDataReader.GetInt32(0);
                        prestamos.FechaLimite = sqlDataReader.GetDateTime(1);
                        prestamos.FechaInicio = sqlDataReader.GetDateTime(2);
                        prestamos.MontoPago = sqlDataReader.GetDecimal(3);
                        prestamos.TipoPrestamo = sqlDataReader.GetInt32(4);
                        prestamos.Interes = sqlDataReader.GetDecimal(5);
                        prestamos.CodigoCuenta = sqlDataReader.GetInt32(6);
                        prestamos.CodigoSucursal = sqlDataReader.GetInt32(7);
                        prestamos1.Add(prestamos);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(prestamos1);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Prestamos prestamos)
        {
            if (prestamos == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Prestamos (FechaLimite, FechaInicio, MontoPago, TipoPrestamo, Interes, CodigoCuenta, CodigoSucursal )
                                                                            VALUES (@FechaLimite, @FechaInicio, @MontoPago, @TipoPrestamo, @Interes , @CodigoCuenta , @CodigoSucursal) ", sqlConnection);

                  
                    sqlCommand.Parameters.AddWithValue("@FechaLimite", prestamos.FechaLimite);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", prestamos.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@MontoPago", prestamos.MontoPago);
                    sqlCommand.Parameters.AddWithValue("@TipoPrestamo", prestamos.TipoPrestamo);
                    sqlCommand.Parameters.AddWithValue("@Interes", prestamos.Interes);
                    sqlCommand.Parameters.AddWithValue("@CodigoCuenta", prestamos.CodigoCuenta);
                    sqlCommand.Parameters.AddWithValue("@CodigoSucursal", prestamos.CodigoSucursal);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(prestamos);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Prestamos prestamos)
        {
            if (prestamos == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Prestamos SET FechaLimite = @FechaLimite,
                                                                             FechaInicio = @FechaInicio,
                                                                             MontoPago = @MontoPago,
                                                                             TipoPrestamo=@TipoPrestamo,
                                                                             Interes = @Interes,
                                                                             CodigoCuenta = @CodigoCuenta,
                                                                             CodigoSucursal = @CodigoSucursal
                                                                            WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", prestamos.FechaLimite);
                    sqlCommand.Parameters.AddWithValue("@FechaLimite", prestamos.FechaLimite);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", prestamos.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@MontoPago", prestamos.MontoPago);
                    sqlCommand.Parameters.AddWithValue("@TipoPrestamo", prestamos.TipoPrestamo);
                    sqlCommand.Parameters.AddWithValue("@Interes", prestamos.Interes);
                    sqlCommand.Parameters.AddWithValue("@CodigoCuenta", prestamos.CodigoCuenta);
                    sqlCommand.Parameters.AddWithValue("@CodigoSucursal", prestamos.CodigoSucursal);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(prestamos);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Prestamos WHERE Codigo = @Codigo ", sqlConnection);

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
