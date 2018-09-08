using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Item_Weapon : MonoBehaviour {

    Weapon myWeapon;
    GameObject detailView;

    public void Push_ItemWeapon(Weapon w, GameObject view)
    {
        myWeapon = w;
        detailView = view;
    }

    public void Click()
    {
        detailView.transform.Find("Label_WeaponName").GetComponent<UILabel>().text = myWeapon.Name;
    }
}
