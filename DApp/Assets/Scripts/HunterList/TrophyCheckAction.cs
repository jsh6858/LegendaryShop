using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyCheckAction : MonoBehaviour
{
    public MyTweenPosition myTopGuiTweenPos, myTrophyTweenPos;
    public MyTweenTextColor[] myTweenTextColors;
    public MyTweenSpriteColor[] myTweenSpriteColors;
    public MyTweenSpriteColor[] myBasicTrophyTweenSpriteColors, myRandomTrophyTweenSpriteColors;
    public MyTweenScale[] myTrophyTweenScales;
    public MyTweenSpriteColor backgroundImg;
    public MyTweenPosition[] myTrophySlots;

    public GameObject[] basicTrophyObj;
    public GameObject[] randomTrophys;

    public GameObject[] xMarks, oMarks, grays;

    public UITable uiTable;
    public GameObject button;

    public TrophyForConfirm tfc;

    public int testNum;

    private int openCount;
    private bool isOpenStart;
    private List<int> indexs;
    private Vector3 basic_1_pos, basic_2_pos;
    bool isTouch = false;

    private void Awake()
    {
        basic_1_pos = basicTrophyObj[0].transform.position;
        basic_2_pos = basicTrophyObj[1].transform.position;
    }
    private void OnEnable()
    {
        myTopGuiTweenPos.TweenStart();
        for (int i = 0; i < myTweenTextColors.Length; i++)
            myTweenTextColors[i].TweenStart(true);
        for (int i = 0; i < myTweenSpriteColors.Length; i++)
            myTweenSpriteColors[i].TweenStart(true);

        if(indexs == null)
        {
            indexs = new List<int>();
        }
        indexs.Clear();
        backgroundImg.GetComponent<UISprite>().color = Color.white;

        basicTrophyObj[0].transform.position = basic_1_pos;
        basicTrophyObj[1].transform.position = basic_2_pos;

        StartCoroutine(CoOpen());
    }

    private void OnDisable()
    {
        for (int i = 0; i < basicTrophyObj.Length; i++)
            basicTrophyObj[i].SetActive(false);

        for (int i = 0; i < randomTrophys.Length; i++)
            randomTrophys[i].SetActive(false);
        
        for(int i = 0; i < xMarks.Length; i++)
        {
            xMarks[i].SetActive(false);
            grays[i].SetActive(false);
        }

        for(int i = 0; i < oMarks.Length; i++)
            oMarks[i].SetActive(false);

        button.SetActive(false);
    }

    public void TrophyMove()
    {
        for (int i = 0; i < basicTrophyObj.Length; i++)
            basicTrophyObj[i].SetActive(true);

        for (int i = 0; i < testNum; i++)
        {
            randomTrophys[i].SetActive(true);
        }

        myTrophyTweenPos.TweenStart(true);

        for(int i = 0; i < myBasicTrophyTweenSpriteColors.Length; i++)
            myBasicTrophyTweenSpriteColors[i].TweenStart(true);

        for (int i = 0; i < testNum; i++)
            myRandomTrophyTweenSpriteColors[i].TweenStart(true);

        SetAlignment();
        openCount = testNum + 2;

        isOpenStart = true;
        
    }

    private void SetAlignment()
    {
        switch (testNum)
        {
            case 1:
                randomTrophys[0].transform.localPosition = new Vector2(15f, -20f);
                break;

            case 2:
                randomTrophys[0].transform.localPosition = new Vector2(-75f, -20f);
                randomTrophys[1].transform.localPosition = new Vector2(105f, -20f);
                break;

            case 3:
                randomTrophys[0].transform.localPosition = new Vector2(-165f, -20f);
                randomTrophys[1].transform.localPosition = new Vector2(15f, -20f);
                randomTrophys[2].transform.localPosition = new Vector2(195f, -20f);
                break;

            case 4:
                randomTrophys[0].transform.localPosition = new Vector2(-165f, 75f);
                randomTrophys[1].transform.localPosition = new Vector2(15f, 75f);
                randomTrophys[2].transform.localPosition = new Vector2(195f, 75f);
                randomTrophys[3].transform.localPosition = new Vector2(15f, -115f);
                break;

            case 5:
                randomTrophys[0].transform.localPosition = new Vector2(-165f, 75f);
                randomTrophys[1].transform.localPosition = new Vector2(15f, 75f);
                randomTrophys[2].transform.localPosition = new Vector2(195f, 75f);
                randomTrophys[3].transform.localPosition = new Vector2(-75f, -115f);
                randomTrophys[4].transform.localPosition = new Vector2(105f, -115f);
                break;

            case 6:
                randomTrophys[0].transform.localPosition = new Vector2(-165f, 75f);
                randomTrophys[1].transform.localPosition = new Vector2(15f, 75f);
                randomTrophys[2].transform.localPosition = new Vector2(195f, 75f);
                randomTrophys[3].transform.localPosition = new Vector2(-165f, -115f);
                randomTrophys[4].transform.localPosition = new Vector2(15f, -115f);
                randomTrophys[5].transform.localPosition = new Vector2(195f, -115f);
                break;
        }

    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isTouch = true;
            }
        }
    }
    private IEnumerator CoOpen()
    {
        while (!isOpenStart)
            yield return null;

        yield return null;
        int index = 0;
        bool isNotOpen = true;
        float time = 0f;
        

        while(openCount != 0)
        {
            while (isNotOpen)
            {
                if (Input.GetMouseButtonDown(0))
                    isNotOpen = false;
                else if(isTouch)
                {
                    isNotOpen = false;
                    isTouch = false;
                }
                yield return null;
            }

            myTrophyTweenScales[index].TweenStart();
            while (time < 0.5f)
            {
                time += Time.deltaTime;
                if (Input.GetMouseButtonDown(0))
                {
                    myTrophyTweenScales[index].ResetTween();
                    break;
                }
                else if (isTouch)
                {
                    isTouch = false;
                    myTrophyTweenScales[index].ResetTween();
                    break;
                    
                }
                yield return null;
            }

            if (index > 1)
            {
                int gatcha = UnityEngine.Random.Range(0, 2);
                if (gatcha == 0)
                {
                    xMarks[index].SetActive(true);
                    grays[index].SetActive(true);
                }
                else
                {
                    oMarks[index].SetActive(true);
                    indexs.Add(index);
                }
            }
            else
            {
                oMarks[index].SetActive(true);
                indexs.Add(index);
            }
            time = 0f;
            isNotOpen = true;
            openCount--;
            index++;
        }

        button.SetActive(true);
        isOpenStart = false;
    }

    public void ConfirmClick()
    {
        backgroundImg.TweenStart(false);
        for(int i = 0; i < myTweenTextColors.Length; i++)
        {
            myTweenTextColors[i].TweenStart(false);
        }

        for(int i = 0; i < myTweenSpriteColors.Length; i++)
        {
            myTweenSpriteColors[i].TweenStart(false);
        }

        for(int i = 0; i < myBasicTrophyTweenSpriteColors.Length; i++)
        {
            myBasicTrophyTweenSpriteColors[i].TweenStart(false);
        }

        for(int i = 0; i < testNum; i++)
        {
            myRandomTrophyTweenSpriteColors[i].TweenStart(false);
        }

        for(int i = 0; i < xMarks.Length; i++)
        {
            xMarks[i].SetActive(false);
            oMarks[i].SetActive(false);
            grays[i].SetActive(false);
        }

        for(int i = 0; i < myTrophySlots.Length; i++)
        {
            if (myTrophySlots[i].gameObject.activeSelf)
                myTrophySlots[i].TweenStart(true);
        }
    }

    public void TrophyCheckEnd()
    {
        EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().ActivatePanelCollider(true);
        this.gameObject.SetActive(false);
        for (int i = 0; i < myTrophySlots.Length; i++)
        {
            myTrophySlots[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
