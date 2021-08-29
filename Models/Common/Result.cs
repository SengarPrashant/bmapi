using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public object Data { get; set; }
    }
}
