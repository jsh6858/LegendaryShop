using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class HunterQuestState : MonoBehaviour
{
    public UISprite monsterImage;
    public UISprite bgImg;
    public UISprite[] hunterImg;
    public UISprite[] weaponImg;
    public UIProgressBar progressBar;
    public UIButton trophyCheckBtn;
    public UILabel progressText;
    public UILabel peroidTimeText;

    public bool isOver;
    private int questIdex;
    private int questOriginTime;

    public void SetInfo(int questId, string monsterImgName, int[] hunterImgs, int[] weaponImgs, int periodTime, int originTime)
    {
        questIdex = questId;
        questOriginTime = originTime;

        monsterImage.spriteName = monsterImgName;

        for(int i = 0; i < hunterImg.Length; i++)
        {
            hunterImg[i].spriteName = GlobalDdataManager.HunterList[hunterImgs[i]].ThumbImageId;
        }

        if(periodTime > 0)
        {
            float val = 1 - (periodTime / originTime);
            progressBar.value = val;

            progressBar.gameObject.SetActive(true);
            peroidTimeText.gameObject.SetActive(true);
            trophyCheckBtn.gameObject.SetActive(false);

            progressText.text = (val * 100).ToString("N1");
            peroidTimeText.text = UsefulFunction.TimeToString(periodTime);

            // bg 테두리 이미지 노말
            // bgImg.spriteName = nomal;
        }
        else
        {
            progressBar.gameObject.SetActive(false);
            peroidTimeText.gameObject.SetActive(false);
            trophyCheckBtn.gameObject.SetActive(true);

            isOver = true;
            progressText.text = "100%";

            // bg 테투리 이미지 초록
            // bgImg.spriteName = complete;
        }

        // 무기는 나중에...
        /*
        for (int i = 0; i < weaponImg.Length; i++)
        {
            weaponImg[i].spriteName = GlobalDdataManager.WeaponList[weaponImgs[i]].ThumbImageId;
        }
        */
    }

    private void Update()
    {
        if(!isOver)
        {
            if (GlobalDdataManager.QuestProgressList[questIdex].PeriodTime > 0)
            {
                int periodTime = GlobalDdataManager.QuestProgressList[questIdex].PeriodTime;
                float val = 1 - (periodTime / questOriginTime);
                progressBar.value = val;

                progressText.text = (val * 100).ToString("N1");
                peroidTimeText.text = UsefulFunction.TimeToString(periodTime);
            }
            else
                isOver = true;
        }
        else
        {
            // 테두리 변경
            progressBar.gameObject.SetActive(false);
            peroidTimeText.gameObject.SetActive(false);
            trophyCheckBtn.gameObject.SetActive(true);

            progressText.text = "100%";
        }
    }
}
