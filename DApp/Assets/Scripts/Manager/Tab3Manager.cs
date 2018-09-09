using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab3Manager : MonoBehaviour {
    
    public bool isActive = false;
    public GameObject myPage;
    public GameObject _objContract;

    Gacha_Page gacha;

    int time1 = 10000, time2 = 20000;
    float fTime = 0f;
    bool bStart = false;

    void Awake()
    {
        myPage.SetActive(false);
    }

    public void init_data()
    {
        gacha = GameObject.Find("Gacha_Page(Clone)").GetComponent<Gacha_Page>();
        bStart = true;
    }

    void Update()
    {
        if (bStart)
        {
            fTime += Time.deltaTime;

            if (1f < fTime)
            {
                fTime = 0f;
                --time1;
                --time2;
                string text1 = (time1 / 3600) + "H " + ((time1 % 3600) / 60) + "M " + ((time1 % 3600) % 60) + "S ";
                string text2 = (time2 / 3600) + "H " + ((time2 % 3600) / 60) + "M " + ((time2 % 3600) % 60) + "S ";
                gacha.TimeSetting(text1, text2);
            }
        }
    }

    public void CallManager()
    {
        isActive = true;
        myPage.SetActive(true);

        if (null == _objContract)
        {
            GameObject obj = Resources.Load("Gacha/Gacha_Page") as GameObject;

            gacha = obj.GetComponent<Gacha_Page>();

            _objContract = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity, myPage.transform);
        }
    }

    public void DropManager()
    {
        isActive = false;
        myPage.SetActive(false);
    }

    public void MakeWeapon()
    {
        // 코인으로 구매하기
    }
}
