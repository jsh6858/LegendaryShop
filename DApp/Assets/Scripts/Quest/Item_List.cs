﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_List : MonoBehaviour {

    public bool _bSelected = false; // 현재 퀘스트 받는 창에서 아이템이 선택되었는지

    // UnLock
    public UILabel _ItemName;
    public UILabel _Percentage;

    // Lock
    public UILabel _UnlockCondition;

    UISprite sprImage;
    UISprite _sprImage
    {
        get
        {
            if (null == sprImage)
                sprImage = transform.Find("UnLock/Button/Background").GetComponent<UISprite>();
            return sprImage;
        }
    }

    GameObject objUnlock;
    GameObject _objUnlock
    {
        get
        {
            if (null == objUnlock)
                objUnlock = transform.Find("UnLock").gameObject;
            return objUnlock;
        }
    }
    
    GameObject objLock;
    GameObject _objLock
    {
        get
        {
            if (null == objLock)
                objLock = transform.Find("Lock").gameObject;
            return objLock;
        }
    }

    public void Set_ItemInfo(string name, float percentage = 70f)
    {
        _ItemName.text = name;
        
        _Percentage.text = string.Format("{0:#.##}", Random.Range(60f, 99f));
    }

    public void Activate(bool b)
    {
        if (b)
        {
            _objUnlock.SetActive(true);
            _objLock.SetActive(false);
        }
        else
        {
            _objUnlock.SetActive(false);
            _objLock.SetActive(true);
        }
    }

    public void Select_Item(bool b = false)
    {
        if(b == true) // 무조건 true로
        {
            _bSelected = b;

            Debug.Log("Item Selcted : " + gameObject.name);
            //_sprImage.spriteName = "Select!";

            return;
        }

        if (!_bSelected)
        {
            Debug.Log("Item Selcted : " + gameObject.name);
            //_sprImage.spriteName = "Select!";
        }
        else
        {
            Debug.Log("Item DeSelcted : " + gameObject.name);
            //_sprImage.spriteName = "UnSelect!";
        }

        _bSelected = !_bSelected;
    }
}
