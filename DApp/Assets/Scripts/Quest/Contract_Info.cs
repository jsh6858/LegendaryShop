using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Contract_Info : MonoBehaviour {

    Quest _myQuest = null;

    // Active
    public UISprite _sprMon;
    public UISprite _sprHun1;
    public UISprite _sprHun2;
    public UISprite _sprHun3;

    public UISlider _slider;

    public UILabel _AcitveTime;

    // UnActive
    public UILabel _UnAcitveTime;

    public void Set_ContractInfo(Quest _quest)
    {
        _myQuest = _quest;


    }
}
