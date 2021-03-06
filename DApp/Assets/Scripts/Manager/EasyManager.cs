﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class EasyManager {

    bool _bInitialize = false;

    public Contract_Info _curContract;

    public bool _bChooseWeapon = false;
    Weapon _selectedWeapon;

    private EasyManager()
    {

    }

    private static EasyManager _Instance = null;
    public static EasyManager Instance
    {
        get
        {
            if (null == _Instance)
                _Instance = new EasyManager();

            return _Instance;
        }
    }

    private Dictionary<string, GameObject> _dicObj = new Dictionary<string, GameObject>();

    public void Initialize()
    {
        if (_bInitialize)
            return;
        _bInitialize = true;

        string[] strObj = new string[] { "GameManager", "UI" };

        for(int i=0; i< strObj.Length; ++i)
            _dicObj.Add(strObj[i], GameObject.Find(strObj[i]));
    }

    public GameObject GetObj(string str)
    {
        if (_dicObj.ContainsKey(str) == false || _dicObj[str] == null)
            _dicObj[str] = GameObject.Find(str);

        return _dicObj[str];
    }

    public void Weapon_Select(Weapon w)
    {
        _selectedWeapon = w;

        // 처리
        _dicObj["UI"].transform.Find("Camera/PopUp/QuestManager(Clone)").GetComponent<QuestManager>().Give_Weapon(w);
    }

}
