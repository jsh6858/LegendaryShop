using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class HunterProgress : MonoBehaviour {

    public UISprite _sprHun1;
    public UISprite _sprHun2;
    public UISprite _sprHun3;

    private void OnEnable()
    {
        List<Quest> listQuest = GlobalDdataManager.QuestProgressList;

        if (listQuest.Count == 0)
            return;

        Quest lastQuest = listQuest[listQuest.Count - 1];

        int[] party = GlobalDdataManager.PartyList[lastQuest.MyParty].PartyMembers;

        _sprHun1.spriteName = GlobalDdataManager.HunterList[party[0]].SDImageId;
        _sprHun2.spriteName = GlobalDdataManager.HunterList[party[1]].SDImageId;
        _sprHun3.spriteName = GlobalDdataManager.HunterList[party[2]].SDImageId;
    }
    
}
