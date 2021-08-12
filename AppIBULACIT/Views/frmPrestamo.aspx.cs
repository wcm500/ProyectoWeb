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
        IEnumerable<Prestamos> prestamo = new ObservableCollection<Prestamos>();
        PrestamoManager prestamoManager = new PrestamoManager();

        IEnumerable<Tipo_Prestamo> tipoPrestamo = new ObservableCollection<Tipo_Prestamo>();
        TipoPrestamoManager tipoPrestamoManager = new TipoPrestamoManager();

        IEnumerable<Cuenta> cuentas = new ObservableCollection<Cuenta>();
        CuentaManager cuentaManager = new CuentaManager();

        IEnumerable<Sucursal> sucursals = new ObservableCollection<Sucursal>();
        SucursalManager sucursalManager = new SucursalManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }
        private async void InicializarControles()
        {

            try
            {
                prestamo = await prestamoManager.ObtenerPrestamos(Session["Token"].ToString());
                gvPrestamo.DataSource = prestamo.ToList();
                gvPrestamo.DataBind();

                tipoPrestamo = await tipoPrestamoManager.ObtenerTiposPrestamos(Session["Token"].ToString());
                sucursals = await sucursalManager.ObtenerServicios(Session["Token"].ToString());
                cuentas = await cuentaManager.ObtenerCuentas(Session["Token"].ToString());




                ddlTipoPrestamo.Items.Clear();
                foreach (Tipo_Prestamo tipoPrestamo in tipoPrestamo)
                {
                    ddlTipoPrestamo.Items.Insert(0, new ListItem(tipoPrestamo.Codigo + " - " + tipoPrestamo.Descripcion, Convert.ToString(tipoPrestamo.Codigo)));
                }

                ddlSucursal.Items.Clear();
                foreach (Sucursal sucursal in sucursals)
                {
                    ddlSucursal.Items.Insert(0, new ListItem(sucursal.Codigo + " - " + sucursal.Ubicacion, Convert.ToString(sucursal.Codigo)));
                }

                ddlCuenta.Items.Clear();
                foreach (Cuenta cuenta in cuentas)
                {
                    ddlCuenta.Items.Insert(0, new ListItem(cuenta.Codigo + " - " + cuenta.Descripcion, Convert.ToString(cuenta.Codigo)));
                }


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
                    ddlEstadoMant.Text = row.Cells[1].Text.Trim();
                    txtFechaInicio.Text = row.Cells[2].Text.Trim();
                    txtMontoPago.Text = row.Cells[3].Text.Trim();
                    ddlTipoPrestamo.Text = row.Cells[4].Text.Trim();//Verificar
                    txtInteres.Text = row.Cells[5].Text.Trim();
                    ddlCuenta.Text = row.Cells[6].Text.Trim();
                    ddlSucursal.Text = row.Cells[7].Text.Trim();
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
            ltrTituloMantenimiento.Text = "Nuevo Prestamo";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            ltrCodigoCuenta.Visible = true;
            ltrFechaInicio.Visible = true;
            ltrInteres.Visible = true;
            ltrSucursal.Visible = true;
            ltrTipoPrestamo.Visible = true;
            ddlEstadoMant.Enabled = true;
            ddlCuenta.Enabled = true;
            ddlSucursal.Enabled = true;
            ddlTipoPrestamo.Enabled = true;
            txtCodigoMant.Text = string.Empty;
            txtInteres.Text = string.Empty;
            txtMontoPago.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
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

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text))  /*Insertar*/
            {
                /// CodigoTipoTarjeta = Convert.ToInt32(ddlCodigoTipoTarjeta.SelectedValue)
                if (ValidarInsertar())
                {
                    Prestamos prestamo = new Prestamos()
                    {
                       Plazos = ddlEstadoMant.SelectedValue,
                       FechaInicio = DateTime.Now,
                       MontoPago = Convert.ToDecimal(txtMontoPago.Text),
                       TipoPrestamo = Convert.ToInt32(ddlTipoPrestamo.SelectedValue),
                       Interes = Convert.ToDecimal(txtInteres.Text),
                       CodigoCuenta = Convert.ToInt32(ddlCuenta.SelectedValue),
                       CodigoSucursal = Convert.ToInt32(ddlSucursal.SelectedValue)

                    };

                    Prestamos prestamoIngresada = await prestamoManager.Ingresar(prestamo, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(prestamoIngresada.Plazos))
                    {
                        lblResultado.Text = "Sucursal ingresada con exito";
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
                    Prestamos prestamo = new Prestamos()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        Plazos = ddlEstadoMant.SelectedValue,
                        FechaInicio = DateTime.Now,
                        MontoPago = Convert.ToDecimal(txtMontoPago.Text),
                        TipoPrestamo = Convert.ToInt32(ddlTipoPrestamo.SelectedValue),
                        Interes = Convert.ToDecimal(txtInteres.Text),
                        CodigoCuenta = Convert.ToInt32(ddlCuenta.SelectedValue),
                        CodigoSucursal = Convert.ToInt32(ddlSucursal.SelectedValue)
                    };

                    Prestamos prestamoModificado = await prestamoManager.Actualizar(prestamo, Session["Token"].ToString());
                    if (!string.IsNullOrEmpty(prestamoModificado.Plazos))
                    //txtAvionCapacidad.Text.All(char.IsNumber) == false
                    {
                        lblResultado.Text = "Sucursal ingresada con exito";
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
            /// CodigoTipoTarjeta = Convert.ToInt32(ddlCodigoTipoTarjeta.SelectedValue)
        }

        private bool ValidarInsertar()
        {

            if (txtInteres.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del nombre de la sucursal";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtMontoPago.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la ubicacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            //if (txtMontoPago.Text.All(char.IsNumber) == false)
            //{
            //    lblStatus.Text = "Fila capacidad debe de ser un número";
            //    lblStatus.ForeColor = Color.Maroon;
            //    lblStatus.Visible = true;
            //    return false;
            //}

            //if (txtInteres.Text.All(char.IsNumber) == false)
            //{
            //    lblStatus.Text = "Fila precio debe de ser un número";
            //    lblStatus.ForeColor = Color.Maroon;
            //    lblStatus.Visible = true;
            //    return false;
            //}

            lblStatus.Text = "Insertado Correctamente";
            lblStatus.ForeColor = Color.Green;
            lblStatus.Visible = true;
            return true;
        }

        private bool ValidarModificar()
        {

            if (txtMontoPago.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el monto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtInteres.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el interes";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            //if (txtMontoPago.Text.All(char.IsLetter) == true)
            //{
            //    lblStatus.Text = "El monto debe de ser un número";
            //    lblStatus.ForeColor = Color.Maroon;
            //    lblStatus.Visible = true;
            //    return false;
            //}

            //if (txtInteres.Text.All(char.IsLetter) == true)
            //{
            //    lblStatus.Text = "El interes debe de ser un número";
            //    lblStatus.ForeColor = Color.Maroon;
            //    lblStatus.Visible = true;
            //    return false;
            //}




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