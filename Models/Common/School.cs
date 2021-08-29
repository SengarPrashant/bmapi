using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class School
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string CityDistrict { get; set; }
        public string PinCode { get; set; }
        public string GeoLocation { get; set; }
        public string ImageExt { get; set; }
        public string ImageData { get; set; }
        public string Contact { get; set; }
        public string SchoolLogo { get; set; }
        public DateTime? SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
    }
}
