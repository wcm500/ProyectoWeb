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
    public partial class frmPrestamo : System.Web.UI.Page
    {
        IEnumerable<Prestamo> prestamo = new ObservableCollection<Prestamo>();
        PrestamoManager prestamoManager = new PrestamoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }
        private async void InicializarControles()
        {

            try
            {
                prestamo = await prestamoManager.ObtenerPrestamos(Session["Token"].ToString());
                gvPrestamo.DataSource = prestamo.ToList();
                gvPrestamo.DataBind();
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

        protected void gvPrestamo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPrestamo.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Ticket";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    CldFechaLimite.SelectedDate = row.Cells[1].Text.Trim();

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
            txtCodigoMant.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await prestamoManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    lblCodigoEliminar.Text = string.Empty;
                    ltrModalMensaje.Text = "Sucursal eliminado";
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

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }

        protected void btnAceptarMant_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {

        }
    }
}