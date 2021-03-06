﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;
using System.IO;
using System.Text;
using System;

public class GameManager : MonoBehaviour
{
    Tab1Manager tab1Manager;
    Tab2Manager tab2Manager;
    Tab3Manager tab3Manager;

    Transform[] bottomBTNs;

    public Transform _trPopup;

    public BoxCollider[] _boxColliders;

    LegendaryClient client;

    void Awake()
    {
        tab1Manager = GameObject.Find("Tab1Manager").GetComponent<Tab1Manager>();
        tab2Manager = GameObject.Find("Tab2Manager").GetComponent<Tab2Manager>();
        tab3Manager = GameObject.Find("Tab3Manager").GetComponent<Tab3Manager>();

        bottomBTNs = new Transform[3];
        bottomBTNs[0] = GameObject.Find("QuestList").transform;
        bottomBTNs[1] = GameObject.Find("HunterList").transform;
        bottomBTNs[2] = GameObject.Find("GachaList").transform;

        client = new LegendaryClient();
        //_ConnectToLoomNetwork();

        _LoadData();

        GlobalDdataManager.MyWeaponList = new List<Weapon>();
        GlobalDdataManager.MyItemList = new List<Item>();

        for (int i = 0; i < 55; ++i)
        {
            int rand = UnityEngine.Random.RandomRange(0, 18);
            GlobalDdataManager.MyItemList.Add(GlobalDdataManager.ItemList[rand]);
        }

        //_GetPlayerName();

        EasyManager.Instance.Initialize();
    }

    void Update()
    {
        for (int i = 0; i < GlobalDdataManager.QuestProgressList.Count; i++)
        {
            GlobalDdataManager.QuestProgressList[i].SetPeriodTime();
        }
    }

    private void _ConnectToLoomNetwork()
    {
        client.SignIn("172.30.1.34");
        //client.SignIn("127.0.0.1");

        // showQuest , slotid(0 ~ 11) -> reportQuest (해당 슬롯의 정보)
        // createQuest , qid / time -> newQuest ( qid / slotid / log )
        // startQuest , slotid -> startQuest ( slotid / endTime )
        // resultQuest , slotid -> resultQuest ( status, slotid, qid, remainTime )
        // deleteQuest , slotid -> deleteQuest ( success, slotid )

    }
    public void ShowQuest(int slotId)
    {
        //client.ShowQuest(slotId);
    }
    public void CreateQuest(int questId, int time)
    {
        //client.CreateQuest(questId, time);
    }
    public void StartQuest(int slotId)
    {
        //client.StartQuest(slotId);
    }
    public void ResultQuest(int slotId)
    {
        //client.ResultQuest(slotId);
    }
    public void DeleteQuest(int slotId)
    {
        //client.DeleteQuest(slotId);
    }

    private void _GetPlayerName()
    {
        // 플레이어 이름이 저장되어 있지 않을 경우 입력받기
        // 저장되어 있는 경우엔 로드
        // (PlayerPrefs)
        PlayerPrefs.DeleteAll();
        if ("" != PlayerPrefs.GetString("PlayerName"))
            GlobalDdataManager.PlayerName = PlayerPrefs.GetString("PlayerName");
        else
            _InputPlayerName();
    }

    public void CallTab1Manager()
    {
        bottomBTNs[0].localPosition = new Vector2(bottomBTNs[0].localPosition.x, -570f);
        bottomBTNs[1].localPosition = new Vector2(bottomBTNs[1].localPosition.x, -630f);
        bottomBTNs[2].localPosition = new Vector2(bottomBTNs[2].localPosition.x, -630f);

        tab1Manager.CallManager();
        tab2Manager.DropManager();
        tab3Manager.DropManager();
    }
    public void CallTab2Manager()
    {
        bottomBTNs[0].localPosition = new Vector2(bottomBTNs[0].localPosition.x, -630f);
        bottomBTNs[1].localPosition = new Vector2(bottomBTNs[1].localPosition.x, -570f);
        bottomBTNs[2].localPosition = new Vector2(bottomBTNs[2].localPosition.x, -630f);

        tab1Manager.DropManager();
        tab2Manager.CallManager();
        tab3Manager.DropManager();
    }
    public void CallTab3Manager()
    {
        bottomBTNs[0].localPosition = new Vector2(bottomBTNs[0].localPosition.x, -630f);
        bottomBTNs[1].localPosition = new Vector2(bottomBTNs[1].localPosition.x, -630f);
        bottomBTNs[2].localPosition = new Vector2(bottomBTNs[2].localPosition.x, -570f);

        tab1Manager.DropManager();
        tab2Manager.DropManager();
        tab3Manager.CallManager();
    }

    public void Show_Popup(GameObject obj)
    {
        Instantiate(obj, Vector3.zero, Quaternion.identity, _trPopup);

        ActivatePanelCollider(false);
    }

    public void Close_PopUp()
    {
        if (_trPopup.childCount == 0)
            return;

        Transform child = _trPopup.GetChild(_trPopup.childCount - 1); //

        Destroy(child.gameObject);

        ActivatePanelCollider(true);
    }

    public void Push_PlayToTitle()
    {
        SceneChanger.Instance.PlayToTitle();
    }

    public void Push_ShowWeapon(GameObject obj)
    {
        if (obj.name == "Display")
            EasyManager.Instance._bChooseWeapon = false;
        else
            EasyManager.Instance._bChooseWeapon = true;

        Show_Popup(Resources.Load("Inventory/Inventory_Weapon") as GameObject);
    }
    public void Push_ShowMakeWeapon()
    {
        Show_Popup(Resources.Load("Gacha/Gacha_EffectPage") as GameObject);
    }
    public void Push_ShowItem(GameObject obj)
    {
        Show_Popup(Resources.Load("Inventory/Inventory_Item") as GameObject);
    }

    public void ActivatePanelCollider(bool b)
    {
        for (int i = 0; i < _boxColliders.Length; ++i)
            _boxColliders[i].enabled = b;
    }


    private void _InputPlayerName()
    {
        GameObject popup = Resources.Load("Gacha/Popup_Nickname") as GameObject;
        GameObject confirmBTN = popup.transform.Find("ConfirmButton").gameObject;
        GameObject cancelBTN = popup.transform.Find("CancelButton").gameObject;

        UIEventTrigger trigger = confirmBTN.GetComponent<UIEventTrigger>();
        EventDelegate beginDrag = new EventDelegate(this, "OnBeginDrag_ConfirmBTN");
        EventDelegate.Add(trigger.onPress, beginDrag);

        EventDelegate endDrag = new EventDelegate(this, "OnEndDrag");
        EventDelegate.Add(trigger.onRelease, endDrag);

        trigger = cancelBTN.GetComponent<UIEventTrigger>();
        beginDrag = new EventDelegate(this, "OnBeginDrag_CancleBTN");
        EventDelegate.Add(trigger.onPress, beginDrag);

        endDrag = new EventDelegate(this, "OnEndDrag");
        EventDelegate.Add(trigger.onRelease, endDrag);

        NGUITools.AddChild(GameObject.Find("PopUp"), popup);
    }

    private void _Confirm_NickName()
    {
        Debug.Log("_Confirm_NickName");
    }
    private void _Cancel_NickName()
    {
        Debug.Log("_Cancel_NickName");
    }

    delegate void func();
    private func function;
    Vector3 pos;
    public void OnBeginDrag_ConfirmBTN()
    {
        pos = Input.mousePosition;
        function = _Confirm_NickName;
    }
    public void OnBeginDrag_CancleBTN()
    {
        pos = Input.mousePosition;
        function = _Cancel_NickName;
    }

    public void OnEndDrag()
    {
        Vector3 end_pos = Input.mousePosition;
        if (0.5f > Vector3.Distance(pos, end_pos))
            function();
    }

    private void _LoadData()
    {
        string path = null;
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/Data/";
#elif UNITY_ANDROID
        path = Application.persistentDataPath + "/Resources/Data/";
#endif
        Debug.Log(Application.persistentDataPath);

        GlobalDdataManager.HunterList = new List<Hunter>();
        GlobalDdataManager.WeaponList = new List<Weapon>();
        GlobalDdataManager.PartyList = new List<Party>();
        GlobalDdataManager.QuestList = new List<Quest>();
        GlobalDdataManager.QuestReadyList = new List<Quest>();
        GlobalDdataManager.QuestProgressList = new List<Quest>();
        GlobalDdataManager.ItemList = new List<Item>();
        GlobalDdataManager.MonsterList = new List<Monster>();

        string s = null;

        TextAsset _txtFile = (TextAsset)Resources.Load("Data/Weapon") as TextAsset;
        s = _txtFile.text;
        string[] temp2 = s.Split('\n');

        for (int i = 0; i < temp2.Length - 1; ++i)
        {
            string[] temp = temp2[i].Split(',');

            temp[6] = temp[6].Replace('/', ',');
            Weapon w = new Weapon(Int32.Parse(temp[0]), temp[1], temp[2], Int32.Parse(temp[3])
                , (WEAPON_TYPE)Int32.Parse(temp[4]), (WEAPON_GRADE)Int32.Parse(temp[5]), temp[6], temp[7], temp[8]);
            GlobalDdataManager.WeaponList.Add(w);
        }

        _txtFile = (TextAsset)Resources.Load("Data/Quest") as TextAsset;
        s = _txtFile.text;
        temp2 = s.Split('\n');

        for (int j = 0; j < temp2.Length - 1; ++j)
        {
            string[] temp = temp2[j].Split(',');

            string[] temp5 = temp[5].Split('/');
            int[] temp5_int = new int[temp5.Length];
            for (int i = 0; i < temp5.Length; ++i)
                temp5_int[i] = Int32.Parse(temp5[i]);

            string[] temp6 = temp[6].Split('/');
            int[] temp6_int = new int[temp6.Length];
            for (int i = 0; i < temp6.Length; ++i)
                temp6_int[i] = Int32.Parse(temp6[i]);

            Quest q = new Quest(Int32.Parse(temp[0]), temp[1], Int32.Parse(temp[2])
                , Int32.Parse(temp[3]), Int32.Parse(temp[4]), temp5_int, temp6_int);
            GlobalDdataManager.QuestList.Add(q);
        }

        _txtFile = (TextAsset)Resources.Load("Data/Party") as TextAsset;
        s = _txtFile.text;
        temp2 = s.Split('\n');

        for (int j = 0; j < temp2.Length - 1; ++j)
        {
            string[] temp = temp2[j].Split(',');

            string[] temp3 = temp[3].Split('/');
            int[] temp3_int = new int[temp3.Length];
            for (int i = 0; i < temp3.Length; ++i)
                temp3_int[i] = Int32.Parse(temp3[i]);

            int[] temp4_int = new int[3];

            Party temp22 = new Party(0, "aa", 0, null, null);
            Party p = new Party(Int32.Parse(temp[0]), temp[1], 0//Int32.Parse(temp[2])
                , temp3_int, temp4_int);
            GlobalDdataManager.PartyList.Add(p);
        }

        _txtFile = (TextAsset)Resources.Load("Data/Monster") as TextAsset;
        s = _txtFile.text;
        temp2 = s.Split('\n');

        for (int j = 0; j < temp2.Length - 1; ++j)
        {
            string[] temp = temp2[j].Split(',');

            Monster m = new Monster(Int32.Parse(temp[0]), temp[1], temp[2], temp[4], Int32.Parse(temp[3]));
            GlobalDdataManager.MonsterList.Add(m);
        }

        _txtFile = (TextAsset)Resources.Load("Data/Item") as TextAsset;
        s = _txtFile.text;
        temp2 = s.Split('\n');

        for (int j = 0; j < temp2.Length - 1; ++j)
        {
            string[] temp = temp2[j].Split(',');

            Item i = new Item(Int32.Parse(temp[0]), temp[1], temp[2], (ITEM_TYPE)Int32.Parse(temp[3])
                , temp[4], temp[5]);
            GlobalDdataManager.ItemList.Add(i);
        }

        _txtFile = (TextAsset)Resources.Load("Data/Hunter") as TextAsset;
        s = _txtFile.text;
        temp2 = s.Split('\n');

        for (int j = 0; j < temp2.Length - 1; ++j)
        {
            string[] temp = temp2[j].Split(',');

            string[] temp6 = temp[6].Split('/');
            int[] temp6_int = new int[temp6.Length];
            for (int i = 0; i < temp6.Length; ++i)
                temp6_int[i] = Int32.Parse(temp6[i]);

            int max = 0;
            int idx = 0;
            for (int i = 0; i < temp6_int.Length; ++i)
            {
                if (temp6_int[i] > max)
                {
                    max = temp6_int[i];
                    idx = i;
                }
            }

            Hunter h = new Hunter(Int32.Parse(temp[0]), temp[1], temp[2], temp[3], Int32.Parse(temp[4])
                , (WEAPON_TYPE)idx, temp6_int);
            GlobalDdataManager.HunterList.Add(h);
        }

    }
}


