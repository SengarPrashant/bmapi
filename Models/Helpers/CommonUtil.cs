using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Helpers
{
    public static class CommonUtil
    {
        public static string GetTransactionID(string customerid, string gateway = "RP")
        {
           return gateway + "-" + customerid + "-" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Year + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
        }
        public static decimal GetRPFormatedAmount(decimal amount, string currency)
        {
           return amount * GetRPCurrencyMapping()[currency];
        }
        public static Dictionary<string,decimal> GetRPCurrencyMapping()
        {
            var mp = new Dictionary<string, decimal>();
            mp.Add("INR", 100);
            mp.Add("USD", 100);
            return mp;
        }
        public static string RPDevKey { get => "rzp_test_Skz3lw9e2y1MY9"; }
        public static string RPDevSecret { get => "t48lEpmQrRdrj56CoTSgVkv6"; }
        public static string RPProdKey { get => ""; }
        public static string RPProdSecret { get => ""; }
        
    }
}
