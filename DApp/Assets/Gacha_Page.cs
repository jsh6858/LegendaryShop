using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha_Page : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Push_ShowMakeWeapon()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Push_ShowMakeWeapon();
    }
}
