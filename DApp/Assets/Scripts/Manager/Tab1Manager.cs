using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab1Manager : MonoBehaviour {

    public bool isActive = false;
    public GameObject myPage;

    GameObject _objContract;

    void Awake()
    {
        CallManager();
    }
	
	void Update ()
    {
		
	}

    public void CallManager()
    {
        isActive = true;
        myPage.SetActive(true);

        if(null == _objContract)
        {
            GameObject obj = Resources.Load("Contract/Contract_Page") as GameObject;

            _objContract = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity, myPage.transform);
        }
    }

    public void DropManager()
    {
        isActive = false;
        myPage.SetActive(false);
    }
}
