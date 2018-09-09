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

        int rank_int = (int)weapon.Grade;
        string rank = "Rank" + rank_int;

        detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = weapon.ThumbImageId;
        detailView.transform.Find("Label_Name").GetComponent<UILabel>().text = weapon.Name;
        detailView.transform.Find("Label_Property").GetComponent<UILabel>().text =
            weapon.Property + "\r\n" + weapon.Special_Ability_1 + "\r\n" + weapon.Special_Ability_2;
        detailView.transform.Find("Label_Rank").GetComponent<UILabel>().text = rank;
    }
}
