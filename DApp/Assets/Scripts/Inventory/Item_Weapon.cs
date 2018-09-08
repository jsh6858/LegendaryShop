using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Item_Weapon : MonoBehaviour {

    Inventory_Weapon window;
    Weapon weapon;
    GameObject detailView;

    public void Push_ItemWeapon(Inventory_Weapon popup_Window, Weapon w, GameObject view)
    {
        window = popup_Window;
        weapon = w;
        detailView = view;
    }

    public void Click()
    {
        window.selectedWeapon = weapon;
        detailView.transform.Find("Label_WeaponName").GetComponent<UILabel>().text = weapon.Name;
    }
}
