﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Contract_Info : MonoBehaviour {

    Contract_Page _trContracPage;

    public Quest _myQuest = null;

    GameObject _Active;
    GameObject _UnActive;

    public float _fMaxTime = 0f;
    public float _fRemainingTime = 0f;
    public bool _bActivate;

    // Active
    public UISprite _sprMon;
    public UISprite _sprHun1;
    public UISprite _sprHun2;
    public UISprite _sprHun3;

    public UISlider _slider;

    public UILabel _AcitveTime;

    // UnActive
    public UILabel _UnAcitveTime;

    public void Set_ContractInfo(Quest _quest) // New Quest!
    {
        _myQuest = _quest;

        _fMaxTime = _fRemainingTime = _myQuest.PeriodTime;
        //_sprMon.spriteName = GlobalDdataManager.MonsterList[_myQuest.MyMonster].MonsterSdId;

        int iPartyMember = GlobalDdataManager.PartyList[_myQuest.MyParty].PartyMembers[0];
        //_sprHun1.spriteName = GlobalDdataManager.HunterList[iPartyMember].SDImageId;

        iPartyMember = GlobalDdataManager.PartyList[_myQuest.MyParty].PartyMembers[1];
        //_sprHun1.spriteName = GlobalDdataManager.HunterList[iPartyMember].SDImageId;

        iPartyMember = GlobalDdataManager.PartyList[_myQuest.MyParty].PartyMembers[2];
        //_sprHun1.spriteName = GlobalDdataManager.HunterList[iPartyMember].SDImageId;

        _fMaxTime = _fRemainingTime = Random.Range(60f, 80f);
    }

    void Start()
    {
        _Active = transform.Find("Active").gameObject;
        _UnActive = transform.Find("UnActive").gameObject;

        _trContracPage = transform.parent.GetComponent<Contract_Page>();

        _fMaxTime = _fRemainingTime = Random.Range(60f, 120f);

        _bActivate = transform.Find("Active").gameObject.activeSelf;
    }

    void Update()
    {
        if (true == _bActivate)
        {
            _slider.value = _fRemainingTime / _fMaxTime;
            _fRemainingTime -= Time.deltaTime * 20f;

            _AcitveTime.text = UsefulFunction.TimeToString(_fRemainingTime);

            if (_fRemainingTime < 0f)
                Set_NewContract();
        }
    }

    public void Set_NewContract()
    {
        Activate(true);

        int iMax = GlobalDdataManager.QuestList.Count;
        Quest rand_quest = GlobalDdataManager.QuestList[Random.Range(0, iMax - 1)];

        Set_ContractInfo(rand_quest);
    }

    public void Activate(bool b)
    {
        if (b== false)
        {
            _bActivate = false;

            _Active.SetActive(false);
            _UnActive.SetActive(true);
        }
        else
        {
            _bActivate = true;

            _Active.SetActive(true);
            _UnActive.SetActive(false);
        }

        _trContracPage.Sort_Contract();
    }
}
