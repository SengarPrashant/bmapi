using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Error
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string ErrorPath { get; set; }
        public string StackTrace { get; set; }
        public string UserDetails { get; set; }
        public string IP { get; set; }
        public DateTime? ErrorDateTime { get; set; }
    }
}
