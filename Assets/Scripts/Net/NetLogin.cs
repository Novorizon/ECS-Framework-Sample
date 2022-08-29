//using Cspb;
//using Google.Protobuf.Collections;
//using Net;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.Serialization.Formatters.Binary;
//using UnityEngine;
//using UnityEngine.UI;

//public class NetLogin : MonoBehaviour
//{
//    // Start is called before the first frame update
//    public Dropdown _serverDropDown;
//    public Dropdown _characterDropDown;
//    public Button _loginButton;
//    private bool _refreshServerList = false;
//    private bool _refreshCharacterList = false;
//    private List<int> _serverIdList = new List<int>();
//    private List<long> _charIdList = new List<long>();
//    private int _serverChoosenId;
//    //private bool _showTestServer;
//    public Toggle _toggleTestServer;

//    public int GetServerChoosen()
//    {
//        return _serverChoosenId;
//    }
//    private long _charChoosenId;
//    public long GetCharChoosen()
//    {
//        return _charChoosenId;
//    }
//    public InputField _username;
//    private string _oldUserName;
//    //private bool _needCreateNewChar;
//    public void EnableCreateNewChar()
//    {
//        _charChoosenId = -1;
//    }
//    public void DisableCreateNewChar()
//    {
//        _charChoosenId = 0;
//    }
//    public Text _log;
//    private readonly List<int> SERVER_ID = new List<int> {1,2,3,4,5,6}; //new List<int>{ 666, 7 , 88 };
//    private readonly int NEW_CHARACTER_ID = -1;

//    public string GetUserName()
//    {
//        return _username.text;
//    }

//    void Start()
//    {
//        _serverIdList = new List<int>();
//        _charIdList = new List<long>();
//        _serverChoosenId = 0;
//        _charChoosenId = 0;
//        _characterDropDown.ClearOptions();
//        _serverDropDown.ClearOptions();
//        _username.text = "";
//        //_showTestServer = _toggleTestServer.isOn;

//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if(!string.Equals(_oldUserName,GameClientInfo.Udid))
//        {
//            _oldUserName = GameClientInfo.Udid;
//            _username.text = _oldUserName;
//        }
//        //CheckUpdate();
//    }
//    public void SetUpdate()
//    {
//        _refreshCharacterList = true;
//        _refreshServerList = true;
//    }
//    public void UpdateServerChoosen()
//    {
//        _serverChoosenId = 0;
//        foreach(int id in _serverIdList)
//        {
//            if (_toggleTestServer.isOn || SERVER_ID.Contains(id))
//            {
//                _serverChoosenId = id;
//                break;
//            }
//        }
//    }
//    public void UpdateCharacterChoosen()
//    {
//        _charChoosenId = 0;
//        if(_serverChoosenId != 0)
//        {
//            foreach(int id in _charIdList)
//            {
//                _charChoosenId = id;
//                break;
//            }
//        }
        
//    }
//    public void CheckUpdate()
//    {
        
//        if (_refreshServerList)
//        {

//            UpdateServerOption();
//            UpdateServerChoosen();
//            _refreshServerList = false;
//        }

//        SetLog("Got Auth Ack, Update Server and Character Info");
        
//        if (_refreshCharacterList)
//        {
//            UpdateCharacterOption();
//            UpdateCharacterChoosen();
//            _refreshCharacterList = false;          

//        }


      
//    }
//    public void UpdateCharacterOption()
//    {
//        List<Character> _list = Net.NetManager.instance.CurrentAuthInfo.characters;
//        List<Dropdown.OptionData> _datalist = new List<Dropdown.OptionData>();
//        _characterDropDown.ClearOptions();
//        _charIdList.Clear();
//        foreach (Character _item in _list)
//        {

//            if(_item.ServerId == _serverChoosenId)
//            {
//                Dropdown.OptionData _data = new Dropdown.OptionData
//                {
//                    text = "CharId: " + _item.PlayerId + "     LastServer: " + _item.ServerId
//                };
//                _datalist.Add(_data);
//                _charIdList.Add(_item.PlayerId);
//            }
//        }
//        Dropdown.OptionData _extradata = new Dropdown.OptionData
//        {
//            text = "Create New Character"
//        };
//        _datalist.Add(_extradata);
//        _charIdList.Add(NEW_CHARACTER_ID);
//        _characterDropDown.AddOptions(_datalist);
//        _characterDropDown.onValueChanged.AddListener(SetCharacterChoosen);
//    }
//    public void UpdateServerOption()
//    {

//        List<Server> _list = Net.NetManager.instance.CurrentAuthInfo.servers;
//        List<Dropdown.OptionData> _datalist = new List<Dropdown.OptionData>();
//        _serverDropDown.ClearOptions();
//        _serverIdList.Clear();

//        foreach (Server _item in _list)
//        {
//            if (!(_toggleTestServer.isOn ^ (SERVER_ID.Contains(_item.ServerId))))
//                continue;
//            Dropdown.OptionData _data = new Dropdown.OptionData
//            {
//                text = "ServerName: " + _item.Name.ToString() + "     Id: " + _item.ServerId.ToString() + " status: " + _item.Status.ToString()
//            };
//            _serverIdList.Add(_item.ServerId);
//            _datalist.Add(_data);
//        }
//        _serverDropDown.AddOptions(_datalist);
//        _serverDropDown.onValueChanged.AddListener(SetServerChoosen);
//    }
//    public void SetServerChoosen(int value)
//    {
//        if(_serverChoosenId != _serverIdList[value])
//        {
//            _serverChoosenId = _serverIdList[value];         
//            _refreshCharacterList = true;
//            CheckUpdate();
//        }
//    }
//    public void SetCharacterChoosen(int value)
//    {
//        if(_charChoosenId != _charIdList[value])
//        {
//            _charChoosenId = _charIdList[value];
//        }
//    }
//    public void DoCreateNewCharLogin()
//    {
//        SetLog("Send Create New Char Msg");        
//        MsgAuth.Instance.SendCreateCharReq(Net.NetManager.instance.CurrentAuthInfo.accessToken, Net.NetManager.instance.CurrentAuthInfo.accountId, _serverChoosenId != 0 ? _serverChoosenId: Net.NetManager.instance.CurrentAuthInfo.recommandServer);
//    }
//    public void DoCharLogin()
//    {
//        SetLog("Send Char Login");
//        if (_serverChoosenId == 0 || _charChoosenId == 0)
//        {
//            SetLog("server or char choosen error");
//            return;
//        }

//        MsgAuth.Instance.SendCharLoginReq(Net.NetManager.instance.CurrentAuthInfo.accessToken, Net.NetManager.instance.CurrentAuthInfo.accountId, _serverChoosenId, _charChoosenId);
//    }
//    public void OnToggleClick()
//    {
//        SetUpdate();
//        CheckUpdate();
//    }
//    public void OnLogin()
//    {
//        if(_charChoosenId==-1)
//        {
//            DoCreateNewCharLogin();
//        }
//        else
//        {
//            DoCharLogin();
//        }
        
//    }
//    public void OnUsernameChange()
//    {
//        if(!string.Equals(_username.text,_oldUserName))
//        {
//            _oldUserName = _username.text;
//            GameClientInfo.Udid = _username.text;
//            if (NetManager.instance.TempKey != null && NetManager.instance.SessionKey == null)
//                MsgAuth.Instance.SendKeyExchangeReq();
//            else
//                MsgAuth.Instance.SendAuthReq();
//            _loginButton.enabled = false;
//            _loginButton.gameObject.SetActive(false);
//            _log.text = "Wait for Auth Msg....";
//        }
//    }
//    public void SetLog(string s)
//    {
//        _log.text = s;
//    }
//}
