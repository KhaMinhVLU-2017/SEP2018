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
    
    public partial class Learning
    {
        public int id { get; set; }
        public Nullable<int> FK_Class { get; set; }
        public Nullable<int> FK_Subject { get; set; }
        public Nullable<int> FK_Semester { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual ScheduleDetail ScheduleDetail { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
