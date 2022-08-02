using UnityEngine;

namespace Game
{
    public static class GameConsts
    {
        //-------------------------------�浵���-------------------------------------------
        public const string RES_DESCRYPT_KEY = "x@7lzY3SW9xvYgmc";
        public const string MSG_DESCRYPT_KEY = "0123456789012345";

        public const string PERSISTENT_CLEAR_CACHE = "PERSISTENT_CLEAR_CACHE";
        public const string PERSISTENT_AUTO_SAVE = "PERSISTENT_AUTO_SAVE";

        public const float PERSISTENT_CACHE_SAVE_INTERVAL = 0.5f;
        public const float PERSISTENT_ATUO_SAVE_INTERVAL = 60;

        public const string PERSISTENT_KEY = "Player";
        //----------------------------------------------------------------------------------
        //��Ч��������
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

        public const string CMD_BATTLE_END = "CMD_BATTLE_END";                            //ս������
        public const string CMD_BATTLE_SETTLEMENT = "CMD_BATTLE_SETTLEMENT";              //ս������
        public const string CMD_EXIT_BATTLE = "CMD_EXIT_BATTLE";                          //�˳�ս��
        public const string CMD_BATTLE_START = "CMD_BATTLE_START";                        //��ʼս��
        public const string CMD_SCENE_PASSIVE_CONDITION = "CMD_SCENE_PASSIVE_CONDITION";  //��������ɸѡ
        public const string CMD_SCENE_PASSIVE = "CMD_SCENE_PASSIVE";                      //������������                  

        public const string CMD_CLICK_MAP = "CMD_CLICK_MAP";                              //��ͼ���

        public const string CMD_TOWER_NEXT_LEVEL_PERFORMANCE_START = "CMD_TOWER_NEXT_LEVEL_PERFORMANCE_START";  //�����淨������һ�صı��ݿ�ʼ
        public const string CMD_TOWER_NEXT_LEVEL_PERFORMANCE_END = "CMD_TOWER_NEXT_LEVEL_PERFORMANCE_END";      //��̹�淨������һ�صı��ݽ���
        public const string CMD_MAP_FINDPATH_FINISH = "CMD_MAP_FINDPATH_FINISH";
        public const string CMD_MAP_FINDPATH_FOOTPRINT = "CMD_MAP_FINDPATH_FOOTPRINT"; // ��ӡ
        //public const string CMD_MAP_UPDATEPATH_LINE = "CMD_MAP_UPDATEPATH_LINE";




        // �������еĴ�ս������  �����߼�������Ϣ�¼�
        //public const string CMD_AUTOCHESS_ON_LEVEL_ADD = "CMD_AUTOCHESS_ON_LEVEL_ADD";
        //public const string CMD_AUTOCHESS_ON_LEVEL_FINISH = "CMD_AUTOCHESS_ON_LEVEL_FINISH";

        public const string LEVEL_START = "LEVEL_START";
        public const string ADD_COMPONENT_TO_LEVEL_ENTITY = "ADD_COMPONENT_TO_LEVEL_ENTITY";
        public const string ON_LEVEL_KILLCOUNT_CHANGE = "ON_LEVEL_KILLCOUNT_CHANGE";    //�������Ļ�ɱ�������仯

        public const string COMPLETE_DEPLOY_ROTATION = "complete_deploy_rotation";
        public const string PULL_SELF_ZOID = "pull_self_zoid";

        public const string ON_CHARACTER_HITDAMAGE_TOIL = "ON_CHARACTER_HITDAMAGE_TOIL"; //����е���ܵ��˺�Ѫ��Ӧ�÷����仯��ʱ��
        public const string ON_CHARACTER_HIT = "ON_CHARACTER_HIT";                       //����е�޻��߹����ܵ��˺�
        public const string ON_CHARACTER_ENTITY_DESTROY = "ON_CHARACTER_ENTITY_DESTROY"; //����е�޼�����ɾ��Entity
        public const string ON_LEVEL_ENTITY_VANISH = "ON_LEVEL_ENTITY_VANISH";           //������е�޵������յ�
        public const string ON_LEVEL_CHARACTER_DIE = "ON_LEVEL_CHARACTER_DIE";           //������е�ޱ���ɱ
        public const string ON_CHARACTER_RETREAT = "ON_CHARACTER_RETREAT";               //����е�޳���

        public const string CMD_SHOW_GATHER_REWARD = "CMD_SHOW_GATHER_REWARD";           //��ʾ�ɼ�����

        public const string ON_SPRINT_START = "ON_SPRINT_START";                         //��̿�ʼ
        public const string ON_SPRINT_END = "ON_SPRINT_END";                             //��̽���

        public const string HIT_EFFECT = "HIT_EFFECT";                                   //���е���Ч
        public const string ON_HIT_TARGET_BEFORE = "ON_HIT_TARGET_AFTER";                //��������֮ǰ
        public const string ON_HIT_TARGET = "ON_HIT_TARGET";                             //��������
        public const string ON_HIT_TARGET_LATER = "ON_HIT_TARGET_LATER";                 //��������֮��

        public const string EFFECT_EXCUTE = "EFFECT_EXCUTE";
        public const string NOTDIE_AND_CURESELF = "NOTDIE_AND_CURESELF";                 //��������������������

        public const string PRESHAKE_EFFECT = "PRESHAKE_EFFECT";                         //����ǰҡ������Ч��
        public const string BUFF_EFFECT = "BUFF_EFFECT";                                 //buff��Ч��
        public const string REMOVE_BUFF = "REMOVE_BUFF";                                 //ȥ��BuffЧ��
        public const string SUCK_BLOOD = "SUCK_BLOOD";
        public const string BREAK_SKILL_ON_LOST_TARGET = "BREAK_SKILL_ON_LOST_TARGET";   //ȡ��һ�����ܵ��ͷţ���Ŀ�궪ʧ������£�
        public const string REMOVE_SHIELD = "REMOVE_SHIELD";                             //�Ƴ�����
        public const string ADD_SHIELD_UI = "ADD_SHIELD_UI";                             //��ӻ���UI
        public const string Modify_SHIELD_UI = "Modify_SHIELD_UI";                       //�޸Ļ���UI
        public const string REMOVE_SHIELD_UI = "REMOVE_SHIELD_UI";

        public const string FINAL_SKILL = "FINAL_SKILL";                                 //�սἼ���ͷ�
        public const string S2C_FINAL_SKILL = "S2C_FINAL_SKILL";                         //���ܵ��������㲥���ͷ��ֶ����ܵ���Ϣ
        public const string AUTO_RELEASE_SKILL = "AUTO_RELEASE_SKILL";                   //�޸ļ����ͷŻ���
        public const string S2C_AUTO_RELEASE_SKILL = "S2C_AUTO_RELEASE_SKILL";           //���ܷ������㲥���޸ļ����ͷŻ��Ƶ���Ϣ
        public const string ON_ENERGY_MODIFICATION = "ON_ENERGY_MODIFICATION";           //����ֵ�������
        public const string ON_ENERGY_MODIFIHEAD = "ON_ENERGY_MODIFIHEAD";               //����ֵ�������Ѫ���仯
        public const string ON_SKILL_DONT_RELEASABLE = "ON_SKILL_DONTRELEASABLE";        //��ĳ�༼�ܱ���ֹʹ��
        public const string ON_SKILL_CAN_ELEASABLE = "ON_SKILL_CAN_ELEASABLE";           //��ĳ�༼�ָܻ�ʹ��

        //���ֲ����¼���Ϣ �� ����GameViewMediator��ȥ���� ��Ӱ�������߼�
        public const string INIT_BULLET_MOUNT = "INIT_BULLET_MOUNT";                     //��ʼ�����ܹҵ�
        public const string CREATE_SCENE_EFFECT = "CREATE_SCENE_EFFECT";                 //ȫ���¼�����һ�����͵ĳ�����Ч
        public const string SET_ENTITY_ANIMATOR = "SET_ENTITY_ANIMATOR";                 //�����¼�
        public const string SET_ENTITY_CLOAKING = "SET_ENTITY_CLOAKING";                 //��������Ч��

        public const string ADD_HALO_EFFECT = "ADD_HALO_EFFECT";
        public const string REMOVE_HALO_EFFECT = "REMOVE_HALO_EFFECT";

        public const string ON_DAMAGE_AFTER = "ON_DAMAGE_AFTER";                         //�˺�֮ǰ
        public const string ON_DAMAGE_LATER = "ON_DAMAGE_LATER";                         //�˺�֮��

        public const string ON_RELEASE_MODE_CHANGE = "ON_RELASE_MODE_CHANGE";            //�ͷż��ܵ�ģʽ�������

        public const string ON_QUIT_BATTLE = "ON_QUIT_BATTLE";//�˳�ս��

        /// <summary> �������������������¼� </summary>
        public const string ON_UI_COMMON_TIPS_CLICK = "ON_UI_COMMON_TIPS_CLICK";

        /// <summary> ����ʵ����� </summary>
        public const string ON_ADD_ENTITY_COMPLETE = "ON_ADD_ENTITY_COMPLETE";

        public const string ON_ADD_ENTITY_COMPLETE_ATCS = "ON_ADD_ENTITY_COMPLETE_ATCS";// �����崴��ʵ����� 
        public const string ON_LEVEL_CHARACTER_DIE_ATCS = "ON_LEVEL_CHARACTER_DIE_ATCS";// ������������е�ޱ���ɱ
        public const string ON_CHARACTER_HITDAMAGE_TOIL_ATCS = "ON_CHARACTER_HITDAMAGE_TOIL_ATCS"; // �����嵱��е���ܵ��˺�Ѫ��Ӧ�÷����仯��ʱ��
        public const string BUFF_ADDIDNUM = "BUFF_ADDIDNUM";
        public const string ON_ADD_ENTITY_COMPLETE_PASSIVE = "ON_ADD_ENTITY_COMPLETE_PASSIVE";// �����崴��ʵ����ɴ�������
        //�����¼�
        public const string CHARACTER_ATTACK = "CHARACTER_ATTACK";

        public const string CLICK_BATTLE_START = "CLICK_BATTLE_START";                          //���ս����ʼ

        //��Ϸ�Ծ��¼� ��ʽ_View[GameViewMediator] _Logic[GameLogicMediator] _Logic_View=================
        public const string CREATE_ZOIDSMODEL_VIEW = "CREATE_ZOIDSMODEL_VIEW";                  //������е��
        public const string CREATE_BULLETMODEL_VIEW = "CREATE_BULLETMODEL_VIEW";                //�����ӵ�

        // ·��ö��
        public const string MAP_GIRD_EFFECT_URL = "Scenes/effect_map_grid.prefab";
        //ˢ��
        public const string CMD_REFRESH_STAMINA = "CMD_REFRESH_STAMINA";            //ˢ������
        public const string CMD_REFRESH_ENERGY = "CMD_REFRESH_ENERGY";              //ˢ�¾���

        public const string CMD_UPDATE_GLOBAL_VOLUME = "CMD_UPDATE_GLOBAL_VOLUME";     //�򿪽���ı����ɫ
        public const string CMD_UPDATE_GLOBAL_VOLUME_BLOOM = "CMD_UPDATE_GLOBAL_VOLUME_BLOOM"; // ѩ�����ر�bloom

        //ս����ʾ��Ϣ
        public const string COMBAT_PROMPT_HIT = "COMBAT_PROMPT_HIT";
        public const string COMBAT_PROMPT_CURE = "COMBAT_PROMPT_CURE";
        public const string COMBAT_PROMPT_TIPS = "COMBAT_PROMPT_TIPS";

        //��������
        public const string CMD_GUIDANCE_COMPLETED = "CMD_GUIDANCE_COMPLETED";              //������������������
        public const string CMD_OPEN_GUIDANCE_VIEW = "CMD_OPEN_GUIDANCE_VIEW";              //������������
        public const string CMD_OPEN_GUIDELINES_VIEW = "CMD_OPEN_GUIDELINES_VIEW";          //����ָ������
        public const string CMD_GUIDE_STEP_START = "CMD_GUIDE_STEP_START";                  //��ʼ�������� 
        public const string CMD_GUIDE_STEP_COMPLETE = "CMD_GUIDE_STEP_COMPLETE";            //�����������
        public const string CMD_GUIDE_STEP_STATE_CHANGE = "CMD_GUIDE_STEP_STATE_CHANGE";    //��������״̬�ı�
        public const string CMD_GUIDE_PLOT_DIALOG = "CMD_GUIDE_PLOT_DIALOG";                //���ž���Ի�
        public const string CMD_GUIDE_PLOT_VIDEO = "CMD_GUIDE_PLOT_VIDEO";                  //���ž��鶯��
        public const string CMD_GUIDE_OBJ_CREATE = "CMD_GUIDE_OBJ_CREATE";                  //���������崴��������Ч
        public const string CMD_GUIDE_MOVE_CAMERA = "CMD_GUIDE_MOVE_CAMERA";                //���� - �ƶ����
        public const string CMD_GUIDE_OPERATION_UICOMPONENT = "CMD_GUIDE_OPERATION_UICOMPONENT";    //����ָ��UI�ؼ�
        public const string GUIDE_CONSTS_RECONNECT_TREE = "GuideTree/GuideReconnect.asset"; //�������� ��������


        public static class Input2EventType
        {
            public const int LOCKSTEP_CREATE_ZOIDS = 10;
            public const int LOCKSTEP_CREATE_BULLET = 15;
        }

        public static class MoneryEvent
        {
            /// <summary>���Ҹ��� </summary>
            public const string CMD_REFRESH_CURRENCY_AMOUNT = "CMD_REFRESH_CURRENCY_AMOUNT";
        }

        public static class PlayerInfoEvent
        {
            public const string OPEN_PLAYER_INFO_WINDOW = "OPEN_PLAYER_INFO_WINDOW";
        }

        public static class CommonViewEvent
        {
            //��Ӧ--ͨ�ý������
            public const string CMD_COMMONREEARD_RESPONSE = "CMD_COMMONREEARD_RESPONSE";
            //��Ӧ--ͨ�ý�������ر�
            public const string CMD_COMMONREEARD_CLOSE = "CMD_COMMONREEARD_CLOSE";
            //ͨ����ʾ����
            public const string CMD_OPEN_COMMONTIPS = "CMD_OPEN_COMMONTIPS";
            //ͨ�ô����봦��
            public const string CMD_ERROR_CODE_HANDLE = "CMD_ERROR_CODE_HANDLE";
        }

        //public static class BeastAdvanceEvent
        //{
        //    public const string ADVANCE_RESULT = "ADVANCE_RESULT";//��е������/����/���Ƿ�����Ϣ
        //}

        public static class ConstsKey
        {

            public const string MIN_PHY_DMG_RATE = "MinPhyDmgRate";                             //��С�����˺�ϵ��
            public const string MIN_MGC_DMG_RATE = "MinMgcDmgRate";                             //��С�����˺�ϵ��
            public const string CRIT_DMG_RATE = "CritDmgRate";                                  //�����˺�����
            public const string CRIT_COEFFICIENT = "CritCoefficient";                           //����ϵ��
            public const string DODGE_COEFFICIENT = "DodgeCoefficient";                         //����ϵ��

            //---------------------------����OR������ָ���------------------------------

            //AC == AutoChess
            public const string AC_CRIT_COEFFICIENT = "ACCritCoefficient";                      //������ʽ����ϵ��
            public const string AC_DAMAGE_REDUCTION = "ACDamageReduction";                      //�˺��������ϵ��
            public const string AC_LEVEL_CORRECTION = "ACLevelCorrection";                      //�ȼ�������ʽ����ϵ��
            public const string AC_DAMAGE_REDUCTION_MAX_VALUE = "ACDamageReductionMaxValue";    //�˺��������ֵ
            public const string AC_LEVEL_SUPPRESSION_RATE = "ACLevelSuppressionRate";           //�ȼ�ѹ�Ʊ���
            public const string AC_LEVEL_CORRECTION_MIN_VALUE = "ACLevelCorrectionMinValue";    //�ȼ�������Сֵ
            public const string AC_LEVEL_CORRECTION_MAX_VALUE = "ACLevelCorrectionMaxValue";    //�ȼ��������ֵ
            public const string AC_ATTACKSPEED_COEFFICIENT = "ACAttackSpeedCoefficient";        //���ټ���ϵ��
            public const string AC_DODGE_COEFFICIENT = "ACDodgeCoefficient";                    //���ܹ�ʽ����ϵ��
            //public const string HP_SCALE_PARAMETER = "HPScaleParameter";                        //Ѫ���̶Ȳ���
            public const string SPRINT_SPD = "SprintSpd";                                       //����ٶȲ���

            public const string HP_PARAM = "HPParam";                                           //����ϵ��
            public const string ATTACK_PARAM = "AttackParam";                                   //����ϵ��
            public const string PHYSICS_DEFENCE_PARAM = "PhysicsDefenceParam";                  //���ϵ��
            public const string MAGIC_DEFENCE_PARAM = "MagicDefenceParam";                      //����ϵ��
            public const string ATTACK_SPEED_PARAM = "AttackSpeedParam";                        //����ϵ��
            public const string ATTACKRANG_PARAM = "RangParam";                                 //��Χϵ��
            public const string PHYSICS_PENETRATE_PARAM = "PhysicsPenetrateParam";              //����͸ϵ��
            public const string MAGIC_PENETRATE_PARAM = "MagicPenetrateParam";                  //������͸ϵ��
            public const string CRITICAL_HIT_PARAM = "CriticalHitParam";                        //����ϵ��
            public const string HIT_PARAM = "HitParam";                                         //����ϵ��
            public const string DODGE_PARAM = "DodgeParam";                                     //����ϵ��
            public const string BLOOD_RETURN_PARAM = "BloodReturnParam";                        //�����ظ�ϵ��
            public const string SUCKING_BLOOD_PARAM = "SuckingBloodParam";                      //��Ѫϵ��
            public const string BACKINJURY_PARAM = "BackinjuryParam";                           //����ϵ��
            public const string ATTACK_ADDDAMAGE_PARAM = "AttackAddDamageParam";                //�չ�����ϵ��
            public const string SKILL_ADDDAMAGE_PARAM = "SkillAddDamageParam";                  //��������ϵ��
            public const string PHYSICS_ADDDAMAGE_PARAM = "PhysicsAddDamageParam";              //��������ϵ��
            public const string MAGIC_ADDDAMAGE_PARAM = "MagicAddDamageParam";                  //��������ϵ��
            public const string CURRENCY_ADDDAMAGE_PARAM = "CurrencyAddDamageParam";            //ͨ������ϵ��
            public const string NORMALATTACK_REDUCEDAMAGE_PARAM = "NormalAttackReduceDamageParam";//�չ�����ϵ��
            public const string SKILL_REDUCEDAMAGE_PARAM = "SkillReduceDamageParam";            //���ܼ���ϵ��
            public const string CURRENTCY_REDUCEDAMAGE_PARAM = "CurrencyReduceDamageParam";     //ͨ�ü���ϵ��
            public const string TreatMent_Param = "CurrencyReduceDamageParam";                  //���Ƽӳ�ϵ��



            public const string BATTLE_EDN_DELAY = "BattleEndDelay";                            //ս�������ӳٽ���ʱ��
            public const string COSUME_CEILING = "ConsumeCeiling";                              //��������
            public const string STRENGTHRECOVERTIMES = "StrengthRecoverTimes";                  //�����ָ�ʱ��(s)
            public const string ENERGY_CEILING = "EnergyCeiling";                               //��������
            public const string ENERGYRECOVERTIMES = "EnergyRecoverTimes";                      //�����ָ�ʱ��(s)

            public const string MAP_GRID_LENGTH = "MapGridLength";                              //ս�����ӱ߳�
            public const string ACGainMinusGainMinValue = "ACGainMinusGainMinValue";            //�������������Сֵ
            public const string ACGainMinusGainMaxValue = "ACGainMinusGainMaxValue";            //��������������ֵ
            //public const string ACRacialRestraintCoefficient = "ACRacialRestraintCoefficient";  //�������ϵ��
            public const string ACCritCorrectionMinValue = "ACCritCorrectionMinValue";          //����������Сֵ
            public const string ACCritCorrectionMaxValue = "ACCritCorrectionMaxValue";          //�����������ֵ
                                                                                                //public const string ACSkillSpeedCoefficient = "ACSkillSpeedCoefficient";            //���ܼ��ټ���ϵ��
                                                                                                //public const string PLAYER_LEVELLIMIT_PARAM_1 = "PlayerLevelLimitParam1";           //��ҵȼ����Ʋ���1
                                                                                                //public const string PLAYER_LEVELLIMIT_PARAM_2 = "PlayerLevelLimitParam2";           //��ҵȼ����Ʋ���2

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
