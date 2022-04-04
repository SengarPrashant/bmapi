using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class CustomerOrderDetail
    {
        public int CustomerId { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PlanCode { get; set; }
        public string Details { get; set; }
        public string GateWayType { get; set; }

    }
}
