using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyForConfirm : MonoBehaviour
{
    public MyTweenSpriteColor[] allSprites;
    public MyTweenTextColor[] allTexts;
    public MyTweenPosition[] trophyPos;

    public void TweenStart(int[] indexs)
    {
        for (int i = 0; i < allSprites.Length; i++)
        {
            allSprites[i].TweenStart(false);
        }

        for (int i = 0; i < allTexts.Length; i++)
        {
            allTexts[i].TweenStart(false);
        }

        for(int i = 0; i < indexs.Length; i++)
        {
            trophyPos[indexs[i]].TweenStart();
        }
    }
}
