using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.Common;

namespace IDAL.Common
{
    public interface IDalCommon
    {
        Task<IEnumerable<PageInfo>> GetPageInfo(string codeCode);
        Task<IEnumerable<Location>> GetLocation(string? searchText, int? startRow, int? pageSize, bool exact);
        Task<IEnumerable<PageLinks>> GetPageLinks(string pageCode);
    }
}
