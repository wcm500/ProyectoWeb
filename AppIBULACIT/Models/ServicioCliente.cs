using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class ServicioCliente
    {
        

        public int CodigoServicio { get; set; }
        public int CodigoUsuario { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public string TipoAyuda { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}