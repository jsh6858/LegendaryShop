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
    private UIGrid grid;
    
    private bool isHaveHunting;

    private void Awake()
    {
        questList = new List<HunterQuestState>();
        HunterProgress.SetActive(false);
    }

    public void Init()
    {
        grid = this.GetComponent<UIGrid>();

        if (questList == null)
        {
            questList = new List<HunterQuestState>();
        }
        
        for(int i = 0; i < GlobalDdataManager.QuestProgressList.Count; i++)
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

        grid.Reposition();
    }

    public void RemoveQuestList(int index)
    {
        questList.RemoveAt(index);
        grid.Reposition();
    }

    private void Update()
    {
        for(int i = questList.Count - 1; i >= 0; i--)
        {
            if(questList[i].isOver)
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
            HunterProgress.SetActive(false);
        }
    }
    private void SettingQuestState(HunterQuestState src, int index)
    {
        Quest q = GlobalDdataManager.QuestProgressList[index];
        Party p = GlobalDdataManager.PartyList[q.MyParty];
        Monster m = GlobalDdataManager.MonsterList[q.MyMonster];

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
