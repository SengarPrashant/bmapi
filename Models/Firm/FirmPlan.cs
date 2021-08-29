using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Firm
{
    public class FirmPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<PlanType> PlanTypeList { get; set; }
        public IEnumerable<Service> ServiceList { get; set; }
    }
}
