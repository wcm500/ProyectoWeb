using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Tipo_Prestamo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_Prestamo()
        {
            this.Prestamos = new HashSet<Prestamos>();
        }

        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestamos> Prestamos { get; set; }
    }
}