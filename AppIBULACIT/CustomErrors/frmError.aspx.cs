using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.CustomErrors
{
    public partial class frmError : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Exception error = Session["LastError"] as Exception;

            if (error != null)
            {
                error = error.GetBaseException();
                lblError.Text = error.Message;
                Session["LastError"] = null;

                ErrorManager errorManager = new ErrorManager();
                Error errorAPT = new Error()
                {
                    CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "Servicio.aspx",
                    Accion = "InicializarControles()",
                    Fuente = error.Source,
                    Numero = error.HResult.ToString(),
                    Descripcion = error.Message
                };

                Error errorIngresado = await errorManager.Ingresar(errorAPT);
            }

        }
    }
}