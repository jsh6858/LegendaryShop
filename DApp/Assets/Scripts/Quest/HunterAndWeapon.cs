using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class HunterAndWeapon : MonoBehaviour {

    // Hunter
    public UILabel _Level;
    public UILabel _Name;
    public UILabel _Power;
    public UISprite _SprHunter;
    public UISprite _SprBestWeapon;
    public UILabel _Number;

    // Weapon
    public UILabel _WpLevel;
    public UILabel _Percentage;
    public UILabel _WpPower;
    public UISprite _SprWeapon;
    public UISprite _SprWeaponFrame;
    public UISprite _SprBestChoice;

    GameObject _objSelect;
    GameObject objSelect
    {
        get
        {
            if (null == _objSelect)
                _objSelect = transform.Find("Weapon/Select").gameObject;
            return _objSelect;
        }
    }

    GameObject _objUnselect;
    GameObject objUnselect
    {
        get
        {
            if (null == _objUnselect)
                _objUnselect = transform.Find("Weapon/Unselect").gameObject;
            return _objUnselect;
        }
    }

    public bool _bActivate = false;

    public void Set_Hunter(Hunter hunter, int iNum)
    {
        _Level.text = "Lv " + Random.Range(50, 99);
        _Name.text = hunter.Name;
        _Power.text = hunter.Power.ToString();
        _SprHunter.spriteName = hunter.ThumbImageId.ToString();
        _SprBestWeapon.spriteName = Max_Value(hunter.Mastery).ToString();
        _Number.text = iNum.ToString();
    }

    public void Set_Weapon(Weapon weapon)
    {
        _WpLevel.text = "Lv " + Random.Range(50, 99);
        _Percentage.text = string.Format("({0:#})%", Random.Range(60f, 99f));
        _WpPower.text = weapon.Power.ToString();
        _SprWeapon.spriteName = weapon.ThumbImageId.ToString();
        _SprWeaponFrame.spriteName = "weapon_grade_frame_" + (int)weapon.Grade;
        //_SprBestChoice = weapon.
    }

    int Max_Value(int[] array)
    {
        int iResult = int.MinValue;

        for(int i=0; i<array.Length;++i)
        {
            if(array[i] > iResult)
                iResult = array[i];
        }

        return iResult;
    }

    public void Activate(bool b)
    {
        if(b)
        {
            objSelect.SetActive(true);
            objUnselect.SetActive(false);
        }
        else
        {
            objSelect.SetActive(false);
            objUnselect.SetActive(true);
        }
        _bActivate = b;
    }
}
