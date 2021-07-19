using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Prestamos
    {
        public int Codigo { get; set; }
        public string Plazos { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public decimal MontoPago { get; set; }
        public int TipoPrestamo { get; set; }
        public decimal Interes { get; set; }
        public int CodigoCuenta { get; set; }
        public int CodigoSucursal { get; set; }

        public virtual Cuenta Cuenta { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual Tipo_Prestamo Tipo_Prestamo { get; set; }
    }
}