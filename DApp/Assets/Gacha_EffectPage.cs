using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Gacha_EffectPage : MonoBehaviour {

    GameObject Sprite_1;
    GameObject Sprite_2;
    GameObject Sprite_3;
    GameObject sprite_brk;
    GameObject Sprite_White;

    GameObject Result;

    float fTime = 0f;
    float fSize = 1.2f;
    float fFactor = 1f;
    float fAlpha = 1f;
    bool bGrowth = true;
    int idx = 0;
    float fWhiteTime = 0f;
    float fWhiteSize = 1f;

    private void Awake()
    {
        Sprite_1 = gameObject.transform.Find("Sprite_Background").transform.Find("Sprite_1").gameObject;
        Sprite_2 = gameObject.transform.Find("Sprite_Background").transform.Find("Sprite_2").gameObject;
        Sprite_3 = gameObject.transform.Find("Sprite_Background").transform.Find("Sprite_3").gameObject;
        Sprite_1.transform.localScale = new Vector3(fSize, fSize, fSize);
        Sprite_3.GetComponent<UISprite>().alpha = fAlpha;

        sprite_brk = gameObject.transform.Find("Sprite_brk").gameObject;
        sprite_brk.SetActive(false);
        Sprite_White = gameObject.transform.Find("Sprite_white").gameObject;
        Sprite_White.SetActive(false);

        Result = gameObject.transform.Find("Result").gameObject;
        Result.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        Sprite_2.transform.Rotate(new Vector3(0f, 0f, -1f), 70f * Time.deltaTime);

        fTime += Time.deltaTime;
        if(1f < fTime)
        {
            fTime = 0f;
            Sprite_1.GetComponent<UISprite>().alpha = 1f;
            fSize = 1.2f;
        }
        Sprite_1.GetComponent<UISprite>().alpha -= 0.25f * Time.deltaTime;
        Sprite_1.transform.localScale = new Vector3(fSize, fSize, fSize);
        fSize -= 0.05f * Time.deltaTime;

        if (bGrowth)
        {
            fFactor += 0.2f * Time.deltaTime;
            fAlpha -= 0.5f * Time.deltaTime;
            Sprite_3.transform.localScale = new Vector3(fFactor, fFactor, fFactor);
            Sprite_3.GetComponent<UISprite>().alpha = fAlpha;
            if (1.025f < Sprite_3.transform.localScale.x)
            {
                fFactor = 1.025f;
                fAlpha = 0.5f;
                Sprite_2.transform.localScale = new Vector3(fFactor, fFactor, fFactor);
                Sprite_3.GetComponent<UISprite>().alpha = fAlpha;
                bGrowth = false;
            }
        }
        else
        {
            fFactor -= 0.2f * Time.deltaTime;
            fAlpha += 0.5f * Time.deltaTime;
            Sprite_3.transform.localScale = new Vector3(fFactor, fFactor, fFactor);
            Sprite_3.GetComponent<UISprite>().alpha = fAlpha;
            if (0.975f > Sprite_3.transform.localScale.x)
            {
                fFactor = 0.975f;
                fAlpha = 1f;
                Sprite_3.transform.localScale = new Vector3(fFactor, fFactor, fFactor);
                Sprite_3.GetComponent<UISprite>().alpha = fAlpha;
                bGrowth = true;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(0 == idx)
            {
                sprite_brk.SetActive(true);
                idx = 1;
            }
            else if(1 == idx)
            {
                sprite_brk.GetComponent<UISprite>().spriteName = "eft_brk_02";
                sprite_brk.GetComponent<UISprite>().width = 189;
                sprite_brk.GetComponent<UISprite>().height = 206;
                idx = 2;
            }
            else if(2 == idx)
            {
                sprite_brk.GetComponent<UISprite>().spriteName = "eft_brk_03";
                sprite_brk.GetComponent<UISprite>().width = 218;
                sprite_brk.GetComponent<UISprite>().height = 221;
                idx = 3;
            }
            else if(3 == idx)
            {
                Sprite_White.SetActive(true);
                idx = 4;
            }
        }

        if(4 == idx)
        {
            fWhiteTime += Time.deltaTime;

            fWhiteSize += 2f * Time.deltaTime;
            Sprite_White.transform.localScale = new Vector3(fWhiteSize, fWhiteSize, fWhiteSize);

            if (2.5f < fWhiteTime)
                idx = 5;
        }
        else if(5 == idx)
        {
            int rand = Random.RandomRange(0, 88);
            Weapon w = GlobalDdataManager.WeaponList[rand];

            GlobalDdataManager.MyWeaponList.Add(w);
            
            Result.SetActive(true);
            Result.transform.Find("Label_Type").GetComponent<UILabel>().text = w.Type.ToString();
            Result.transform.Find("Label_Name").GetComponent<UILabel>().text = w.Name;
            Result.transform.Find("Label_Power").GetComponent<UILabel>().text = w.Power.ToString();

            idx = 6;
        }
	}

    public void Close()
    {
        EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().Close_PopUp();
    }
}
