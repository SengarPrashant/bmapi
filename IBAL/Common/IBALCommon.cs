using Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBAL.Common
{
    public interface IBALCommon
    {
        Task<IEnumerable<PageInfo>> GetPageInfo(string codeCode);
        Task<IEnumerable<Location>> GetLocation(string? searchText, int? startRow, int? pageSize, bool exact);
        Task<IEnumerable<PageLinks>> GetPageLinks(string pageCode);
        Task<int> SaveCustomerbasicDetails(CustomerBasicDetail customer);
    }
}
