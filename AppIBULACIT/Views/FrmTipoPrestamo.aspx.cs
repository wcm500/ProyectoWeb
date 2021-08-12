using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.Views
{
    public partial class frmTipoPrestamo : System.Web.UI.Page
    {
        IEnumerable<Tipo_Prestamo> tipo_Prestamos = new ObservableCollection<Tipo_Prestamo>();
        TipoPrestamoManager tipoprestamoManager = new TipoPrestamoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                tipo_Prestamos = await tipoprestamoManager.ObtenerTiposPrestamos(Session["Token"].ToString());
                gvTipoPrestamo.DataSource = tipo_Prestamos.ToList();
                gvTipoPrestamo.DataBind();
            }
            catch (Exception ex)
            {
                ///Error de usuario
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error
                {
                    CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "Servicio.aspx",
                    Accion = "InicializarControles()",
                    Fuente = ex.Source,
                    Numero = ex.HResult.ToString(),
                    Descripcion = ex.Message
                };

                Error errorIngresado = await errorManager.Ingresar(error);
                lblStatus.Text = "Hubo un error al cargar la lista de servicios.";
                lblStatus.Visible = true;
            }
        }


        protected void gvTipoPrestamo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTipoPrestamo.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Ticket";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtDescripcion.Text = row.Cells[1].Text.Trim();
                    ddlEstadoMant.Text = row.Cells[2].Text.Trim();
                    txtFechaInscripcion.Text = row.Cells[3].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el servicio # ";
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nuevo Tipo de Prestamo";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            txtDescripcion.Visible = true;
            ltrDescripcion.Visible = true;
            ddlEstadoMant.Enabled = true;
            txtFechaInscripcion.Visible = true;
            ltrFechaInscripcion.Visible = true;
            txtCodigoMant.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtFechaInscripcion.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await tipoprestamoManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    lblCodigoEliminar.Text = string.Empty;
                    ltrModalMensaje.Text = "Tipo Prestamo eliminado";
                    btnAceptarModal.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    InicializarControles();
                }
            }
            catch (Exception ex)
            {

                ErrorManager errorManager = new ErrorManager();
                Error error = new Error
                {
                    CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "Servicio.aspx",
                    Accion = "InicializarControles()",
                    Fuente = ex.Source,
                    Numero = ex.HResult.ToString(),
                    Descripcion = ex.Message
                };

                Error errorIngresado = await errorManager.Ingresar(error);
            }
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text))//Insertar
            {
                if (ValidarInsertar())
                {
                    Tipo_Prestamo tipo_prestamo = new Tipo_Prestamo()
                    {
                        Descripcion = txtDescripcion.Text,
                        TipoTasa = ddlEstadoMant.SelectedValue,
                        FechaInscripcion = DateTime.Now
                    };

                    Tipo_Prestamo TipoPrestamoIngresado = await tipoprestamoManager.Ingresar(tipo_prestamo, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(TipoPrestamoIngresado.Descripcion))
                    {
                        lblResultado.Text = "Tipo de Prestamo ingresado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion.";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }
                }
            }
            else //Modificar
            {
                if (ValidarModificar())
                {
                    Tipo_Prestamo tipo_prestamo = new Tipo_Prestamo()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        Descripcion = txtDescripcion.Text,
                        TipoTasa = ddlEstadoMant.SelectedValue,
                        FechaInscripcion = DateTime.Now
                    };

                    Tipo_Prestamo TipoPrestamoModificado = await tipoprestamoManager.Actualizar(tipo_prestamo, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(TipoPrestamoModificado.Descripcion))
                    {
                        lblResultado.Text = "Tipo Prestamo actualizado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion.";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }
                }
            }
        }


        private bool ValidarInsertar()
        {

            if (txtDescripcion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar una descripcion del prestamo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtDescripcion.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "No solo pueden ingresar numeros";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            lblStatus.Text = "Insertado Correctamente";
            lblStatus.ForeColor = Color.Green;
            lblStatus.Visible = true;
            return true;
        }

        private bool ValidarModificar()
        {
            if (txtDescripcion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar una descripcion del prestamo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtDescripcion.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "No solo pueden ingresar numeros";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            lblStatus.Text = "Modificado Correctamente";
            lblStatus.ForeColor = Color.Green;
            lblStatus.Visible = true;
            return true;
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseMantenimiento(); });", true);
        }
    }
}