//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Workshop_5_Group_3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reward
    {
        public Reward()
        {
            this.Customers_Rewards = new HashSet<Customers_Rewards>();
        }
    
        public int RewardId { get; set; }
        public string RwdName { get; set; }
        public string RwdDesc { get; set; }
    
        public virtual ICollection<Customers_Rewards> Customers_Rewards { get; set; }
    }
}
