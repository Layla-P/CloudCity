using System;
using CloudCityCakesMVC.Models.Enums;

namespace CloudCityCakesMVC.Models.DTO
{
    public class EmailOrder
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}