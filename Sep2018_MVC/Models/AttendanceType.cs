//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sep2018_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AttendanceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceType()
        {
            this.AttendanceDetails = new HashSet<AttendanceDetail>();
        }
    
        public int id { get; set; }
        public string TypeName { get; set; }
<<<<<<< HEAD
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceDetail> AttendanceDetails { get; set; }
=======
>>>>>>> origin/master
    }
}
