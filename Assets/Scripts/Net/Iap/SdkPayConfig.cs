using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Net {
    public enum SdkPayType {
        Consumable = 0,
        NonConsumable,
        Subscription
    }

    public class SdkPayItem {
        public string productId;
        public string price;
        public string displayPrice;
        public string currencyCode;
        public SdkPayType productPayType;

        public SdkPayItem() {
            productId = "";
            price = "";
            displayPrice = "";
            currencyCode = "";
            productPayType = SdkPayType.Consumable;
        }

        public SdkPayItem(string id, SdkPayType type) {
            productId = id;
            price = "";
            displayPrice = "";
            currencyCode = "";
            productPayType = type;
        }

        public SdkPayItem(string id, string price, string displayPrice, string curCode) {
            productId = id;
            this.price = price;
            currencyCode = curCode;
            this.displayPrice = displayPrice;
            productPayType = SdkPayType.Consumable;
        }
    }

    public class SdkPayConfig {
        private HashSet<SdkPayItem> _products = new HashSet<SdkPayItem>();
        public HashSet<SdkPayItem> Products {
            get { return _products; }
        }

        public void AddProduct(SdkPayItem item) {
            _products.Add(item);
        }

        public void AddProduct(string productId, SdkPayType type) {
            if (string.IsNullOrEmpty(productId)) {
                return;
            }

            SdkPayItem item = new SdkPayItem(productId, type);

            Products.Add(item);
        }

        public SdkPayItem GetPayItem(string id) {
            SdkPayItem payItem = null;
            foreach (SdkPayItem item in _products) {
                if (item.productId == id) {
                    payItem = item;
                    break;
                }
            }

            return payItem;
        }

        public string ToJson() {
            string jsonStr = "";
            int productCount = Products.Count;

            if (productCount > 0) {
                jsonStr = JsonConvert.SerializeObject(Products);
            }

            return jsonStr;
        }

        public static SdkPayConfig ParseSdkPayConfig(string json) {
            if (string.IsNullOrEmpty(json)) {
                return null;
            }

            List<SdkPayItem> list = JsonConvert.DeserializeObject<List<SdkPayItem>>(json); 

            SdkPayConfig config = null;

            if (list != null && list.Count > 0) {
                config = new SdkPayConfig();

                foreach (SdkPayItem item in list) {
                    config.AddProduct(item);
                }
             
            }

            return config;
        }
    }

}