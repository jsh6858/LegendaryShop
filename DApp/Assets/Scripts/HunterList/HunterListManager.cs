using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class HunterListManager : MonoBehaviour
{
    public GameObject listItemPrefab;
    public GameObject HunterProgress;
    public UISlider hunterProgressBar;
    public TrophyCheckAction TrophyObj;

    private List<HunterQuestState> questList;
    public UIGrid grid;
    
    private bool isHaveHunting;
    int currentNum = 0;

    private void Awake()
    {
        questList = new List<HunterQuestState>();
        HunterProgress.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        grid = this.GetComponent<UIGrid>();

        if (questList == null)
        {
            questList = new List<HunterQuestState>();
        }

        if(GlobalDdataManager.QuestProgressList.Count > 0)
            HunterProgress.SetActive(true);


        for (int i = 0; i < GlobalDdataManager.QuestProgressList.Count; i++)
        {
            if (GlobalDdataManager.QuestProgressList[i].PeriodTime > 0)
                continue;

            HunterQuestState temp = GameObject.Instantiate(listItemPrefab, this.transform).GetComponent<HunterQuestState>();
            SettingQuestState(temp, i);
            AddOnClickEvent(this, temp.trophyCheckBtn, "ShowTrophy");
            questList.Add(temp);
        }
        
        currentNum = questList.Count;

        grid.Reposition();
    }

    public void AddQuestList(Quest quest)
    {
        GlobalDdataManager.QuestProgressList.Add(quest);
        isHaveHunting = true;
        HunterQuestState temp = GameObject.Instantiate(listItemPrefab, this.transform).GetComponent<HunterQuestState>();
        SettingQuestState(temp, questList.Count);
        AddOnClickEvent(this, temp.trophyCheckBtn, "ShowTrophy");
        questList.Add(temp);
    }

    public void RemoveQuestList(int index)
    {
        questList.RemoveAt(index);
        grid.Reposition();
    }

    private void Update()
    {
        return;

        for(int i = GlobalDdataManager.QuestProgressList.Count - 1; i >= 0; i--)
        {
            if(GlobalDdataManager.QuestProgressList[i].PeriodTime > 0)
            {
                isHaveHunting = true;
                break;
            }

            isHaveHunting = false;
        }

        if(isHaveHunting)
        {
            HunterProgress.SetActive(true);
            hunterProgressBar.value = questList[questList.Count - 1].progressBar.value;
        }
        else
        {
            //HunterProgress.SetActive(false);
        }

        if(GlobalDdataManager.QuestProgressList.Count != currentNum)
        {
            for (int i = 0; i < GlobalDdataManager.QuestProgressList.Count; i++)
            {
                if (GlobalDdataManager.QuestProgressList[i].PeriodTime > 0)
                    continue;

                HunterQuestState temp = GameObject.Instantiate(listItemPrefab, this.transform).GetComponent<HunterQuestState>();
                SettingQuestState(temp, i);
                AddOnClickEvent(this, temp.trophyCheckBtn, "ShowTrophy");
                questList.Add(temp);
            }

            for (int i = GlobalDdataManager.QuestProgressList.Count - 1; i >= 0; i--)
            {
                if (GlobalDdataManager.QuestProgressList[i].PeriodTime <= 0)
                    continue;

                isHaveHunting = true;
                HunterQuestState temp = GameObject.Instantiate(listItemPrefab, this.transform).GetComponent<HunterQuestState>();
                SettingQuestState(temp, i);
                AddOnClickEvent(this, temp.trophyCheckBtn, "ShowTrophy");
                questList.Add(temp);
            }
            currentNum = questList.Count;

            grid.Reposition();
        }
    }
    private void SettingQuestState(HunterQuestState src, int index)
    {
        Quest q = GlobalDdataManager.QuestProgressList[index];
        Party p = GlobalDdataManager.PartyList[q.MyParty];
        Monster m = GlobalDdataManager.MonsterList[q.MyMonster % GlobalDdataManager.MonsterList.Count];

        src.SetInfo(index, m.MonsterThumbImageId, p.PartyMembers, null, q.PeriodTime, q.PeriodTime);
    }

    public void ShowTrophy()
    {
        EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().ActivatePanelCollider(false);

        TrophyObj.gameObject.SetActive(true);
    }

    public void AddOnClickEvent(MonoBehaviour target, UIButton btn, string method)
    {
        EventDelegate onClickEvent = new EventDelegate(target, method);
        
        EventDelegate.Add(btn.onClick, onClickEvent);
    }
    
}
