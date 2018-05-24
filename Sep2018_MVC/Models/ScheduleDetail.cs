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
    
    public partial class ScheduleDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScheduleDetail()
        {
            this.Attendances = new HashSet<Attendance>();
        }
    
        public int id { get; set; }
        public Nullable<System.TimeSpan> BeginTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<int> Lession { get; set; }
        public string Unit_Lession { get; set; }
        public Nullable<int> FK_Learning { get; set; }
        public Nullable<int> FK_Schedule { get; set; }
        public string FK_User_GV { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual Learning Learning { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual User User { get; set; }
    }
}
