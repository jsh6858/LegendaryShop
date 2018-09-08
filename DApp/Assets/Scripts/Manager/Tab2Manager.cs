using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab2Manager : MonoBehaviour {

    public bool isActive = false;
    public GameObject myPage;

    public ScreenFader fader;

    void Awake()
    {
        myPage.SetActive(false);
        fader.fadeOutSpeed = 5f;

        fader.onFadeOutComplete = () => { Debug.Log("FadeOut Complete"); fader.QuickSetFadeIn(); };
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            // 입력을 막음
            StartCoroutine(fader.FadeOut());
        }
    }
    

    public void CallManager()
    {
        isActive = true;
        myPage.SetActive(true);
    }

    public void DropManager()
    {
        isActive = false;
        myPage.SetActive(false);
    }
}
