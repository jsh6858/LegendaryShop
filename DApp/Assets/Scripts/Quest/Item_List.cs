using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_List : MonoBehaviour {

    // UnLock
    public UILabel _ItemName;
    public UILabel _Percentage;

    // Lock
    public UILabel _UnlockCondition;

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
}
