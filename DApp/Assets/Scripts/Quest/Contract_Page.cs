using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract_Page : MonoBehaviour {

    private GameManager GameManager;
    GameManager _GameManager
    {
        get
        {
            if (null == GameManager)
                GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            return GameManager;
        }
    }

    private GameObject QuestManager;
    GameObject _QuestManager
    {
        get
        {
            if (null == QuestManager)
                QuestManager = Resources.Load("Quest/QuestManager") as GameObject;

            return QuestManager;
        }
    }

    public void Push_ShowDetail()
    {
        Debug.Log("Show Detail");
        
        _GameManager.Show_Popup(_QuestManager);
    }

    public void Refresh_ActiveTime()
    {
        Debug.Log("Refresh_ActiveTime");
    }

    public void Refresh_UnActiveTime()
    {
        Debug.Log("Refresh_UnActiveTime");
    }
}
