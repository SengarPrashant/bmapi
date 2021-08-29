using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IDAL.Firm;
using Models.Common;
using Dapper;
using Models.Firm;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using DAL.Common;

namespace DAL.Firm
{
    public class DalFirm : IDALFirm
    {
        public async Task<FirmPlan> GetPlan(string planCode, Boolean? isActive)
        {
            FirmPlan plans;
            try
            {
                using (var connection = new SqlConnection(DalConstants.SqlConString))
                {
                    await connection.OpenAsync();
                    plans = connection.QueryAsync<FirmPlan>("GetPlan",
                                    new { planCode, isActive },
                                    commandType: CommandType.StoredProcedure).Result.ToList().FirstOrDefault();
                }
                return plans;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Service>> GetPlanServices(int planId, bool isActive)
        {
            IEnumerable<Service> service;
            try
            {
                using (var connection = new SqlConnection(DalConstants.SqlConString))
                {
                    await connection.OpenAsync();
                    service = await connection.QueryAsync<Service>("GetPlanServices",
                                    new { planId, isActive },
                                    commandType: CommandType.StoredProcedure);
                }
                return service;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PlanType>> GetPlanTypes(int planId, bool isActive)
        {
            IEnumerable<PlanType> planTypes;
            try
            {
                using (var connection = new SqlConnection(DalConstants.SqlConString))
                {
                    await connection.OpenAsync();
                    planTypes = await connection.QueryAsync<PlanType>("GetPlanType",
                                    new { planId, isActive },
                                    commandType: CommandType.StoredProcedure);
                }
                return planTypes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
