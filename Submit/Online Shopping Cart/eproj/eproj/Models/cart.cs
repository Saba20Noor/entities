//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eproj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class cart
    {
        public int cart_id { get; set; }
        public Nullable<int> cust_id { get; set; }
        public Nullable<int> pro_id { get; set; }
        [Display(Name ="Quantity")]
        public Nullable<long> cqty { get; set; }
        [Display(Name ="Price")]
        public Nullable<long> total { get; set; }
   
        public virtual customer customer { get; set; }
        public virtual product product { get; set; }
    }
}
