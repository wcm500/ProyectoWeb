using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT
{
    public partial class _Default : Page
    {
        IEnumerable<ServicioCliente> servicioCliente = new ObservableCollection<ServicioCliente>();
        ServicioClienteManager servicioClienteManager = new ServicioClienteManager();

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
                    servicioCliente = await servicioClienteManager.ObtenerServicios(Session["Token"].ToString());
                    InicializarControles();
                    ObtenerDatosGrafico();
                }
            }
        }

        private void ObtenerDatosGrafico()
        {

            StringBuilder script = new StringBuilder();
            StringBuilder labelsGraficoVistas = new StringBuilder();
            StringBuilder dataGraficoVistas = new StringBuilder();
            StringBuilder backgroundcolorsGraficoVistas = new StringBuilder();

            var random = new Random();

            foreach (var servicio in servicioCliente.GroupBy(info => info.TipoAyuda).
                Select(group => new {
                    TipoAyuda = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.TipoAyuda))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labelsGraficoVistas.Append(string.Format("'{0}',", servicio.TipoAyuda)); // 'Correo','frmError',
                dataGraficoVistas.Append(string.Format("'{0}',", servicio.Cantidad)); // '2','3',
                backgroundcolorsGraficoVistas.Append(string.Format("'{0}',", color));

                labelsGraficoVistasGlobal = labelsGraficoVistas.ToString().Substring(0, labelsGraficoVistas.Length - 1);
                dataGraficoVistasGlobal = dataGraficoVistas.ToString().Substring(0, dataGraficoVistas.Length - 1);
                backgroundcolorsGraficoVistasGlobal =
                    backgroundcolorsGraficoVistas.ToString().Substring(backgroundcolorsGraficoVistas.Length - 1);
            }

        }

        private async void InicializarControles()
        {

            try
            {
                EstadisticaManager estadisticaManager = new EstadisticaManager();

                Estadistica estadistica = new Estadistica
                {
                    CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Navegador = Request.Browser.Browser,
                    PlataformaDispositivo = Request.Browser.Platform,
                    FabricanteDispostivo = Request.Browser.MobileDeviceManufacturer,
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

            }
        }
    }
}