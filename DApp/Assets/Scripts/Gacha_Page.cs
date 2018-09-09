using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha_Page : MonoBehaviour {

    public bool making = false;

    UILabel timer1, timer2;

    private void Awake()
    {
        timer1 = gameObject.transform.Find("Child1").transform.Find("Label").GetComponent<UILabel>();
        timer2 = gameObject.transform.Find("Child2").transform.Find("Label").GetComponent<UILabel>();

        int time1 = 10000;
        int time2 = 20000;
        timer1.text = (time1 / 3600) + "H " + ((time1 % 3600) / 60) + "M " + ((time1 % 3600) % 60) + "S ";
        timer2.text = (time2 / 3600) + "H " + ((time2 % 3600) / 60) + "M " + ((time2 % 3600) % 60) + "S ";

        GameObject.Find("Tab3Manager").GetComponent<Tab3Manager>().init_data();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void TimeSetting(string text1, string text2)
    {
        timer1.text = text1;
        timer2.text = text2;
    }

    public void Push_ShowMakeWeapon()
    {
        if (!making)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Push_ShowMakeWeapon();
            making = true;
        }
    }
}
