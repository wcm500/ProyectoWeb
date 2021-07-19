using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class SolicitidTarjeta
    {
        public int Codigo { get; set; }
        public int CodigoCliente { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public string CondicionLaboral { get; set; }
        public decimal IngresoMensual { get; set; }
        public int CodigoTipoTarjeta { get; set; }

        public virtual TipoTarjeta TipoTarjeta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}