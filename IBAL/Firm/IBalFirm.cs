using Models.Firm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBAL.Firm
{
    public interface IBalFirm
    {
        Task<FirmPlan> GetPlan(string planCode, Boolean? isActive);
        Task<IEnumerable<PlanType>> GetPlanTypes(int planId, bool isActive, int planTypeID=0);
    }
}
