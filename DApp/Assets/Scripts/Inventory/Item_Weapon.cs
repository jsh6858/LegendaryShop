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

        int rank_int = 0;
        switch (weapon.Grade)
        {
            case WEAPON_GRADE.VERY_HIGH:
                rank_int = 1;
                break;
            case WEAPON_GRADE.LEGENDARY:
                rank_int = 2;
                break;
            case WEAPON_GRADE.LEGENDARY_TWO:
                rank_int = 3;
                break;
            case WEAPON_GRADE.LEGENDARY_LAST:
                rank_int = 4;
                break;
        }

        if (WEAPON_GRADE.VERY_HIGH == weapon.Grade)
            gameObject.transform.Find("Sprite_Outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_0";
        else if (WEAPON_GRADE.LEGENDARY == weapon.Grade)
            gameObject.transform.Find("Sprite_Outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_1";
        else if (WEAPON_GRADE.LEGENDARY_TWO == weapon.Grade)
            gameObject.transform.Find("Sprite_Outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_2";
        else if (WEAPON_GRADE.LEGENDARY_LAST == weapon.Grade)
            gameObject.transform.Find("Sprite_Outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_3";

        gameObject.transform.Find("Label_WeaponRank").GetComponent<UILabel>().text = weapon.Name;//"Rank" + rank_int;
        gameObject.transform.Find("Sprite_Weapon").GetComponent<UISprite>().spriteName = weapon.ThumbImageId;

    }

    public void Click()
    {
        window.selectedWeapon = weapon;

        int rank_int = 0;

        switch(weapon.Grade)
        {
            case WEAPON_GRADE.VERY_HIGH:
                rank_int = 1;
                break;
            case WEAPON_GRADE.LEGENDARY:
                rank_int = 2;
                break;
            case WEAPON_GRADE.LEGENDARY_TWO:
                rank_int = 3;
                break;
            case WEAPON_GRADE.LEGENDARY_LAST:
                rank_int = 4;
                break;
        }
        
        if (WEAPON_GRADE.VERY_HIGH == weapon.Grade)
            detailView.transform.Find("outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_0";
        else if (WEAPON_GRADE.LEGENDARY == weapon.Grade)
            detailView.transform.Find("outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_1";
        else if (WEAPON_GRADE.LEGENDARY_TWO == weapon.Grade)
            detailView.transform.Find("outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_2";
        else if (WEAPON_GRADE.LEGENDARY_LAST == weapon.Grade)
            detailView.transform.Find("outline").GetComponent<UISprite>().spriteName = "weapon_grade_frame_3";

        detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = weapon.ThumbImageId;
        detailView.transform.Find("Label_Name").GetComponent<UILabel>().text = weapon.Name;
        detailView.transform.Find("Label_Property").GetComponent<UILabel>().text =
            weapon.Property + "\r\n" + weapon.Special_Ability_1 + "\r\n" + weapon.Special_Ability_2;
        detailView.transform.Find("Label_Rank").GetComponent<UILabel>().text = "Rank" + rank_int;
    }
}
