using MVC;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class InitializePanel : NotifierBehaviour
    {
        public Transform panel;

        public Button confirmBtn;
        public Button cancelBtn;
        public Text confirmLabel;
        public Text cancelLabel;
        public Text content;

        public Slider progressSlider;
        public TextMeshProUGUI progressNum;
        public TextMeshProUGUI tips;

        public Image splashObject;
        public Image logo;

        public System.Action initEndCallback;
        public System.Action progressEndCallback;
        public System.Action splashEndCallback;

        private System.Action confirmCallback;
        private System.Action cancelCallback;

        private long totalSize;        //要下载资源的总量
        private float wantedProgress;

        private Dictionary<string, string> textContext = new Dictionary<string, string>();


        void Start()
        {
            panel = transform.Find("Panel").transform;
            Button btnConfirm = panel.Find("Confirm").GetComponent<Button>();
            btnConfirm.onClick.AddListener(OnConfirmClick);

            //confirmBtn = transform.Find("Panel/PopWindow/ConfirmBtn").GetComponent<Button>();
            //confirmBtn.onClick.AddListener(OnConfirmClick);

            //cancelBtn = transform.Find("Panel/PopWindow/CancelBtn").GetComponent<Button>();
            //cancelBtn.onClick.AddListener(OnCancelClick);

            //confirmLabel = transform.Find("Panel/PopWindow/ConfirmBtn/Text").GetComponent<Text>();
            //cancelLabel = transform.Find("Panel/PopWindow/CancelBtn/Text").GetComponent<Text>();
            //content = transform.Find("Panel/PopWindow/Text").GetComponent<Text>();

            //progressSlider = transform.Find("Panel/ProgressSlider").GetComponent<Slider>();
            //progressNum = transform.Find("Panel/ProgressNum").GetComponent<TextMeshProUGUI>();
            //tips = transform.Find("Panel/Tips").GetComponent<TextMeshProUGUI>();

            SetTextContext();
            //popWindow.SetActive(false);
            InitUI();
        }

        private void SetTextContext()
        {
            //if (PlayerPrefs.HasKey("Language"))
            //{
            //    var language = (Language)PlayerPrefs.GetInt("Language", 0);
            //    if (language == Language.ChineseSimplified)
            //    {
            //        var context = Resources.Load<TextAsset>("cn");
            //        textContext = JsonTools.DeserializeObject<Dictionary<string, string>>(context.text);
            //        return;
            //    }
            //}
            //if (Application.systemLanguage == SystemLanguage.ChineseSimplified)
            //{
            //    var context = Resources.Load<TextAsset>("cn");
            //    textContext = JsonTools.DeserializeObject<Dictionary<string, string>>(context.text);
            //}
            //else
            //{
            //    var context = Resources.Load<TextAsset>("en");
            //    textContext = JsonTools.DeserializeObject<Dictionary<string, string>>(context.text);
            //}
        }

        private void InitUI()
        {
            //tips.text = GetTextWithKey("initialpanel_text_1");
        }

        private string GetTextWithKey(string key)
        {
            textContext.TryGetValue(key, out var str);
            return str;
        }

        public void OnConfirmClick()
        {
            panel.gameObject.SetActive(false);
            confirmCallback?.Invoke();
        }

        public void OnCancelClick()
        {
            panel.gameObject.SetActive(false);
            cancelCallback?.Invoke();
        }

        public void Pop(string content, System.Action OnConfirm, string confirmLabel, System.Action OnCancel, string cancelLabel)
        {
            panel.gameObject.SetActive(true);

            confirmCallback = OnConfirm;
            cancelCallback = OnCancel;

            this.content.text = GetTextWithKey(content);
            this.confirmLabel.text = GetTextWithKey(confirmLabel);
            this.cancelLabel.text = GetTextWithKey(cancelLabel);
        }

        public void SetTotalSize(long size)
        {
            totalSize = size;
        }

        public void SetProgress(string tips, float progress)
        {
            this.tips.text = GetTextWithKey(tips);
            if (wantedProgress < progress)
                wantedProgress = progress;
        }

        public void SetProgressNum(string tips, float progress)
        {
            float totalMB = GetProgress(totalSize);
            float curMB = GetProgress(totalSize * progress);
            progressNum.text = curMB.ToString("00") + "/" + totalMB.ToString("00") + "M";
            SetProgress(tips, progress);
        }

        //根据要下载的大小返回要下载的MB值
        public float GetProgress(float size)
        {
            float needKB = size / 1024;
            float needMB = needKB / 1024;
            return needMB;
        }

        public void ResetProgress()
        {
            progressSlider.value = 0;
            wantedProgress = 0;
        }

        public void ResetTotalSize()
        {
            totalSize = 0;
            progressNum.text = "";
        }

        private void Update()
        {
            //progressSlider.value = Mathf.MoveTowards(progressSlider.value, wantedProgress, Time.deltaTime);

            //if (progressEndCallback != null && progressSlider.value >= 0.99f)
            //{
            //    progressEndCallback.Invoke();
            //    progressEndCallback = null;
            //}

        }

        public void OnSplashEnd()
        {
            splashObject.gameObject.SetActive(false);
            splashEndCallback?.Invoke();
        }

        public void OnInitializedEnd()
        {
            gameObject.SetActive(false);
            initEndCallback?.Invoke();
        }
    }
}
