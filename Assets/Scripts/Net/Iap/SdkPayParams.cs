using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Net {
    public class SdkPayParams {
        public string productId;
        public string productName;
        public string productDesc;
        public int price;
        public int buyNum;
        public int coinNum;
        public string serverId;
        public string serverName;
        public string roleId;
        public string roleName;
        public int roleLevel;
        public string vip;
        public string orderId;
        public string extension;
        public string payNotifyUrl;
        public int payType = 0;

        public SdkPayParams() {
            productId = "";
            productName = "";
            productDesc = "";
            price = 0;
            buyNum = 0;
            coinNum = 0;
            serverId = "";
            serverName = "";
            roleId = "";
            roleName = "";
            roleLevel = 0;
            vip = "";
            orderId = "";
            extension = "";
            payNotifyUrl = "";
            payType = 0;
        }

        public string ToJson() {
            string jsonStr = JsonConvert.SerializeObject(this);
            return jsonStr;
        }
    }
}
