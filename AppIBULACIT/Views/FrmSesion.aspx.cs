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
    public partial class FrmSesion : System.Web.UI.Page
    {
        IEnumerable<Sesion> sesions = new ObservableCollection<Sesion>();
        SesionManager sesionManager = new SesionManager();

        public string labelsGraficoVistasGlobal = string.Empty;
        public string dataGraficoVistasGlobal = string.Empty;
        public string backgroundcolorsGraficoVistasGlobal = string.Empty;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                {
                    sesions = await sesionManager.ObtenerSesiones(Session["Token"].ToString());
                    InicializarControles();
                    //ObtenerDatosGrafico();
                }
            }
        }

        private async void InicializarControles()
        {

            try
            {
                //servicioCliente = await servicioClienteManager.ObtenerServicios(Session["Token"].ToString());
                gvSesiones.DataSource = sesions.ToList();
                gvSesiones.DataBind();


                EstadisticaManager estadisticaManager = new EstadisticaManager();

                Estadistica estadistica = new Estadistica
                {
                    CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Navegador = Request.Browser.Browser,
                    PlataformaDispositivo = Request.Browser.Platform,
                    FabricanteDispostivo = Request.Browser.MobileDeviceManufacturer,//Dispositivo //FabricanteDispostivo
                    Vista = Convert.ToString(Request.Url).Split('/').Last(),
                    Accion = "InicializarControles"
                };
                Estadistica estadisticaIngresada = await estadisticaManager.Ingresar(estadistica);
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

        protected void gvSesiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvSesiones_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}