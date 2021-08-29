using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class PageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InfoCode { get; set; }
        public string Value { get; set; }
        public Boolean IsActive { get; set; }
    }
}
