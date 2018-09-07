using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScene : MonoBehaviour {

    public Transform[] _trBottomButtons;

    public void Push_Contract()
    {
        Select_Menu(0);
    }

    public void Push_Hunter()
    {
        Select_Menu(1);
    }

    public void Push_Gacha()
    {
        Select_Menu(2);
    }

    private void Select_Menu(int iMenu)
    {
        for(int i=0; i< _trBottomButtons.Length;++i)
        {
            _trBottomButtons[i].localPosition = new Vector2(_trBottomButtons[i].localPosition.x, -630f);
        }
        _trBottomButtons[iMenu].localPosition = new Vector2(_trBottomButtons[iMenu].localPosition.x, -570f);


    }
}
