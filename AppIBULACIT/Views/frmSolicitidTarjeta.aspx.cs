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
    public partial class frmSolicitidTarjeta : System.Web.UI.Page
    {
        IEnumerable<SolicitidTarjeta> solicitidTarjeta = new ObservableCollection<SolicitidTarjeta>();
        SolicitudTarjetaManager solicitudTarjetaManager = new SolicitudTarjetaManager();

        IEnumerable<TipoTarjeta> tipoTarjet = new ObservableCollection<TipoTarjeta>();
        TipoTarjetaManager tipoTarjetaManager = new TipoTarjetaManager();

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
                solicitidTarjeta = await solicitudTarjetaManager.ObtenerSolicitudTarjetas(Session["Token"].ToString());
                gvSolicitudTarjetas.DataSource = solicitidTarjeta.ToList();
                gvSolicitudTarjetas.DataBind();

                tipoTarjet = await tipoTarjetaManager.ObtenerTipoTarjetas(Session["Token"].ToString());
               

            //01 Pruebas DropDown
            ddlCodigoTipoTarjeta.Items.Clear();
            foreach (TipoTarjeta tipoTarjeta in tipoTarjet)
            {
            ddlCodigoTipoTarjeta.Items.Insert(0, new ListItem(tipoTarjeta.Codigo + " - " + tipoTarjeta.Descripcion, Convert.ToString(tipoTarjeta.Codigo)));           
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
                lblStatus.Text = "Hubo un error al cargar la lista de sucrusales.";
                lblStatus.Visible = true;
            }
        }

        protected void gvSolicitudTarjetas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSolicitudTarjetas.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar servicio";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtCodigoCliente.Text = row.Cells[1].Text.Trim();
                    txtFechaSolicitud.Text = row.Cells[2].Text.Trim();
                    ddlCondicionLaboral.Text = row.Cells[3].Text.Trim();
                    txtIngresoMensual.Text = row.Cells[4].Text.Trim();                
                    ddlCodigoTipoTarjeta.Text = row.Cells[5].Text.Trim();

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
            ltrTituloMantenimiento.Text = "Solictiar Tarjeta";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;                      
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            ltrCodigoCliente.Visible = true;
            txtCodigoCliente.Visible = true;
            ltrFechaSolicitud.Visible = true;
            txtFechaSolicitud.Visible = true;          
            ddlCondicionLaboral.Enabled = true;
            ltrIngresoMensual.Visible = true;
            txtIngresoMensual.Visible = true;   
            ddlCodigoTipoTarjeta.Enabled = true;
            txtCodigoMant.Text = string.Empty;
            txtCodigoCliente.Text = string.Empty;
            txtFechaSolicitud.Text  = string.Empty;         
            txtIngresoMensual.Text = string.Empty;      
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await solicitudTarjetaManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());

                if (!string.IsNullOrEmpty(resultado))
                {
                    lblCodigoEliminar.Text = string.Empty;
                    ltrModalMensaje.Text = "Servicio eliminado";
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
           if (string.IsNullOrEmpty(txtCodigoMant.Text))//Insertar
            {
              if (ValidarInsertar())
                {
                    SolicitidTarjeta solicitidTarjeta = new SolicitidTarjeta()               
                     {
                        CodigoCliente = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                        FechaSolicitud = DateTime.Now,
                        CondicionLaboral = ddlCondicionLaboral.SelectedValue,                     
                        IngresoMensual = Convert.ToDecimal(txtIngresoMensual.Text),
                         CodigoTipoTarjeta = Convert.ToInt32(ddlCodigoTipoTarjeta.SelectedValue)
                    };

                    SolicitidTarjeta solicitidTarjetaIngresado = await solicitudTarjetaManager.Ingresar(solicitidTarjeta, Session["Token"].ToString());
                    if (!string.IsNullOrEmpty(solicitidTarjetaIngresado.CondicionLaboral))
                    {
                        lblResultado.Text = "Servicio ingresado con exito";
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
                    SolicitidTarjeta solicitidTarjeta = new SolicitidTarjeta()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        CodigoCliente = Convert.ToInt32(txtCodigoCliente.Text),
                        FechaSolicitud = Convert.ToDateTime(txtFechaSolicitud.Text),
                        CondicionLaboral = ddlCondicionLaboral.SelectedValue,
                        IngresoMensual = Convert.ToDecimal(txtIngresoMensual.Text),
                        CodigoTipoTarjeta = Convert.ToInt32(ddlCodigoTipoTarjeta.SelectedValue)
                    };

                    SolicitidTarjeta solicitidTarjetaIModificado = await solicitudTarjetaManager.Actualizar(solicitidTarjeta, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(solicitidTarjetaIModificado.CondicionLaboral))
                    {
                        lblResultado.Text = "Actualizado con exito";
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

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseMantenimiento(); });", true);
        }

        //Validaciones
        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtIngresoMensual.Text))
            {
                lblStatus.Text = "Debe ingresar su Ingreso Mensual";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            if (txtIngresoMensual.Text.All(char.IsNumber) == false)
            {
                lblStatus.Text = "Ingreso Mensual debe de ser un número";
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
            if (string.IsNullOrEmpty(txtIngresoMensual.Text))
            {
                lblStatus.Text = "Debe ingresar su Ingreso Mensual";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            lblStatus.Text = "Modificado Correctamente";
            lblStatus.ForeColor = Color.Green;
            lblStatus.Visible = true;
            return true;

        }







    }
}





























