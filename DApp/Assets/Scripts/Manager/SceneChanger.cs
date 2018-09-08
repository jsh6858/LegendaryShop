using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger {

    private static SceneChanger _Instance;
    public static SceneChanger Instance
    {
        get
        {
            if (null == _Instance)
                _Instance = new SceneChanger();

            return _Instance;
        }
    }

    public void TitleToPlay()
    {
        SceneManager.LoadSceneAsync("Play");
    }

    public void PlayToTitle()
    {
        SceneManager.LoadSceneAsync("Title");
    }
}
