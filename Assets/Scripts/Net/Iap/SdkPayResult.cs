using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Net {
    public class SdkPayResult {
        public string productId;
        public string orderId;
        public string productName;
        public string extension;
        public string signature;

        public static SdkPayResult ParsePayResult(string msg) {
            SdkPayResult pr = JsonConvert.DeserializeObject<SdkPayResult>(msg);
            return pr; 
        }

        public static List<SdkPayResult> ParsePayResultList(string msg) {
            List<SdkPayResult> arrlist = JsonConvert.DeserializeObject<List<SdkPayResult>>(msg);
            return arrlist;
        }
    }
}
