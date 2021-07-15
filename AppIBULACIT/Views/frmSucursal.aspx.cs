using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.Views
{
    public partial class frmSucursal : System.Web.UI.Page
    {
        IEnumerable<Sucursal> sucursals = new ObservableCollection<Sucursal>();
        SucursalManager sucursalManager = new SucursalManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                sucursals = await sucursalManager.ObtenerServicios(Session["Token"].ToString());
                gvSurcursales.DataSource = sucursals.ToList();
                gvSurcursales.DataBind();
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
                lblStatus.Text = "Hubo un error al cargar la lista de sucrusales.";
                lblStatus.Visible = true;
            }
        }

        protected void gvSurcursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptarModal_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptarMant_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {

        }
    }
}