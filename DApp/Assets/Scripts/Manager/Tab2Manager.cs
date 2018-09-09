using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab2Manager : MonoBehaviour {

    public bool isActive = false;
    public GameObject myPage;
    public GameObject listItemPrefab;
    public int testNum;

    public HunterListManager listManager;

    GameObject _objHunter;
    public ScreenFader fader;
    UIScrollView view;

    void Awake()
    {
        myPage.SetActive(false);
        fader.fadeOutSpeed = 5f;

        fader.onFadeOutComplete = () => { Debug.Log("FadeOut Complete"); fader.QuickSetFadeIn(); };

        GameObject obj = Resources.Load("HunterList/HunterList") as GameObject;

        _objHunter = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity, myPage.transform);
    }

    public void Init()
    {
        listManager.Init();
    }
    

    public void CallManager()
    {
        isActive = true;
        myPage.SetActive(true);

        if (null == _objHunter)
        {
            GameObject obj = Resources.Load("HunterList/HunterList") as GameObject;

            _objHunter = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity, myPage.transform);
        }
    }

    public void DropManager()
    {
        isActive = false;
        myPage.SetActive(false);
    }
}
