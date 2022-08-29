using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Net {

    public class PlatformIap : MonoBehaviour {

        private static PlatformIap _instance = null;
        public static PlatformIap Instance {
            get {
                return _instance;
            }
        }

        const string JAVA_UnityPlatformSdk = "com.tap4fun.GamePlatformSdk.UnityPlatformSdk";
        private static AndroidJavaClass _androidPluginPlatformSdk;
        public static AndroidJavaClass AndroidPluginPlatformSdk {
            get {
#if UNITY_ANDROID
                if (_androidPluginPlatformSdk == null)
                    _androidPluginPlatformSdk = JniUtils.FindClass(JAVA_UnityPlatformSdk);
#endif
                return _androidPluginPlatformSdk;
            }
        }

        SdkPayConfig _config;

        void Awake() {
            if (_instance != null) {
                Debug.LogError("Another PlatformIap has already been created previously. " + gameObject.name + " is goning to be destroyed.");
                Destroy(this);
                return;
            }

            DontDestroyOnLoad(gameObject);
            _instance = this;
        }

        void OnDestroy() {
            _instance = null;
            _androidPluginPlatformSdk = null;
        }

        protected void OnApplicationQuit() {
            _instance = null;
        }

        public void InitSdk() {
            string goName = gameObject.name;
#if UNITY_ANDROID
            JniUtils.CallStaticSafe(AndroidPluginPlatformSdk, "_PlatformSdkInit", goName);
#endif
        }

        public void InitPay(SdkPayConfig config) {
            string configString = config.ToJson();
#if UNITY_ANDROID
            JniUtils.CallStaticSafe(AndroidPluginPlatformSdk, "_PlatformSdkPayInit", configString);
#endif
        }

        public bool HasPendingTransaction() {
#if UNITY_ANDROID
           return JniUtils.CallStaticSafe<bool>(AndroidPluginPlatformSdk, "_PlatformSdkHasPendingTransaction");
#else
            return false;
#endif
        }

        public void Buy(string productId) {
            SdkPayParams parm = new SdkPayParams();
            parm.productId = productId;
            parm.payType = (int)SdkPayType.Consumable;
#if UNITY_ANDROID
            JniUtils.CallStaticSafe(AndroidPluginPlatformSdk, "_PlatformSdkPay", parm.ToJson());
#endif
        }

        public void Buy(string productId, string customData) {
            SdkPayParams parm = new SdkPayParams();
            parm.productId = productId;
            parm.orderId = customData;
            parm.payType = (int)SdkPayType.Consumable;
#if UNITY_ANDROID
            JniUtils.CallStaticSafe(AndroidPluginPlatformSdk, "_PlatformSdkPay", parm.ToJson());
#endif
        }

        public void ConfirmBuyCompleted() {
#if UNITY_ANDROID
            JniUtils.CallStaticSafe(AndroidPluginPlatformSdk, "_PlatformSdkPayConfirm");
#endif
        }

        public void ConfirmBuyCompleted(string[] productIdList) {

            if (productIdList == null) {
                return;
            }

            string comfirmParm = "";
            for (int i = 0; i < productIdList.Length; i++) {
                comfirmParm += productIdList[i];

                if (i != productIdList.Length - 1) {
                    comfirmParm += ",";
                }
            }

#if UNITY_ANDROID
            JniUtils.CallStaticSafe(AndroidPluginPlatformSdk, "_PlatformSdkPayConfirm", comfirmParm);
#endif
        }

        public void OnInitSucCallback() {
            Debug.Log("///////////////// OnInitSucCallback");
        }

        public void OnPayInitSucCallback(string message) {
            Debug.Log("///////////////// OnPayInitSucCallback Product msg : " + message);

            SdkPayConfig config = SdkPayConfig.ParseSdkPayConfig(message);

            if (config != null && config.Products.Count > 0) {

                Debug.Log("///////////////// OnPayInitSucCallback Product count: " + config.Products.Count);

                _config = config;

                //StoreObject.ExtProductInfo productInfo = new StoreObject.ExtProductInfo();
                //productInfo.storeCountry = ext.GetGameLocale();
                //productInfo.iapInfoList = new List<StoreObject.ExtIapInfo>();

                //foreach (SdkPayItem item in config.Products) {
                //    StoreObject.ExtIapInfo info = new StoreObject.ExtIapInfo();
                //    info.indentifier = item.productId;
                //    info.price = item.price;
                //    info.displayPrice = item.displayPrice;
                //    productInfo.iapInfoList.Add(info);
                //}

                //if (GameIAP.instance != null && GameIAP.instance.iap_storeObject != null) {
                //    string jsonStr = JsonConvert.SerializeObject(productInfo);
                //    GameIAP.instance.iap_storeObject.OnReceiveProductData(jsonStr);
                //}
            }

        }

        private void OnPayInitFailedCallback() {
            Debug.Log("///////////////// OnPayInitFailedCallback");
        }

        public void OnGetUnfinishedPayCallback(string message) {
            List<SdkPayResult> resultList = SdkPayResult.ParsePayResultList(message);

            Debug.Log("///////////////// OnGetUnfinishedPayCallback listCount: " + resultList.Count + " /// msg: " + message);

            if (resultList == null || resultList.Count <= 0) {
                return;
            }

            //foreach (SdkPayResult result in resultList) {

            //    if (GameIAP.instance != null && GameIAP.instance.iap_storeObject != null) {
            //        SdkPayItem item = _config.GetPayItem(result.productId);
            //        string receiptStr = result.extension;
            //        string productIdentifierStr = result.productId;
            //        string transactionIdentifierStr = result.signature;
            //        string currencyCodStr = item != null ? item.currencyCode : "";
            //        string priceStr = (item != null && !string.IsNullOrEmpty(item.price)) ? item.price.ToString() : "";

            //        Debug.LogFormat("///////////////// OnGetUnfinishedPayCallback: {0} {1}", priceStr, currencyCodStr);
            //        GameIAP.instance.iap_storeObject.transactionCompleted(receiptStr, productIdentifierStr, transactionIdentifierStr, 1, priceStr, currencyCodStr);
            //    }
            //}
        }

        public void OnPaySucCallback(string message) {
            SdkPayResult result = SdkPayResult.ParsePayResult(message);

            Debug.Log("///////////////// OnPaySucCallback: " + message);

            if (result == null) {
                Debug.LogError("The pay result data parse error!");
                return;
            }

            //if (GameIAP.instance != null && GameIAP.instance.iap_storeObject != null) {
            //    SdkPayItem item = _config.GetPayItem(result.productId);
            //    string receiptStr = result.extension;
            //    string productIdentifierStr = result.productId;
            //    string transactionIdentifierStr = result.signature;
            //    string currencyCodStr = item != null ? item.currencyCode : "";
            //    string priceStr = (item != null && !string.IsNullOrEmpty(item.price)) ? item.price.ToString() : "";

            //    Debug.LogFormat("///////////////// OnPaySucCallback2: {0} {1}", priceStr, currencyCodStr);
            //    GameIAP.instance.iap_storeObject.transactionCompleted(receiptStr, productIdentifierStr, transactionIdentifierStr, 1, priceStr, currencyCodStr);
            //}
        }
        
        private void OnPayFailedCallback(string message) {
            Debug.Log("///////////////// OnPayFailedCallback " + message);

            //if (GameIAP.instance != null && GameIAP.instance.iap_storeObject != null) {
            //    GameIAP.instance.iap_storeObject.OnTransactionFailed(message == "1" ? true : false);
            //}
        }

        private void OnConsumeSucCallback(string message) {
            
            Debug.Log("///////////////// OnConsumeSucCallback " + message);
        }

        private void OnConsumeFailedCallback(string message) {

            Debug.Log("///////////////// OnConsumeFailedCallback " + message);
        }

    }
}
