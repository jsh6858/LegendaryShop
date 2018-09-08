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
        LegendFramework.Quest quest = EasyManager.Instance._curContract._myQuest;

        _Title.text = quest.Title;

       // _MonsterName.text 
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
