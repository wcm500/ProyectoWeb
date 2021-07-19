using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class TipoTarjeta
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoTarjeta()
        {
            this.SolicitidTarjeta = new HashSet<SolicitidTarjeta>();
        }

        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitidTarjeta> SolicitidTarjeta { get; set; }

    }
}