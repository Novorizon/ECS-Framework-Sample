using MVC.Extensions;
using MVC.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HudWindow : UIWindow
    {
        private Button buttonTime;
        private TextMeshProUGUI textTime;
        Image imageHeadIcon;
        //private GameObject taskOpenImg, taskCloseImg, taskScrollViewObj, taskItem;
        //private UILoopScrollRect taskItemScrollView;

        private HeroProxy heroProxy;

        //private Dictionary<GameObject, TaskHudItemComponent> taskItemComs = new Dictionary<GameObject, TaskHudItemComponent>();

        protected override void OnCreate(GameObject gameObject, object userdata)
        {
            base.OnCreate(gameObject, userdata);
            heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;

            textTime = transform.Find("Root/Top/Time/Label").GetComponent<TextMeshProUGUI>();
            imageHeadIcon = transform.Find("Root/LeftTop/PlayerInfo/Head/Head/Icon").GetComponent<Image>();
            imageHeadIcon.LoadSprite("ui_beast_levelup_shangchengjiantou");
            //titleBtn.onClick.AddListener(OnTitleBtnClick);
            //titleText = titleBtn.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            //taskOpenImg = transform.Find("Panel/TitleBtn/TaskOpenImg").gameObject;
            //taskCloseImg = transform.Find("Panel/TitleBtn/TaskCloseImg").gameObject;
            //taskScrollViewObj = transform.Find("Panel/TaskScrollView").gameObject;
            //taskItemScrollView = taskScrollViewObj.GetComponent<UILoopScrollRect>();
            //taskItem = taskScrollViewObj.transform.Find("Viewport/Content/TaskHudItem").gameObject;

            //taskItem.gameObject.SetActive(false);

            InitUI();

            EnableUpdate(true);
        }


        protected override void OnUpdate(float deltaTime)
        {
            string str = DateTime.Now.ToLongTimeString();
            textTime.text = str.Substring(0, 8);
        }
        protected override void OnDelete()
        {
            buttonTime.onClick.RemoveAllListeners();
            EnableUpdate(false);
            //ClearItemData();
            base.OnDelete();
        }

        private void ClearItemData()
        {
            //taskItemScrollView.Clear();
            //taskItemScrollView.OnCreateItem -= OnCreateServerIdItem;
            //taskItemScrollView.OnDeleteItem -= OnDeleteServerIdItem;
            //foreach (var item in taskItemComs)
            //{
            //    item.Value.gameObject.Recycle();
            //    item.Value.Destroy();
            //}
            //taskItemComs.Clear();
        }

        private void InitUI()
        {
            //titleText.text = heroProxy.GetLocalization("task_label_13");
            //SetHudOpenImg();
            //RefreshUI();
        }
        public void RefreshQuestUI(QuestState state)
        {

        }

        public void RefreshUI()
        {
            //ClearItemData();
            //taskItemScrollView.OnCreateItem += OnCreateServerIdItem;
            //taskItemScrollView.OnDeleteItem += OnDeleteServerIdItem;

            //var curSectionVO = taskProxy.GetCurSection();
            //if (curSectionVO == null)
            //{
            //    LogManager.Warning("没章节了 要展示解锁章节提示");
            //    curSectionVO = taskProxy.GetNextSection();
            //    taskItemScrollView.AddItemWrap(taskItem, curSectionVO);
            //    return;
            //}
            //if (curSectionVO == null)
            //{
            //    LogManager.Warning("全部章节任务打完");
            //    return;
            //}

            //taskItemScrollView.AddItemWrap(taskItem, curSectionVO);

            //var curMissionVO = taskProxy.GetCurMission();
            //if (curMissionVO == null)
            //{
            //    LogManager.Warning("只有章节 没有任务了");
            //    return;
            //}
            //taskItemScrollView.AddItemWrap(taskItem, curMissionVO);

            //TODO 支线任务以及其他 如果需要加在这里
        }

        private void OnCreateServerIdItem(GameObject item, object userdata)
        {
            //TaskHudItemComponent com = (TaskHudItemComponent)AddComponent(typeof(TaskHudItemComponent).FullName, item, userdata);
            //taskItemComs.Add(item, com);
        }

        private void OnDeleteServerIdItem(GameObject item, object userdata)
        {
            //if (taskItemComs.TryGetValue(item, out var component))
            //{
            //    component.Destroy();
            //    taskItemComs.Remove(item);
            //}
        }

        private void SetHudOpenImg()
        {
            //taskOpenImg.SetActive(!taskProxy.isCloseTaskHud);
            //taskCloseImg.SetActive(taskProxy.isCloseTaskHud);
            //taskScrollViewObj.SetActive(!taskProxy.isCloseTaskHud);
        }

        private void OnTitleBtnClick()
        {
            //taskProxy.isCloseTaskHud = !taskProxy.isCloseTaskHud;
            //SetHudOpenImg();
        }
    }

    //public class TaskHudItemComponent : UIComponent
    //{
    //    private GameObject taskUnlockBg, taskRewardBg, taskUnlockIcon, taskRewardIcon, sectionWdt, missionWdt;
    //    private TextMeshProUGUI name, progressText, progressNum, missionText, nextTips;
    //    private Button itemBtn, rewardBtn;
    //    private Image progressFg;

    //    private LocalizationProxy localProxy;
    //    private TaskProxy taskProxy;
    //    private SectionVO curSectionVO;
    //    private MissionVO curMissionVO;

    //    protected override void OnCreate(GameObject gameObject, object userdata)
    //    {
    //        base.OnCreate(gameObject, userdata);
    //        localProxy = Facade.RetrieveProxy(LocalizationProxy.NAME) as LocalizationProxy;
    //        taskProxy = ILGameEntry.Instance.RetrieveProxy(TaskProxy.NAME) as TaskProxy;

    //        taskUnlockBg = transform.Find("Task_Unlock_Bg").gameObject;
    //        taskRewardBg = transform.Find("Task_Reward_Bg").gameObject;
    //        taskUnlockIcon = transform.Find("Task_Unlock_Icon").gameObject;
    //        taskRewardIcon = transform.Find("Task_Reward_Icon").gameObject;
    //        sectionWdt = transform.Find("SectionWdt").gameObject;
    //        progressFg = transform.Find("SectionWdt/ProgressBg/ProgressFg").GetComponent<Image>();
    //        progressText = transform.Find("SectionWdt/ProgressImg/ProgressText").GetComponent<TextMeshProUGUI>();
    //        progressNum = transform.Find("SectionWdt/ProgressImg/ProgressNum").GetComponent<TextMeshProUGUI>();
    //        nextTips = transform.Find("NextTips").GetComponent<TextMeshProUGUI>();
    //        missionWdt = transform.Find("MissionWdt").gameObject;
    //        missionText = transform.Find("MissionWdt/MissionText").GetComponent<TextMeshProUGUI>();
    //        name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    //        itemBtn = transform.GetComponent<Button>();
    //        itemBtn.onClick.AddListener(OnItemBtnClick);
    //        rewardBtn = transform.Find("RewardBtn").GetComponent<Button>();
    //        rewardBtn.onClick.AddListener(OnRewardBtnClick);

    //        InitUI();
    //        if (userdata is SectionVO sectionVO)
    //        {
    //            curSectionVO = sectionVO;
    //            InitSectionUI();
    //        }
    //        else if (userdata is MissionVO missionVO)
    //        {
    //            curMissionVO = missionVO;
    //            InitMissionUI();
    //        }
    //    }

    //    protected override void OnDelete()
    //    {
    //        itemBtn.onClick.RemoveAllListeners();
    //        rewardBtn.onClick.RemoveAllListeners();
    //        curSectionVO = null;
    //        curMissionVO = null;
    //        base.OnDelete();
    //    }

    //    private void InitUI()
    //    {
    //        nextTips.gameObject.SetActive(false);
    //    }

    //    private void OnRewardBtnClick()
    //    {
    //        if (curSectionVO != null)
    //            taskProxy.GetRewardToServer(section_reward_type.Section, curSectionVO.sectionConf.ID);
    //        else if (curMissionVO != null)
    //            taskProxy.GetRewardToServer(section_reward_type.Mission, curMissionVO.missionConf.ID);
    //    }

    //    private void OnItemBtnClick()
    //    {
    //        if (curSectionVO != null)
    //        {
    //            UIManager.Instance.OpenWindow(ILUIConfig.TaskPanel, TaskType.SECTION);
    //        }
    //        else if (curMissionVO != null)
    //        {
    //            UIManager.Instance.OpenWindow(ILUIConfig.TaskPanel, TaskType.MAINLINE);
    //        }
    //    }

    //    private void InitSectionUI()
    //    {
    //        if (curSectionVO == null) return;
    //        SetUIState(curSectionVO.taskType);
    //        sectionWdt.SetActive(true);
    //        float progress = taskProxy.GetSectionProgress(curSectionVO);
    //        progressFg.fillAmount = progress;
    //        progressText.text = localProxy.GetLocalization("task_label_16");
    //        progressNum.text = (progress * 100).ToString("0") + "%";
    //        missionWdt.SetActive(false);
    //        name.text = localProxy.GetLocalization("task_map_1") + localProxy.GetLocalization(curSectionVO.sectionConf.SecName);
    //        name.color = new Color32(212, 148, 0, 255);

    //        if (curSectionVO.taskType == completeStatus.Locked)
    //        {
    //            nextTips.gameObject.SetActive(true);
    //            sectionWdt.SetActive(false);
    //            int needlevel = 0;
    //            foreach (var item in curSectionVO.sectionConf.UnlockRequier)
    //            {
    //                if ((SectionUnLockType)item.Key == SectionUnLockType.PLAYERLEVEL)
    //                    needlevel = item.Value;
    //            }
    //            nextTips.text = needlevel == 0 ? localProxy.GetLocalization("task_label_19") : localProxy.GetLocalization("task_label_17") + " , " + localProxy.GetLocalization("task_label_18") + needlevel;
    //        }
    //    }

    //    private void InitMissionUI()
    //    {
    //        if (curMissionVO == null) return;
    //        SetUIState(curMissionVO.taskType);
    //        sectionWdt.SetActive(false);
    //        missionWdt.SetActive(true);
    //        name.text = localProxy.GetLocalization("task_map_2") + localProxy.GetLocalization(curMissionVO.missionConf.Name);
    //        name.color = new Color32(88, 194, 255, 255);
    //        var stepVO = taskProxy.GetCurStep();
    //        if (stepVO == null)
    //            LogManager.Warning("当前无步骤可展示,需要程序检查");
    //        else
    //            missionText.text = "[" + localProxy.GetLocalization("task_label_9", taskProxy.GetMissionProgress(stepVO.stepConf.ID)) + "]" + taskProxy.GetStepProgress(stepVO.stepConf.StepTarget, stepVO.stepConf.StepDes, stepVO.stepConf.TargetValue, stepVO.questSchedule);
    //    }

    //    private void SetUIState(completeStatus curTaskType)
    //    {
    //        taskUnlockBg.SetActive(curTaskType != completeStatus.Completed);
    //        taskRewardBg.SetActive(curTaskType == completeStatus.Completed);
    //        taskUnlockIcon.SetActive(curTaskType != completeStatus.Completed);
    //        taskRewardIcon.SetActive(curTaskType == completeStatus.Completed);
    //        rewardBtn.gameObject.SetActive(curTaskType == completeStatus.Completed);
    //    }

    //    public void RefreshUI()
    //    {
    //        InitSectionUI();
    //        InitMissionUI();
    //    }
    //}
}