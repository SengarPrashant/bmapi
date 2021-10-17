using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class CustomerOrder
    {
        public string orderId { get; set; }
        public string razorpayKey { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string description { get; set; }
    }
}
