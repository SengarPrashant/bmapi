using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Firm
{
    public class PlanType
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Caption { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public string ShortSummary { get; set; }
        public string Services { get; set; }
        public Boolean IsActive { get; set; }
    }
}
