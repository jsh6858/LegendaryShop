using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class QuestManager : MonoBehaviour {

    readonly int[] POWER_NEED = new int[] { 4000, 7000, 10000 };
    readonly int[] OPEN_ITEM = new int[] { 1, 1, 1 };

    int _iCurSelectedHunter;
    List<int> _iCurSelectedItems = new List<int>();

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
    public Transform _trHand;
    public Transform _trSign;

    Vector3 _vOriPos;

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

    private void Awake()
    {
        _vOriPos = _trHand.position;
    }

    private void OnEnable()
    {
        Quest quest = EasyManager.Instance._curContract._myQuest;
        _Title.text = quest.Title;

        Monster monster = GlobalDdataManager.MonsterList[quest.MyMonster % GlobalDdataManager.MonsterList.Count];
        _MonsterName.text = monster.MonsterName;
        _MonsterPower.text = monster.MonsterPower.ToString();
        _Time.text = quest.PeriodTime.ToString();
        _SprMon.spriteName = monster.MonsterSdId.ToString();

        int[] normal = quest.Normal_Trophy;
        int[] unique = quest.Random_Trophy;

        _itemList[0].Set_ItemInfo(GlobalDdataManager.ItemList[normal[0]].Name);
        _itemList[0].Select_Item(true);
        _itemList[1].Set_ItemInfo(GlobalDdataManager.ItemList[normal[1]].Name);
        _itemList[1].Select_Item(true);

        for (int i = 2; i < 5; ++i)
            _itemList[i].Set_ItemInfo(GlobalDdataManager.ItemList[unique[i - 2]].Name);

        for (int i=5; i<8; ++i)
            _itemList[i].Set_ItemInfo(GlobalDdataManager.ItemList[unique[i-5]].Name, POWER_NEED[i-5]);

        int[] hunter = GlobalDdataManager.PartyList[quest.MyParty].PartyMembers;

        for (int i = 0; i < _hunterAndWeaponList.Length; ++i)
            _hunterAndWeaponList[i].Set_Hunter(GlobalDdataManager.HunterList[hunter[i]], i+1);
        
        Party party = GlobalDdataManager.PartyList[quest.MyParty];
        _PartyName.text = party.PartyName;
        _PartyPower.text = party.PartyPower.ToString();

        _SignText.text = string.Format("갑(임대인)과 을({0})은 위내용에 대해 모두동의합니다.", party.PartyName);
        _SignDate.text = System.DateTime.Now.ToString("yyyy년 MM월 dd일");

        _trHand.gameObject.SetActive(false);
        _trHand.position = _vOriPos;
        _trSign.gameObject.SetActive(false);

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
        
        int iAbleToOpen = 5;
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

        float fMonPower = float.Parse(_MonsterPower.text);
        float fPercentage = Mathf.Min(100f, 100f * iResult / fMonPower);
        _SuccessRate.text = string.Format("{0:#.##}", fPercentage);
    }

    public void Push_Admission()
    {
        Add_QuestList();
        EasyManager.Instance._curContract.Activate(false);

        StartCoroutine(Finish_Animation());
    }

    IEnumerator Finish_Animation()
    {
        _trHand.gameObject.SetActive(true);
        _trSign.gameObject.SetActive(true);

        float fTime = 2f;

        while(true)
        {
            if (fTime < 0f)
                break;

            fTime -= Time.deltaTime;

            if(fTime < 1f)
                _trHand.Translate(new Vector3(1f, -2f) * Time.deltaTime * 0.2f);

            yield return null;
        }
        
        Close_Detail();
    }

    
    public void Close_Detail()
    {
        _GameManager.Close_PopUp();
    }

    void Add_QuestList()
    {
        //LegendFramework.Quest quest = new LegendFramework.Quest(0, "title", 10, 1, 1, new int[] { 1, 1 }, new int[] { 2, 2 });

        bool[] bItem = new bool[6];

        for(int i=2; i<_itemList.Length; ++i)
        {
            if (_itemList[i]._bSelected)
            {
                Debug.Log("아이템 번호(추가): " + i + " 선택되어 QuestProgressList에 들어감.");
                bItem[i -2] = true;
            }
            else
                bItem[i - 2] = false;
        }
        EasyManager.Instance._curContract._myQuest.Trophy_Checked = bItem;

        GlobalDdataManager.QuestProgressList.Add(EasyManager.Instance._curContract._myQuest);
       // _GameManager.StartQuest()
    }

    public void Push_AddWeapon(GameObject obj)
    {
        int iSelect = 0;
        for(int i=0; i<_hunterAndWeaponList.Length; ++i)
        {
            if(_hunterAndWeaponList[i].name == obj.name)
            {
                iSelect = i;
                break;
            }
        }
        _iCurSelectedHunter = iSelect;

        _GameManager.Push_ShowWeapon(obj);
    }

    public void Select_Item(GameObject obj)
    {
        int iSelectItem = -1; // 선택한 아이템

        for (int i = 2; i < _itemList.Length; ++i)
        {
            if (_itemList[i].name == obj.name)
            {
                iSelectItem = i;
                break;
            }
        }

        if (-1 == iSelectItem) // 0,1 눌린경우
            return;

        if (_iCurSelectedItems.Contains(iSelectItem))
        {
            _iCurSelectedItems.Remove(iSelectItem);

            _itemList[iSelectItem].Select_Item(false);
        }
        else
        {
            int iAbleToSelect = int.Parse(_CheckNum.text) - 2 - 1;

            if (_iCurSelectedItems.Count >= iAbleToSelect)
            {
                _itemList[_iCurSelectedItems[0]].Select_Item(false);

                _iCurSelectedItems.RemoveAt(0);
            }

            _iCurSelectedItems.Add(iSelectItem);
            _itemList[iSelectItem].Select_Item(true);
        }
    }

    public void Give_Weapon(Weapon weapon)
    {
        _hunterAndWeaponList[_iCurSelectedHunter].Set_Weapon(weapon);
        _hunterAndWeaponList[_iCurSelectedHunter].Activate(true);

        Calculate_AllPower();
    }
}
