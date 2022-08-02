using UnityEngine;

namespace Game
{
    public static class GameConsts
    {
        //-------------------------------存档相关-------------------------------------------
        public const string RES_DESCRYPT_KEY = "x@7lzY3SW9xvYgmc";
        public const string MSG_DESCRYPT_KEY = "0123456789012345";

        public const string PERSISTENT_CLEAR_CACHE = "PERSISTENT_CLEAR_CACHE";
        public const string PERSISTENT_AUTO_SAVE = "PERSISTENT_AUTO_SAVE";

        public const float PERSISTENT_CACHE_SAVE_INTERVAL = 0.5f;
        public const float PERSISTENT_ATUO_SAVE_INTERVAL = 60;

        public const string PERSISTENT_KEY = "Player";
        //----------------------------------------------------------------------------------
        //音效变量设置
        public const string AUDIO_TAG_BGM = "Bgm";
        public const string AUDIO_TAG_SFX = "Sfx";
        //----------------------------------------------------------------------------------

        public const string USER_NAME = "username";
        public const string USER_PWD = "userpwd";
        public const string USER_ISLOGIN = "userIsLogin";
        public const string CC_DATA_NAME = "cc.data";

        public const string CMD_CHECK_RESOURCE_UPDATE = "CMD_CHECK_RESOURCE_UPDATE";
        public const string CMD_SKIP_RESOURCE_UPDATE = "CMD_SKIP_RESOURCE_UPDATE";
        public const string CMD_START_RESOURCE_UPDATE = "CMD_START_RESOURCE_UPDATED";
        public const string CMD_ALL_RESOURCE_UPDATED = "CMD_ALL_RESOURCE_UPDATED";
        public const string CMD_GAME_START = "CMD_GAME_START";
        //public const string CMD_ENTER_BATTLE_RESULT = "CMD_ENTER_BATTLE_RESULT";
        public const string CMD_LOCKSTEP_GAME_INIT = "CMD_LOCKSTEP_GAME_INIT";
        public const string CMD_LOCKSTEP_GAME_START = "CMD_LOCKSTEP_GAME_START";
        public const string CMD_LOCKSTEP_GAME_EVENT = "CMD_LOCKSTEP_GAME_EVENT";

        //public const string CMD_LOADING_PROGRESS_ADD = "CMD_LOADING_PROGRESS_ADD";
        //public const string CMD_ON_LEVEL_REMOVE = "OnLevelRemoveCommand";
        //public const string CMD_ON_LEVEL_ADD = "OnLevelAddCommand";
        //public const string CMD_ON_LEVEL_FINISH = "OnLevelFinishCommand";

        public const string CMD_RESET_PLAYER_DEPLOYED_INFO = "CMD_RESET_PLAYER_DEPLOYED_INFO";

        public const string CMD_LOGIN_GATEWAY = "CMD_LOGIN_GATEWAY";
        public const string CMD_LOGIN_GATEWAY_SUCCESS = "CMD_LOGIN_GATEWAY_SUCCESS";
        public const string CMD_READY_TO_LOGIN_GAME = "CMD_READY_TO_LOGIN_GAME";
        public const string CMD_LOGIN_GAME_SUCCESS = "CMD_LOGIN_GAME_SUCCESS";
        public const string CMD_LOGIN_GAME_FAILURE = "CMD_LOGIN_GAME_FAILURE";

        public const string CMD_BATTLE_END = "CMD_BATTLE_END";                            //战斗结束
        public const string CMD_BATTLE_SETTLEMENT = "CMD_BATTLE_SETTLEMENT";              //战斗结算
        public const string CMD_EXIT_BATTLE = "CMD_EXIT_BATTLE";                          //退出战斗
        public const string CMD_BATTLE_START = "CMD_BATTLE_START";                        //开始战斗
        public const string CMD_SCENE_PASSIVE_CONDITION = "CMD_SCENE_PASSIVE_CONDITION";  //被动条件筛选
        public const string CMD_SCENE_PASSIVE = "CMD_SCENE_PASSIVE";                      //场景被动触发                  

        public const string CMD_CLICK_MAP = "CMD_CLICK_MAP";                              //地图点击

        public const string CMD_TOWER_NEXT_LEVEL_PERFORMANCE_START = "CMD_TOWER_NEXT_LEVEL_PERFORMANCE_START";  //爬塔玩法进入下一关的表演开始
        public const string CMD_TOWER_NEXT_LEVEL_PERFORMANCE_END = "CMD_TOWER_NEXT_LEVEL_PERFORMANCE_END";      //帕坦玩法进入下一关的表演结束
        public const string CMD_MAP_FINDPATH_FINISH = "CMD_MAP_FINDPATH_FINISH";
        public const string CMD_MAP_FINDPATH_FOOTPRINT = "CMD_MAP_FINDPATH_FOOTPRINT"; // 脚印
        //public const string CMD_MAP_UPDATEPATH_LINE = "CMD_MAP_UPDATEPATH_LINE";




        // 自走棋中的打开战斗界面  数据逻辑层面消息事件
        //public const string CMD_AUTOCHESS_ON_LEVEL_ADD = "CMD_AUTOCHESS_ON_LEVEL_ADD";
        //public const string CMD_AUTOCHESS_ON_LEVEL_FINISH = "CMD_AUTOCHESS_ON_LEVEL_FINISH";

        public const string LEVEL_START = "LEVEL_START";
        public const string ADD_COMPONENT_TO_LEVEL_ENTITY = "ADD_COMPONENT_TO_LEVEL_ENTITY";
        public const string ON_LEVEL_KILLCOUNT_CHANGE = "ON_LEVEL_KILLCOUNT_CHANGE";    //当场景的击杀数发生变化

        public const string COMPLETE_DEPLOY_ROTATION = "complete_deploy_rotation";
        public const string PULL_SELF_ZOID = "pull_self_zoid";

        public const string ON_CHARACTER_HITDAMAGE_TOIL = "ON_CHARACTER_HITDAMAGE_TOIL"; //当机械兽受到伤害血条应该发生变化的时候
        public const string ON_CHARACTER_HIT = "ON_CHARACTER_HIT";                       //当机械兽或者怪物受到伤害
        public const string ON_CHARACTER_ENTITY_DESTROY = "ON_CHARACTER_ENTITY_DESTROY"; //当机械兽及怪物删除Entity
        public const string ON_LEVEL_ENTITY_VANISH = "ON_LEVEL_ENTITY_VANISH";           //用作机械兽到达了终点
        public const string ON_LEVEL_CHARACTER_DIE = "ON_LEVEL_CHARACTER_DIE";           //用作机械兽被击杀
        public const string ON_CHARACTER_RETREAT = "ON_CHARACTER_RETREAT";               //当机械兽撤离

        public const string CMD_SHOW_GATHER_REWARD = "CMD_SHOW_GATHER_REWARD";           //显示采集奖励

        public const string ON_SPRINT_START = "ON_SPRINT_START";                         //冲刺开始
        public const string ON_SPRINT_END = "ON_SPRINT_END";                             //冲刺结束

        public const string HIT_EFFECT = "HIT_EFFECT";                                   //命中的特效
        public const string ON_HIT_TARGET_BEFORE = "ON_HIT_TARGET_AFTER";                //技能命中之前
        public const string ON_HIT_TARGET = "ON_HIT_TARGET";                             //技能命中
        public const string ON_HIT_TARGET_LATER = "ON_HIT_TARGET_LATER";                 //技能命中之后

        public const string EFFECT_EXCUTE = "EFFECT_EXCUTE";
        public const string NOTDIE_AND_CURESELF = "NOTDIE_AND_CURESELF";                 //抗拒死亡并且治疗自身

        public const string PRESHAKE_EFFECT = "PRESHAKE_EFFECT";                         //技能前摇触发的效果
        public const string BUFF_EFFECT = "BUFF_EFFECT";                                 //buff的效果
        public const string REMOVE_BUFF = "REMOVE_BUFF";                                 //去除Buff效果
        public const string SUCK_BLOOD = "SUCK_BLOOD";
        public const string BREAK_SKILL_ON_LOST_TARGET = "BREAK_SKILL_ON_LOST_TARGET";   //取消一个技能的释放（在目标丢失的情况下）
        public const string REMOVE_SHIELD = "REMOVE_SHIELD";                             //移除护盾
        public const string ADD_SHIELD_UI = "ADD_SHIELD_UI";                             //添加护盾UI
        public const string Modify_SHIELD_UI = "Modify_SHIELD_UI";                       //修改护盾UI
        public const string REMOVE_SHIELD_UI = "REMOVE_SHIELD_UI";

        public const string FINAL_SKILL = "FINAL_SKILL";                                 //终结技的释放
        public const string S2C_FINAL_SKILL = "S2C_FINAL_SKILL";                         //接受到服务器广播的释放手动技能的消息
        public const string AUTO_RELEASE_SKILL = "AUTO_RELEASE_SKILL";                   //修改技能释放机制
        public const string S2C_AUTO_RELEASE_SKILL = "S2C_AUTO_RELEASE_SKILL";           //接受服务器广播的修改技能释放机制的消息
        public const string ON_ENERGY_MODIFICATION = "ON_ENERGY_MODIFICATION";           //能量值发生变更
        public const string ON_ENERGY_MODIFIHEAD = "ON_ENERGY_MODIFIHEAD";               //能量值发生变更血条变化
        public const string ON_SKILL_DONT_RELEASABLE = "ON_SKILL_DONTRELEASABLE";        //当某类技能被禁止使用
        public const string ON_SKILL_CAN_ELEASABLE = "ON_SKILL_CAN_ELEASABLE";           //当某类技能恢复使用

        //表现层面事件消息 ： 放在GameViewMediator中去处理 不影响数据逻辑
        public const string INIT_BULLET_MOUNT = "INIT_BULLET_MOUNT";                     //初始化技能挂点
        public const string CREATE_SCENE_EFFECT = "CREATE_SCENE_EFFECT";                 //全局事件创建一般类型的场景特效
        public const string SET_ENTITY_ANIMATOR = "SET_ENTITY_ANIMATOR";                 //动画事件
        public const string SET_ENTITY_CLOAKING = "SET_ENTITY_CLOAKING";                 //设置隐身效果

        public const string ADD_HALO_EFFECT = "ADD_HALO_EFFECT";
        public const string REMOVE_HALO_EFFECT = "REMOVE_HALO_EFFECT";

        public const string ON_DAMAGE_AFTER = "ON_DAMAGE_AFTER";                         //伤害之前
        public const string ON_DAMAGE_LATER = "ON_DAMAGE_LATER";                         //伤害之后

        public const string ON_RELEASE_MODE_CHANGE = "ON_RELASE_MODE_CHANGE";            //释放技能的模式发生变更

        public const string ON_QUIT_BATTLE = "ON_QUIT_BATTLE";//退出战斗

        /// <summary> 公共弹窗二级界面点击事件 </summary>
        public const string ON_UI_COMMON_TIPS_CLICK = "ON_UI_COMMON_TIPS_CLICK";

        /// <summary> 创建实体完成 </summary>
        public const string ON_ADD_ENTITY_COMPLETE = "ON_ADD_ENTITY_COMPLETE";

        public const string ON_ADD_ENTITY_COMPLETE_ATCS = "ON_ADD_ENTITY_COMPLETE_ATCS";// 自走棋创建实体完成 
        public const string ON_LEVEL_CHARACTER_DIE_ATCS = "ON_LEVEL_CHARACTER_DIE_ATCS";// 自走棋用作机械兽被击杀
        public const string ON_CHARACTER_HITDAMAGE_TOIL_ATCS = "ON_CHARACTER_HITDAMAGE_TOIL_ATCS"; // 自走棋当机械兽受到伤害血条应该发生变化的时候
        public const string BUFF_ADDIDNUM = "BUFF_ADDIDNUM";
        public const string ON_ADD_ENTITY_COMPLETE_PASSIVE = "ON_ADD_ENTITY_COMPLETE_PASSIVE";// 自走棋创建实体完成触发被动
        //攻击事件
        public const string CHARACTER_ATTACK = "CHARACTER_ATTACK";

        public const string CLICK_BATTLE_START = "CLICK_BATTLE_START";                          //点击战斗开始

        //游戏对局事件 格式_View[GameViewMediator] _Logic[GameLogicMediator] _Logic_View=================
        public const string CREATE_ZOIDSMODEL_VIEW = "CREATE_ZOIDSMODEL_VIEW";                  //创建机械兽
        public const string CREATE_BULLETMODEL_VIEW = "CREATE_BULLETMODEL_VIEW";                //创建子弹

        // 路径枚举
        public const string MAP_GIRD_EFFECT_URL = "Scenes/effect_map_grid.prefab";
        //刷新
        public const string CMD_REFRESH_STAMINA = "CMD_REFRESH_STAMINA";            //刷新体力
        public const string CMD_REFRESH_ENERGY = "CMD_REFRESH_ENERGY";              //刷新精力

        public const string CMD_UPDATE_GLOBAL_VOLUME = "CMD_UPDATE_GLOBAL_VOLUME";     //打开界面的背板黑色
        public const string CMD_UPDATE_GLOBAL_VOLUME_BLOOM = "CMD_UPDATE_GLOBAL_VOLUME_BLOOM"; // 雪天气关闭bloom

        //战斗提示消息
        public const string COMBAT_PROMPT_HIT = "COMBAT_PROMPT_HIT";
        public const string COMBAT_PROMPT_CURE = "COMBAT_PROMPT_CURE";
        public const string COMBAT_PROMPT_TIPS = "COMBAT_PROMPT_TIPS";

        //新手引导
        public const string CMD_GUIDANCE_COMPLETED = "CMD_GUIDANCE_COMPLETED";              //所有引导步骤均已完成
        public const string CMD_OPEN_GUIDANCE_VIEW = "CMD_OPEN_GUIDANCE_VIEW";              //开启引导界面
        public const string CMD_OPEN_GUIDELINES_VIEW = "CMD_OPEN_GUIDELINES_VIEW";          //开启指引界面
        public const string CMD_GUIDE_STEP_START = "CMD_GUIDE_STEP_START";                  //开始引导步骤 
        public const string CMD_GUIDE_STEP_COMPLETE = "CMD_GUIDE_STEP_COMPLETE";            //完成引导步骤
        public const string CMD_GUIDE_STEP_STATE_CHANGE = "CMD_GUIDE_STEP_STATE_CHANGE";    //引导步骤状态改变
        public const string CMD_GUIDE_PLOT_DIALOG = "CMD_GUIDE_PLOT_DIALOG";                //播放剧情对话
        public const string CMD_GUIDE_PLOT_VIDEO = "CMD_GUIDE_PLOT_VIDEO";                  //播放剧情动画
        public const string CMD_GUIDE_OBJ_CREATE = "CMD_GUIDE_OBJ_CREATE";                  //大世界物体创建生成特效
        public const string CMD_GUIDE_MOVE_CAMERA = "CMD_GUIDE_MOVE_CAMERA";                //引导 - 移动相机
        public const string CMD_GUIDE_OPERATION_UICOMPONENT = "CMD_GUIDE_OPERATION_UICOMPONENT";    //操作指定UI控件
        public const string GUIDE_CONSTS_RECONNECT_TREE = "GuideTree/GuideReconnect.asset"; //引导常量 断线重连


        public static class Input2EventType
        {
            public const int LOCKSTEP_CREATE_ZOIDS = 10;
            public const int LOCKSTEP_CREATE_BULLET = 15;
        }

        public static class MoneryEvent
        {
            /// <summary>货币更新 </summary>
            public const string CMD_REFRESH_CURRENCY_AMOUNT = "CMD_REFRESH_CURRENCY_AMOUNT";
        }

        public static class PlayerInfoEvent
        {
            public const string OPEN_PLAYER_INFO_WINDOW = "OPEN_PLAYER_INFO_WINDOW";
        }

        public static class CommonViewEvent
        {
            //响应--通用奖励获得
            public const string CMD_COMMONREEARD_RESPONSE = "CMD_COMMONREEARD_RESPONSE";
            //响应--通用奖励界面关闭
            public const string CMD_COMMONREEARD_CLOSE = "CMD_COMMONREEARD_CLOSE";
            //通用提示界面
            public const string CMD_OPEN_COMMONTIPS = "CMD_OPEN_COMMONTIPS";
            //通用错误码处理
            public const string CMD_ERROR_CODE_HANDLE = "CMD_ERROR_CODE_HANDLE";
        }

        //public static class BeastAdvanceEvent
        //{
        //    public const string ADVANCE_RESULT = "ADVANCE_RESULT";//机械兽升级/升阶/升星返回消息
        //}

        public static class ConstsKey
        {

            public const string MIN_PHY_DMG_RATE = "MinPhyDmgRate";                             //最小物理伤害系数
            public const string MIN_MGC_DMG_RATE = "MinMgcDmgRate";                             //最小法术伤害系数
            public const string CRIT_DMG_RATE = "CritDmgRate";                                  //暴击伤害倍率
            public const string CRIT_COEFFICIENT = "CritCoefficient";                           //暴击系数
            public const string DODGE_COEFFICIENT = "DodgeCoefficient";                         //闪避系数

            //---------------------------塔防OR自走棋分割线------------------------------

            //AC == AutoChess
            public const string AC_CRIT_COEFFICIENT = "ACCritCoefficient";                      //暴击公式计算系数
            public const string AC_DAMAGE_REDUCTION = "ACDamageReduction";                      //伤害减免计算系数
            public const string AC_LEVEL_CORRECTION = "ACLevelCorrection";                      //等级修正公式计算系数
            public const string AC_DAMAGE_REDUCTION_MAX_VALUE = "ACDamageReductionMaxValue";    //伤害减免最大值
            public const string AC_LEVEL_SUPPRESSION_RATE = "ACLevelSuppressionRate";           //等级压制比例
            public const string AC_LEVEL_CORRECTION_MIN_VALUE = "ACLevelCorrectionMinValue";    //等级修正最小值
            public const string AC_LEVEL_CORRECTION_MAX_VALUE = "ACLevelCorrectionMaxValue";    //等级修正最大值
            public const string AC_ATTACKSPEED_COEFFICIENT = "ACAttackSpeedCoefficient";        //攻速计算系数
            public const string AC_DODGE_COEFFICIENT = "ACDodgeCoefficient";                    //闪避公式计算系数
            //public const string HP_SCALE_PARAMETER = "HPScaleParameter";                        //血条刻度参数
            public const string SPRINT_SPD = "SprintSpd";                                       //冲刺速度参数

            public const string HP_PARAM = "HPParam";                                           //生命系数
            public const string ATTACK_PARAM = "AttackParam";                                   //攻击系数
            public const string PHYSICS_DEFENCE_PARAM = "PhysicsDefenceParam";                  //物防系数
            public const string MAGIC_DEFENCE_PARAM = "MagicDefenceParam";                      //法防系数
            public const string ATTACK_SPEED_PARAM = "AttackSpeedParam";                        //攻速系数
            public const string ATTACKRANG_PARAM = "RangParam";                                 //范围系数
            public const string PHYSICS_PENETRATE_PARAM = "PhysicsPenetrateParam";              //物理穿透系数
            public const string MAGIC_PENETRATE_PARAM = "MagicPenetrateParam";                  //法术穿透系数
            public const string CRITICAL_HIT_PARAM = "CriticalHitParam";                        //暴击系数
            public const string HIT_PARAM = "HitParam";                                         //命中系数
            public const string DODGE_PARAM = "DodgeParam";                                     //闪避系数
            public const string BLOOD_RETURN_PARAM = "BloodReturnParam";                        //生命回复系数
            public const string SUCKING_BLOOD_PARAM = "SuckingBloodParam";                      //吸血系数
            public const string BACKINJURY_PARAM = "BackinjuryParam";                           //反伤系数
            public const string ATTACK_ADDDAMAGE_PARAM = "AttackAddDamageParam";                //普攻增伤系数
            public const string SKILL_ADDDAMAGE_PARAM = "SkillAddDamageParam";                  //技能增伤系数
            public const string PHYSICS_ADDDAMAGE_PARAM = "PhysicsAddDamageParam";              //物理增伤系数
            public const string MAGIC_ADDDAMAGE_PARAM = "MagicAddDamageParam";                  //法术增伤系数
            public const string CURRENCY_ADDDAMAGE_PARAM = "CurrencyAddDamageParam";            //通用增伤系数
            public const string NORMALATTACK_REDUCEDAMAGE_PARAM = "NormalAttackReduceDamageParam";//普攻减伤系数
            public const string SKILL_REDUCEDAMAGE_PARAM = "SkillReduceDamageParam";            //技能减伤系数
            public const string CURRENTCY_REDUCEDAMAGE_PARAM = "CurrencyReduceDamageParam";     //通用减伤系数
            public const string TreatMent_Param = "CurrencyReduceDamageParam";                  //治疗加成系数



            public const string BATTLE_EDN_DELAY = "BattleEndDelay";                            //战斗结束延迟结算时长
            public const string COSUME_CEILING = "ConsumeCeiling";                              //体力上限
            public const string STRENGTHRECOVERTIMES = "StrengthRecoverTimes";                  //体力恢复时间(s)
            public const string ENERGY_CEILING = "EnergyCeiling";                               //精力上限
            public const string ENERGYRECOVERTIMES = "EnergyRecoverTimes";                      //精力恢复时间(s)

            public const string MAP_GRID_LENGTH = "MapGridLength";                              //战斗格子边长
            public const string ACGainMinusGainMinValue = "ACGainMinusGainMinValue";            //增益减益修正最小值
            public const string ACGainMinusGainMaxValue = "ACGainMinusGainMaxValue";            //增益减益修正最大值
            //public const string ACRacialRestraintCoefficient = "ACRacialRestraintCoefficient";  //种族克制系数
            public const string ACCritCorrectionMinValue = "ACCritCorrectionMinValue";          //暴伤修正最小值
            public const string ACCritCorrectionMaxValue = "ACCritCorrectionMaxValue";          //暴伤修正最大值
                                                                                                //public const string ACSkillSpeedCoefficient = "ACSkillSpeedCoefficient";            //技能急速计算系数
                                                                                                //public const string PLAYER_LEVELLIMIT_PARAM_1 = "PlayerLevelLimitParam1";           //玩家等级限制参数1
                                                                                                //public const string PLAYER_LEVELLIMIT_PARAM_2 = "PlayerLevelLimitParam2";           //玩家等级限制参数2

        }

        public static class GuideBTVarablesName
        {
            public const string TargetScene = "TargetScene";
            public const string StartReconnectStep = "StartReconnectStep";
            public const string TargetWindow = "GuidanceWindow";
            public const string TargetComponent = "TargetComponent";
            public const string InteractiveType = "InteractiveType";
            public const string Interactive = "Interactive";

            public const string HUDWindowName = "MainHUD";
            public const string NewChapterBtn = "Root/Right/ControlRoom/World";

            public const int InfrastructureType = 7;

        }

        public static class TableName
        {
            public const string MechanicalBeast = "mechanical_beast.xlsx";
            public const string MechanicalBeastValue = "mechanical_beast_value.xlsx";
            public const string MechanicalBeastAdvance = "mechanical_beast_advance.xlsx";
            public const string MechanicalBeastLevel = "mechanical_beast_level.xlsx";
            public const string MechanicalBeastStar = "mechanical_beast_star.xlsx";
            public const string Monster = "monster.xlsx";
            public const string MonsterValue = "monster_value.xlsx";
            public const string Skill = "skill.xlsx";
            public const string Model = "model.xlsx";
            public const string Effect = "effect.xlsx";
            public const string Constant = "constant.xlsx";
            public const string Constant_value = "constant_value.xlsx";
            public const string Scene = "scene.xlsx";
            public const string Icon = "icon.xlsx";
            public const string Item = "item.xlsx";
            public const string AI = "ai.xlsx";
            public const string Buff = "buff.xlsx";
            public const string BuffValue = "buff_value.xlsx";
            public const string SkillEffect = "skill_effect.xlsx";
            public const string PassiveSkill = "skill_passive.xlsx";
            public const string SkillHalo = "skill_halo.xlsx";
            public const string SkillValue = "skill_value.xlsx";
            public const string SpecialSkillValue = "special_skill_value.xlsx";
            public const string Transmit = "transmit.xlsx";
            public const string Map = "map.xlsx";
            public const string MapRule = "map_rule.xlsx";
            public const string Stage = "stage.xlsx";
            public const string StageConfig = "stageconfig.xlsx";
            public const string Fog = "fog.xlsx";
            public const string GatherRule = "gather_rule.xlsx";
            public const string Gather = "gather.xlsx";
            public const string Tool = "tool.xlsx";
            public const string StageConfigShow = "stageconfigshow.xlsx";
            public const string Weather = "weather.xlsx";
            public const string WeatherRule = "weather_rule.xlsx";
            public const string BluePrint = "blueprint.xlsx";
            public const string Attribute = "attribute.xlsx";
            public const string BagPage = "bag_page.xlsx";
            public const string Language = "language.xlsx";
            public const string Weapon = "weapon.xlsx";
            public const string WeaponRandom = "weapon_random.xlsx";
            public const string WeaponRandomAttr = "weapon_random_attr.xlsx";
            public const string WeaponValue = "weapon_value.xlsx";
            public const string Equipment = "equipment.xlsx";
            public const string EquipmentRandom = "equipment_random.xlsx";
            public const string EquipmentRandomAttr = "equipment_random_attr.xlsx";
            public const string EquipmentValue = "equipment_value.xlsx";
            public const string Resources = "resource.xlsx";
            public const string Occupation = "occupation.xlsx";
            public const string LevelMonster = "monster_info.xlsx";
            public const string Section = "task_section.xlsx";
            public const string Mission = "task_mission.xlsx";
            public const string Step = "task_step.xlsx";
            public const string TaskEvent = "task_event.xlsx";
            public const string TaskActive = "task_active.xlsx";
            public const string TaskCycle = "task_daily_weekly.xlsx";
            public const string Tower = "tower.xlsx";
            public const string Towerruler = "towerruler.xlsx";
            public const string Shop = "shop.xlsx";
            public const string Ruins = "ruins.xlsx";
            public const string PlayerExp = "player_exp.xlsx";
            public const string Fossil = "fossil.xlsx";
            public const string Fragment = "fragment.xlsx";
            public const string Npc = "npc.xlsx";
            public const string NpcMonster = "npc_monster.xlsx";
            public const string Carddrop = "carddrop.xlsx";
            public const string Sound = "sound.xlsx";
            public const string HeadPicture = "head_picture.xlsx";
            public const string GiftbagChoose = "giftbagchoose.xlsx";
            public const string GiftbagDrop = "giftbagdrop.xlsx";
            public const string DialogueCH = "dialogue.xlsx";
            public const string SkillGrow = "skill_grow.xlsx";
            public const string EquipmentAdvance = "equipment_advance.xlsx";
            public const string EquipmentLevel = "equipment_level.xlsx";
            public const string EquipmentStar = "equipment_star.xlsx";
            public const string Map_passivity = "map_passivity.xlsx";
            public const string InfraBuilding = "infrastructure_building.xlsx";
            public const string InfraSlot = "infrastructure_slot.xlsx";
            public const string InfraLevel = "infrastructure_level.xlsx";
            public const string EquipemntMake = "equipment_make.xlsx";
            public const string Rogue = "rogue.xlsx";
            public const string RogueEvent = "rogue_event.xlsx";
            public const string Fetters = "fetters.xlsx";
            public const string Excavate = "excavate.xlsx";
            public const string Guidance = "guidance.xlsx";
            public const string GuidanceStep = "guidance_step.xlsx";
            public const string SpecialStage = "special_combat_stage.xlsx";
            public const string ModelAssemblies = "model_assemblies.xlsx";
            public const string Video = "video.xlsx";
        }
    }
}
