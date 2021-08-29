using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Firm
{
    public class Service
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
