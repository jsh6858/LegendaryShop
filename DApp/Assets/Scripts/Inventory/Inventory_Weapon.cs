using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Inventory_Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject item = Resources.Load("Inventory/Item_Weapon") as GameObject;

        for (int i = 0; i < GlobalDdataManager.MyWeaponList.Count; ++i)
        {
            Weapon w = GlobalDdataManager.MyWeaponList[i];

            NGUITools.AddChild(GameObject.Find("WeaponScrollView").transform.Find("Grid").gameObject, item);
            GameObject obj = GameObject.Find("WeaponScrollView").transform.Find("Grid").GetComponent<UIGrid>().GetChild(
                GameObject.Find("WeaponScrollView").transform.Find("Grid").GetComponent<UIGrid>().GetChildList().Count - 1).gameObject;

            obj.transform.Find("Label_WeaponRank").GetComponent<UILabel>().text = w.Power.ToString();
            obj.GetComponent<Item_Weapon>().Push_ItemWeapon(w, gameObject.transform.Find("DetailView").gameObject);
        }
        GameObject.Find("WeaponScrollView").transform.Find("Grid").GetComponent<UIGrid>().Reposition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Push_CloseInventory()
    {
        EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().Close_PopUp();
    }
}
