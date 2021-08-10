using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
using Microsoft.Ajax.Utilities;

namespace AppIBULACIT.Views
{
    public partial class frmServicioCliente : System.Web.UI.Page
    {
        IEnumerable<ServicioCliente> servicioCliente = new ObservableCollection<ServicioCliente>();
        ServicioClienteManager servicioClienteManager = new ServicioClienteManager();

        public string labelsGraficoVistasGlobal = string.Empty;
        public string dataGraficoVistasGlobal = string.Empty;
        public string backgroundcolorsGraficoVistasGlobal = string.Empty;

        async protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else {
                    servicioCliente = await servicioClienteManager.ObtenerServicios(Session["Token"].ToString());
                    InicializarControles(); 
                    ObtenerDatosGrafico(); 
                }
            }

        }
        private async void InicializarControles()
        {

            try
            {
                //servicioCliente = await servicioClienteManager.ObtenerServicios(Session["Token"].ToString());
                gvServicioCliente.DataSource = servicioCliente.ToList();
                gvServicioCliente.DataBind();


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

                Error errorIngresado = await errorManager.Ingresar(error);
                lblStatus.Text = "Hubo un error al cargar la lista de servicios.";
                lblStatus.Visible = true;
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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nuevo Ticket Soporte";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            ltrFechaCreacion.Visible = true;
            txtFechaCreacion.Visible = true;
            txtDescripcion.Visible = true;
            ltrDescripcion.Visible = true;
            
            ddlEstadoMant.Enabled = true;
            txtCodigoMant.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text))//Insertar
            {
                if (ValidarInsertar())
                {
                    ServicioCliente servicioCliente = new ServicioCliente()
                    {
                        CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                        FechaCreacion = DateTime.Now,
                        Descripcion = txtDescripcion.Text,
                        TipoAyuda = ddlEstadoMant.SelectedValue
                    };

                    ServicioCliente servicioIngresado = await servicioClienteManager.Ingresar(servicioCliente, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(servicioIngresado.Descripcion))
                    {
                        lblResultado.Text = "Servicio Cliente ingresado con exito";
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


                    ServicioCliente servicioCliente = new ServicioCliente()
                    {
                        CodigoServicio = Convert.ToInt32(txtCodigoMant.Text),
                        CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                        FechaCreacion = DateTime.Now,
                        Descripcion = txtDescripcion.Text,
                        TipoAyuda = ddlEstadoMant.SelectedValue
                    };

                    ServicioCliente servicioClienteModificado = await servicioClienteManager.Actualizar(servicioCliente, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(servicioClienteModificado.Descripcion))
                    {
                        lblResultado.Text = "actualizado con exito";
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
                lblStatus.Text = "Debe ingresar una descripcion del ticket";
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
                lblStatus.Text = "Debe ingresar una descripcion del ticket";
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

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await servicioClienteManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    lblCodigoEliminar.Text = string.Empty;
                    ltrModalMensaje.Text = "eliminado";
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

        protected void gvServicioCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvServicioCliente.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Ticket";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtCodigoUsuario.Text = row.Cells[1].Text.Trim();
                    txtFechaCreacion.Text = row.Cells[2].Text.Trim();
                    txtDescripcion.Text = row.Cells[3].Text.Trim();

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
    }
}