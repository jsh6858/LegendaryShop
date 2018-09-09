using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyAlignment : MonoBehaviour
{
    public Transform[] trophyList;
    private UITable uITable;

    public int testNum;

    private void Awake()
    {
        uITable = this.GetComponent<UITable>();
    }

    void OnEnable ()
    {
        SetAlignment();
    }

    private void SetAlignment()
    {
        switch(testNum)
        {
            case 1:
                trophyList[0].transform.position = new Vector2(15f, -20f);
                break;

            case 2:
                trophyList[0].transform.position = new Vector2(-75f, -20f);
                trophyList[1].transform.position = new Vector2(105f, -20f); ;
                break;

            case 3:
                trophyList[0].transform.position = new Vector2(-165f, -20f);
                trophyList[1].transform.position = new Vector2(15f, -20f);
                trophyList[2].transform.position = new Vector2(195f, -20f);
                break;

            case 4:
                trophyList[0].transform.position = new Vector2(-165f, 75f);
                trophyList[1].transform.position = new Vector2(15f, 75f);
                trophyList[2].transform.position = new Vector2(195f, 75f);
                trophyList[3].transform.position = new Vector2(15f, -115f);
                break;

            case 5:
                trophyList[0].transform.position = new Vector2(-165f, 75f);
                trophyList[1].transform.position = new Vector2(15f, 75f);
                trophyList[2].transform.position = new Vector2(195f, 75f);
                trophyList[3].transform.position = new Vector2(-75f, -115f);
                trophyList[4].transform.position = new Vector2(105f, -115f);
                break;

            case 6:
                trophyList[0].transform.position = new Vector2(-165f, 75f);
                trophyList[1].transform.position = new Vector2(15f, 75f);
                trophyList[2].transform.position = new Vector2(195f, 75f);
                trophyList[3].transform.position = new Vector2(-165f, -115f);
                trophyList[4].transform.position = new Vector2(15f, -115f);
                trophyList[5].transform.position = new Vector2(195f, -115f);
                break;
        }
        uITable.Reposition();

    }
}
