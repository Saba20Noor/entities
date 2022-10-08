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
    
    public partial class order_detail
    {
        public int order_id { get; set; }
        public string full_name { get; set; }
        public Nullable<int> pay_id { get; set; }
        public Nullable<int> creditcard_no { get; set; }
        public Nullable<System.DateTime> order_date { get; set; }
        public Nullable<System.DateTime> expire_date { get; set; }
        public Nullable<long> amount { get; set; }
        public Nullable<int> quantity { get; set; }
        public string order_status { get; set; }
        public Nullable<int> zipcode { get; set; }
        public string deliver_address { get; set; }
        public Nullable<long> contact_no { get; set; }
        public Nullable<int> cust_id { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual payment_method payment_method { get; set; }
    }
}