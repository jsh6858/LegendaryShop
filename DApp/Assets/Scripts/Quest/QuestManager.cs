using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    // Title
    public UILabel _Title;

    // MonsterInfo
    public UILabel _MonsterName;
    public UILabel _MonsterPower;
    public UILabel _SuccessRate;
    public UILabel _Time;
    public Sprite _SprMon;

    // Item Info
    Item_List[] _itemList = new Item_List[8];

    // Party_Info
    public UILabel _PartyName;
    public UILabel _PartyPower;

    public UILabel _PartyPlus;
    public UILabel _PowerNeed;
    public UILabel _CheckNum;
    public Sprite _SrpCheck;

    HunterAndWeapon[] _hunterAndWeaponList = new HunterAndWeapon[8];

    // Bottom
    public UILabel _SignText;
    public Sprite _SprSign;
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
    
    public void Close_Detail()
    {
        _GameManager.Close_PopUp();
    }
}
