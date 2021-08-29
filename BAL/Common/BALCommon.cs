using System;
using System.Collections.Generic;
using System.Text;
using IDAL.Common;
using IBAL.Common;
using System.Threading.Tasks;
using Models.Common;

namespace BAL.Common
{
    public class BALCommon :IBALCommon
    {
        private readonly IDalCommon _dalCommon;
        public BALCommon(IDalCommon dalCommon)
        {
            _dalCommon = dalCommon;
        }

        public Task<IEnumerable<Location>> GetLocation(string searchText, int? startRow, int? pageSize, bool exact)
        {
           return _dalCommon.GetLocation(searchText, startRow, pageSize, exact);
        }

        public Task<IEnumerable<PageInfo>> GetPageInfo(string pageCode)
        {
            return _dalCommon.GetPageInfo(pageCode);
        }

        public Task<IEnumerable<PageLinks>> GetPageLinks(string pageCode)
        {
            return _dalCommon.GetPageLinks(pageCode);
        }
    }
}
