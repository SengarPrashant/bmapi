using Models.Firm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Firm
{
    public interface IDALFirm
    {
        Task<FirmPlan> GetPlan(string planCode, Boolean? isActive);
        Task<IEnumerable<PlanType>> GetPlanTypes(int planId, Boolean isActive);
        Task<IEnumerable<Service>> GetPlanServices(int planId, Boolean isActive);
    }
}
