using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Push_CloseInventory()
    {
        EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().Close_PopUp();
    }
}
