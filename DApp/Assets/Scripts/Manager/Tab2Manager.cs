using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab2Manager : MonoBehaviour {

    public bool isActive = false;
    GameObject myPage;

    void Awake()
    {
        myPage = GameObject.Find("Tab2Panel");
        myPage.SetActive(false);
    }

    void Update()
    {

    }

    public void CallManager()
    {
        isActive = true;
        myPage.SetActive(true);
    }

    public void DropManager()
    {
        isActive = false;
        myPage.SetActive(false);
    }
}
