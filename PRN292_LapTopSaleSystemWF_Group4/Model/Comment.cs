//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRN292_LapTopSaleSystemWF_Group4.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public System.DateTime date { get; set; }
        public bool Accept { get; set; }
        public bool Active { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ProductID { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

        public Comment(string content, DateTime date, bool accept, bool active, int? userID, int? productID)
        {
            Content = content;
            this.date = date;
            Accept = accept;
            Active = active;
            UserID = userID;
            ProductID = productID;
        }
    }
}
