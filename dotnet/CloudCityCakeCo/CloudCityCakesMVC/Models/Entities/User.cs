using System;
using System.Collections.Generic;

namespace CloudCityCakesMVC.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<CakeOrder> CakeOrders { get; set; }
    }
}