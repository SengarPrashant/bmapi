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
    }
}
