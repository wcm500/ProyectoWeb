using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.Views
{
    public partial class frmSucursal : System.Web.UI.Page
    {
        IEnumerable<Sucursal> sucursals = new ObservableCollection<Sucursal>();
        SucursalManager sucursalManager = new SucursalManager();

        public string labelsGraficoVistasGlobal = string.Empty;
        public string dataGraficoVistasGlobal = string.Empty;
        public string backgroundcolorsGraficoVistasGlobal = string.Empty;

        async protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                {
                    sucursals = await sucursalManager.ObtenerServicios(Session["Token"].ToString());
                    
                    InicializarControles();
                    

                }

            }
        }

        private async void InicializarControles()
        {
            try
            {
                gvSurcursales.DataSource = sucursals.ToList();
                gvSurcursales.DataBind();
                ObtenerDatosGrafico();
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

        private void ObtenerDatosGrafico()
        {
            StringBuilder script = new StringBuilder();
            StringBuilder labelsGraficoVistas = new StringBuilder();
            StringBuilder dataGraficoVistas = new StringBuilder();
            StringBuilder backgroundcolorsGraficoVistas = new StringBuilder();

            var random = new Random();

            foreach (var sucursals in sucursals.GroupBy(info => info.Ubicacion).
                Select(group => new {
                    Ubicacion = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.Ubicacion))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labelsGraficoVistas.Append(string.Format("'{0}',", sucursals.Ubicacion)); // 'Correo','frmError',
                dataGraficoVistas.Append(string.Format("'{0}',", sucursals.Cantidad)); // '2','3',
                backgroundcolorsGraficoVistas.Append(string.Format("'{0}',", color));

                labelsGraficoVistasGlobal = labelsGraficoVistas.ToString().Substring(0, labelsGraficoVistas.Length - 1);
                dataGraficoVistasGlobal = dataGraficoVistas.ToString().Substring(0, dataGraficoVistas.Length - 1);
                backgroundcolorsGraficoVistasGlobal =
                    backgroundcolorsGraficoVistas.ToString().Substring(backgroundcolorsGraficoVistas.Length - 1);
            }

        }

        protected void gvSurcursales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSurcursales.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Sucursal";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtNombre.Text = row.Cells[2].Text.Trim();
                    txtUbicacion.Text = row.Cells[1].Text.Trim();
                    ddlEstadoMant.SelectedValue = row.Cells[3].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text;
                    ltrModalMensaje.Text = "Esta seguro de eliminar el registro de la sucursal # ";
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nueva Scursal";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            ltrNombre.Visible = true;
            txtNombre.Visible = true;
            ddlEstadoMant.Enabled = true;
            txtCodigoMant.Text = string.Empty;
            ltrUbicacion.Visible = true;
            txtUbicacion.Visible = true;
            txtNombre.Text = string.Empty;
            //ltrUbicacion.Text = string.Empty;
            txtUbicacion.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await sucursalManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
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

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCodigoMant.Text))  /*Insertar*/
            {
                if (ValidarInsertar())
                {


                    Sucursal sucursal = new Sucursal()
                    {
                        Nombre = txtNombre.Text,
                        Ubicacion = txtUbicacion.Text,
                        Estado = ddlEstadoMant.SelectedValue
                    };

                    Sucursal sucursalIngresada = await sucursalManager.Ingresar(sucursal, Session["Token"].ToString());

                    /*Validar*/
                    if (!string.IsNullOrEmpty(sucursalIngresada.Nombre))
                    {
                        lblResultado.Text = "ingresada con exito";
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

            else  /*Modificar*/
            {
                if (ValidarModificar())
                {
                    Sucursal sucursal = new Sucursal()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        Nombre = txtNombre.Text,
                        Ubicacion = txtUbicacion.Text,
                        Estado = ddlEstadoMant.SelectedValue
                    };

                    Sucursal sucursalModificado = await sucursalManager.Actualizar(sucursal, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(sucursalModificado.Nombre))
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

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del nombre de la sucursal";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtUbicacion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la ubicacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Fila capacidad debe de ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtUbicacion.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Fila precio debe de ser un número";
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

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del nombre de la sucursal";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtUbicacion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la ubicacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Nombre  no debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtUbicacion.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "La Ubicacion no debe de ser un número";
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