using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
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
    public partial class frmTipoTarjeta : System.Web.UI.Page
    {
        IEnumerable<TipoTarjeta> tipoTarjet = new ObservableCollection<TipoTarjeta>();
        TipoTarjetaManager tipoTarjetaManager = new TipoTarjetaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                tipoTarjet = await tipoTarjetaManager.ObtenerTipoTarjetas(Session["Token"].ToString());
                gvTipoTarjeta.DataSource = tipoTarjet.ToList();
                gvTipoTarjeta.DataBind();
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

        protected void gvTipoTarjeta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTipoTarjeta.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar servicio";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtDescripcion.Text = row.Cells[1].Text.Trim();
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
            ltrTituloMantenimiento.Text = "Tipo Tarjeta";
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

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {        
           if (string.IsNullOrEmpty(txtCodigoMant.Text))//Insertar
             {
                if (ValidarInsertar())
                {
                    TipoTarjeta tipoTarjeta = new TipoTarjeta()
                    {
                        Descripcion = txtDescripcion.Text,
                    };

                    TipoTarjeta tipoTarjetaIngresado = await tipoTarjetaManager.Ingresar(tipoTarjeta, Session["Token"].ToString());
                    if (!string.IsNullOrEmpty(tipoTarjetaIngresado.Descripcion))
                    {
                        lblResultado.Text = "ingresado con exito";
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
                        TipoTarjeta tipoTarjeta = new TipoTarjeta()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        Descripcion = txtDescripcion.Text,
                    };

                    TipoTarjeta tipoTarjetaModificado = await tipoTarjetaManager.Actualizar(tipoTarjeta, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(tipoTarjetaModificado.Descripcion))
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

                    }
                }
            }
        }
        protected async void btnAceptarModal_Click(object sender, EventArgs e)
            {
            try
                {
                    string resultado = string.Empty;
                    resultado = await tipoTarjetaManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                    if (!string.IsNullOrEmpty(resultado))
                    {
                        lblCodigoEliminar.Text = string.Empty;
                        ltrModalMensaje.Text = " eliminado";
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

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseMantenimiento(); });", true);
        }


        //Validaciones importantes
        private bool ValidarInsertar()
        {
            if  (string.IsNullOrEmpty(txtDescripcion.Text))                                  
            {
                lblStatus.Text = "Debe ingresar la descripcion";
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
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                lblStatus.Text = "Debe ingresar la descripcion";
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

    }
}
