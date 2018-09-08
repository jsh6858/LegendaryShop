using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScene : MonoBehaviour {

    public Transform[] _trBottomButtons;

    public GameObject[] _objPages;

    public GameObject[] _objPanels;

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
            if(i == iMenu)
                _trBottomButtons[i].localPosition = new Vector2(_trBottomButtons[i].localPosition.x, -570f);
            else
                _trBottomButtons[i].localPosition = new Vector2(_trBottomButtons[i].localPosition.x, -630f);
        }
        
        for (int i = 0; i < _objPages.Length; ++i)
        {
            if (i == iMenu)
                _objPages[i].SetActive(true);
            else
                _objPages[i].SetActive(false);
        }
    }

    public void Set_FixedPanel(bool b)
    {
        for (int i = 0; i < _objPanels.Length; ++i)
            _objPanels[i].SetActive(b);
    }

    public void CloseAllPages()
    {
        for (int i = 0; i < _objPages.Length; ++i)
        {
            _objPages[i].SetActive(false);
        }
    }
}
