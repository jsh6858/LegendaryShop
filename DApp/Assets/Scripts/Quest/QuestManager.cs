﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class QuestManager : MonoBehaviour {

    readonly int[] POWER_NEED = new int[] { 1000, 3000, 5000, 7000, 9000 };
    readonly int[] OPEN_ITEM = new int[] { 1, 1, 1, 2, 1 };

    // Title
    public UILabel _Title;

    // MonsterInfo
    public UILabel _MonsterName;
    public UILabel _MonsterPower;
    public UILabel _SuccessRate;
    public UILabel _Time;
    public UISprite _SprMon;

    // Item Info
    public Item_List[] _itemList = new Item_List[8];

    // Party_Info
    public UILabel _PartyName;
    public UILabel _PartyPower;

    public UILabel _PartyPlus;
    public UILabel _PowerNeed;
    public UILabel _CheckNum;
    public UISprite _SrpCheck;

    public HunterAndWeapon[] _hunterAndWeaponList = new HunterAndWeapon[3];

    // Bottom
    public UILabel _SignText;
    public UISprite _SprSign;
    public UILabel _SignDate;

    private GameManager GameManager;
    GameManager _GameManager
    {
        get
        {
            if (null == GameManager)
                GameManager = EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>();

            return GameManager;
        }
    }

    private void OnEnable()
    {
        Quest quest = EasyManager.Instance._curContract._myQuest;
        _Title.text = quest.Title;

        Monster monster = GlobalDdataManager.MonsterList[quest.MyMonster % GlobalDdataManager.MonsterList.Count];
        _MonsterName.text = monster.MonsterName;
        _MonsterPower.text = monster.MonsterPower.ToString();
        _SuccessRate.text = string.Format("{0:#.##}", Random.Range(60f, 99f));
        _Time.text = quest.PeriodTime.ToString();
        //_SprMon.spriteName = monster.MonsterSdId.ToString();

        int[] normal = quest.Normal_Trophy;
        int[] unique = quest.Random_Trophy;

        _itemList[0].Set_ItemInfo(GlobalDdataManager.ItemList[normal[0]].Name);
        _itemList[1].Set_ItemInfo(GlobalDdataManager.ItemList[normal[1]].Name);

        for(int i=2; i<8; ++i)
            _itemList[i].Set_ItemInfo(GlobalDdataManager.ItemList[unique[i-2]].Name);

        int[] hunter = GlobalDdataManager.PartyList[quest.MyParty].PartyMembers;

        for (int i = 0; i < _hunterAndWeaponList.Length; ++i)
            _hunterAndWeaponList[i].Set_Hunter(GlobalDdataManager.HunterList[hunter[i]], i+1);
        
        Party party = GlobalDdataManager.PartyList[quest.MyParty];
        _PartyName.text = party.PartyName;
        _PartyPower.text = party.PartyPower.ToString();
        /*_PartyPlus.text = ??
        public UILabel _PowerNeed;
        public UILabel _CheckNum;
        public UISprite _SrpCheck;
        */

        Calculate_AllPower();
    }

    void Calculate_AllPower()
    {
        int iResult = 0;

        // Hunters' power
        for (int i = 0; i < _hunterAndWeaponList.Length; ++i)
            iResult += int.Parse(_hunterAndWeaponList[i]._Power.text);

        // Weapon's power
        for (int i = 0; i < _hunterAndWeaponList.Length; ++i)
        {
            if (_hunterAndWeaponList[i]._bActivate == false)
                continue;

            iResult += int.Parse(_hunterAndWeaponList[i]._WpPower.text);
        }
        
        int iAbleToOpen = 2;
        int iNeedPower = 0;
        int iNextOpen = 0;

        for (int i = 0; i < POWER_NEED.Length; ++i)
        {
            if (iResult > POWER_NEED[i])
            {
                iAbleToOpen += OPEN_ITEM[i];
            }
            else
            {
                iNeedPower = POWER_NEED[i] - iResult;
                iNextOpen = OPEN_ITEM[i];

                break;
            }
        }

        _PartyPower.text = iResult.ToString();
        _PowerNeed.text = iNeedPower.ToString();
        _CheckNum.text = iAbleToOpen.ToString();
        _PartyPlus.text = iNextOpen.ToString();

        for (int i=0; i< _itemList.Length; ++i)
        {
            if(i < iAbleToOpen)
                _itemList[i].Activate(true);
            else
                _itemList[i].Activate(false);
        }
    }

    public void Push_Admission()
    {
        EasyManager.Instance._curContract.Activate(false);

        Add_QuestList();

        Close_Detail(); 
    }
    
    public void Close_Detail()
    {
        _GameManager.Close_PopUp();
    }

    void Add_QuestList()
    {
        //LegendFramework.Quest quest = new LegendFramework.Quest(0, "title", 10, 1, 1, new int[] { 1, 1 }, new int[] { 2, 2 });

        LegendFramework.GlobalDdataManager.QuestProgressList.Add(EasyManager.Instance._curContract._myQuest);
    }
}
