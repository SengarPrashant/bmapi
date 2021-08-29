using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class Location
    {
        public int Id { get; set; }
        public string SubDistrictName { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public bool IsActive { get; set; }
    }
}
