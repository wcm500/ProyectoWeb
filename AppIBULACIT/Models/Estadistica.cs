using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Estadistica
    {
        public int Codigo { get; set; }
        public Nullable<int> CodigoUsuario { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
        public string Navegador { get; set; }
        public string PlataformaDispositivo { get; set; }
        public string FabricanteDispostivo { get; set; }
        public string Vista { get; set; }
        public string Accion { get; set; }
    }
}