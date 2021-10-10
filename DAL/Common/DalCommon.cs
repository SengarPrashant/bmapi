using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IDAL.Common;
using Models.Common;

namespace DAL.Common
{
    public class DalCommon : IDalCommon
    {
        public async Task<IEnumerable<PageInfo>> GetPageInfo(string pageCode)
        {
            IEnumerable<PageInfo> pageInfo;
            try
            {
                using (var connection = new SqlConnection(DalConstants.SqlConString))
                {
                    await connection.OpenAsync();
                    pageInfo = await connection.QueryAsync<PageInfo>("GetPageInfo", new { pageCode },commandType: CommandType.StoredProcedure);
                }
                return pageInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<PageLinks>> GetPageLinks(string pageCode)
        {
            IEnumerable<PageLinks> pageLinks;
            try
            {
                using (var connection = new SqlConnection(DalConstants.SqlConString))
                {
                    await connection.OpenAsync();
                    pageLinks = await connection.QueryAsync<PageLinks>("GetPageLinks", new { pageCode }, commandType: CommandType.StoredProcedure);
                }
                return pageLinks;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Location>> GetLocation(string? searchText,int? startRow, int? pageSize, bool exact)
        {
            IEnumerable<Location> locations;
            try
            {
                var p = new DynamicParameters();
                if (!string.IsNullOrEmpty(searchText))
                {
                    p.Add("@searchText", searchText);
                }
                if(startRow != null)
                {
                    p.Add("@startRow", startRow);
                }
                if (pageSize != null)
                {
                    p.Add("@pageSize", pageSize);
                }
                p.Add("@exact", exact);
                using (var connection = new SqlConnection(DalConstants.SqlConString))
                {
                    await connection.OpenAsync();
                    locations = await connection.QueryAsync<Location>("GetLocation", p, commandType: CommandType.StoredProcedure);
                }
                return locations;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SaveCustomerbasicDetails(CustomerBasicDetail customer)
        {
            try
            {
                IEnumerable<int> customerID;
                var p = new DynamicParameters();
                p.Add("@Name", customer.Name);
                p.Add("@Email", customer.Email);
                p.Add("@Mobile", customer.Mobile);
                p.Add("@BusinessName", customer.BusinessName);
                p.Add("@Gstin", customer.Gstin);
                p.Add("@Country", customer.Country);
                p.Add("@State", customer.State);
                using (var connection = new SqlConnection(DalConstants.SqlConString))
                {
                    await connection.OpenAsync();
                    customerID = await connection.QueryAsync<int>("saveCustomerBasicDetail", p, commandType: CommandType.StoredProcedure);
                }
                return customerID.AsList<int>().Count > 0 ? customerID.AsList<int>()[0] : 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
