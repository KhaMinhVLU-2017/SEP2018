﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SEP_T06Entities1 : DbContext
    {
        public SEP_T06Entities1()
            : base("name=SEP_T06Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<diemdanh> diemdanhs { get; set; }
        public virtual DbSet<giaovien> giaoviens { get; set; }
        public virtual DbSet<giaovu> giaovus { get; set; }
        public virtual DbSet<hocki> hockis { get; set; }
        public virtual DbSet<lichday> lichdays { get; set; }
        public virtual DbSet<lop> lops { get; set; }
        public virtual DbSet<monhoc> monhocs { get; set; }
        public virtual DbSet<phong> phongs { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<session> sessions { get; set; }
        public virtual DbSet<sinhvien> sinhviens { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<thoikhoabieu> thoikhoabieux { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<usergiaovien> usergiaoviens { get; set; }
        public virtual DbSet<usergiaovu> usergiaovus { get; set; }
        public virtual DbSet<lichhoc> lichhocs { get; set; }
    }
}
