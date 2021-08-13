﻿using System;
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
    [RoutePrefix("api/Tipo_Prestamo")]
    public class Tipo_PrestamoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Tipo_Prestamo tipo_prestamo = new Tipo_Prestamo();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT *
                                                             FROM [Tipo Prestamo]
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        tipo_prestamo.Codigo = sqlDataReader.GetInt32(0);
                        tipo_prestamo.Descripcion = sqlDataReader.GetString(1);
                        tipo_prestamo.TipoTasa = sqlDataReader.GetString(2);
                        tipo_prestamo.FechaInscripcion = sqlDataReader.GetDateTime(3);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(tipo_prestamo);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Tipo_Prestamo> tiposprestamos = new List<Tipo_Prestamo>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT* FROM  [Tipo Prestamo]", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Tipo_Prestamo tipo_prestamo = new Tipo_Prestamo();
                        tipo_prestamo.Codigo = sqlDataReader.GetInt32(0);
                        tipo_prestamo.Descripcion = sqlDataReader.GetString(1);
                        tipo_prestamo.TipoTasa = sqlDataReader.GetString(2);
                        tipo_prestamo.FechaInscripcion = sqlDataReader.GetDateTime(3);
                        tiposprestamos.Add(tipo_prestamo);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(tiposprestamos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Tipo_Prestamo tipo_prestamo)
        {
            if (tipo_prestamo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [Tipo Prestamo] (Descripcion, TipoTasa, FechaInscripcion)
                                                                                VALUES (@Descripcion, @TipoTasa, @FechaInscripcion) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Descripcion", tipo_prestamo.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@TipoTasa", tipo_prestamo.TipoTasa);
                    sqlCommand.Parameters.AddWithValue("@FechaInscripcion", tipo_prestamo.FechaInscripcion);




                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tipo_prestamo);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Tipo_Prestamo tipo_prestamo)
        {
            if (tipo_prestamo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE [Tipo Prestamo] SET Descripcion = @Descripcion,
                                                                                        TipoTasa = @TipoTasa,
                                                                                        FechaInscripcion = @FechaInscripcion 
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", tipo_prestamo.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", tipo_prestamo.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@TipoTasa", tipo_prestamo.TipoTasa);
                    sqlCommand.Parameters.AddWithValue("@FechaInscripcion", tipo_prestamo.FechaInscripcion);



                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(tipo_prestamo);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE [Tipo Prestamo] WHERE Codigo= @Codigo ", sqlConnection);

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
