using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab3Manager : MonoBehaviour {
    
    public bool isActive = false;
    public GameObject myPage;
    public GameObject _objContract;

    void Awake()
    {
        myPage.SetActive(false);
    }

    void Update()
    {

    }

    public void CallManager()
    {
        isActive = true;
        myPage.SetActive(true);

        if (null == _objContract)
        {
            GameObject obj = Resources.Load("Gacha/Gacha_Page") as GameObject;

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
