using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract_Page : MonoBehaviour
{
    public Contract_Info[] _Contract_Infos;

    private GameManager GameManager;
    GameManager _GameManager
    {
        get
        {
            if (null == GameManager)
            {
                GameManager = EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>();
            }
                

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

    public void Push_ShowDetail(GameObject obj)
    {
        Debug.Log("Show Detail");

        for (int i = 0; i < _Contract_Infos.Length; ++i)
        {
            if (_Contract_Infos[i].name == obj.name)
            {
                EasyManager.Instance._curContract = _Contract_Infos[i];

                _GameManager.Show_Popup(_QuestManager);
                
                return;
            }
        }
    }

    private void Awake()
    {
        _GameManager.ShowQuest(0);
    }

    public void Refresh_ActiveTime(GameObject obj)
    {
        Debug.Log("Refresh_ActiveTime");

        for(int i=0; i< _Contract_Infos.Length; ++i)
        {
            if(_Contract_Infos[i].name == obj.name)
            {
                _Contract_Infos[i].Set_NewContract();
                return;
            }
        }
    }

    public void Refresh_UnActiveTime(GameObject obj)
    {
        Debug.Log("Refresh_UnActiveTime");

        for (int i = 0; i < _Contract_Infos.Length; ++i)
        {
            if (_Contract_Infos[i].name == obj.name)
            {
                _Contract_Infos[i].Set_NewContract();
                return;
            }
        }
    }

    public void Sort_Contract()
    {
        for(int i=0; i< _Contract_Infos.Length - 1; ++i)
        {
            for(int j=0; j< _Contract_Infos.Length -1 - i; ++j)
            {
                if (_Contract_Infos[j]._bActivate == false && _Contract_Infos[j+1]._bActivate)
                {
                    Vector2 vTemp = _Contract_Infos[j].transform.position;
                    _Contract_Infos[j].transform.position = _Contract_Infos[j + 1].transform.position;
                    _Contract_Infos[j + 1].transform.position = vTemp;

                    Contract_Info info = _Contract_Infos[j];
                    _Contract_Infos[j] = _Contract_Infos[j + 1];
                    _Contract_Infos[j + 1] = info;
                }
            }
        }
    }
    
}
