using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public static string RPWehookDevSecret { get => "qazxsw@qwerty"; }
        

        public static string CalculateSHA256(string text, string secret)
        {
            string result = "";
            var enc = Encoding.Default;
            byte[]
            baText2BeHashed = enc.GetBytes(text),
            baSalt = enc.GetBytes(secret);
            HMACSHA256 hasher = new HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            result = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
            return result;
        }
        public static bool CompareSignatures(string orderId, string paymentId, string razorPaySignature, string _secret)
        {
            var text = orderId + "|" + paymentId;
            var secret = _secret;
            var generatedSignature = CalculateSHA256(text, secret);
            if (generatedSignature == razorPaySignature)
                return true;
            else
                return false;
        }

    }
}
