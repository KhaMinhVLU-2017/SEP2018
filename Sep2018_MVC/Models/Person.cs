
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
    
public partial class Person
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Person()
    {

        this.Reports = new HashSet<Report>();

    }


    public int id { get; set; }

    public string MS { get; set; }

    public string Name { get; set; }

    public string HomeTown { get; set; }

    public Nullable<System.DateTime> Birthday { get; set; }

    public string Email { get; set; }

    public Nullable<int> PhoneNumber { get; set; }

    public string CurrentResidence { get; set; }

    public Nullable<bool> Gender { get; set; }



    public virtual User User { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Report> Reports { get; set; }

}

}
