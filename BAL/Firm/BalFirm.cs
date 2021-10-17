using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using IBAL.Firm;
using Models.Firm;
using IDAL.Firm;

namespace BAL.Firm
{
    public class BalFirm : IBalFirm
    {
        private readonly IDALFirm _dalFirm;
        public BalFirm(IDALFirm dalFirm)
        {
            _dalFirm = dalFirm;
        }
        public async Task<FirmPlan> GetPlan(string planCode, bool? isActive)
        {
            try
            {
                var plan = await _dalFirm.GetPlan(planCode, isActive);
                if(plan != null)
                {
                    var planTyps = await _dalFirm.GetPlanTypes(plan.Id, true);
                    var planServices = await _dalFirm.GetPlanServices(plan.Id, true);
                    plan.PlanTypeList = planTyps;
                    plan.ServiceList = planServices;
                }
                return plan;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PlanType>> GetPlanTypes(int planId, bool isActive, int planTypeID = 0)
        {
            try
            {
                IEnumerable<PlanType> planTyps = await _dalFirm.GetPlanTypes(planId, isActive);
                if(planTypeID > 0)
                {
                   return planTyps.ToList().Where(item => item.Id == planTypeID).ToList();
                }
                return planTyps.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
