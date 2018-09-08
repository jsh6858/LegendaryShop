using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab1Manager : MonoBehaviour {

    public bool isActive = false;
    public GameObject myPage;

    void Awake()
    {
        myPage.SetActive(true);
    }
	
	void Update ()
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
