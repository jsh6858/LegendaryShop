using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnim : MonoBehaviour {

    UISprite _uiSprite;

    string[] _szAnim = { "hunter_sd_00, hunter_sd_01, hunter_sd_20, hunter_sd_21, hunter_sd_22", "monster_sd_00", "monster_sd_01"};

    string _szSprite;
    string _szSprite2;

    float _fTime = 0f;

	// Use this for initialization
	void Start () {
        _uiSprite = GetComponent<UISprite>();

        _fTime = 1f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(_szSprite == null)
        {
            for(int i=0; i<_szAnim.Length;++i)
            {
                if(_szAnim[i] == _uiSprite.spriteName)
                {
                    if (i % 2 == 0)
                    {
                        _szSprite = _szAnim[i];
                        _szSprite2 = _szAnim[i + 1];
                    }
                    else
                    {
                        _szSprite = _szAnim[i];
                        _szSprite2 = _szAnim[i -1];
                    }
                }
            }
        }

        _fTime -= Time.deltaTime;

        if(_fTime < 0f)
        {
            _fTime = 1f;

            if(_uiSprite.spriteName == _szSprite)
            {
                _uiSprite.spriteName = _szSprite2;
            }
            else
            {
                _uiSprite.spriteName = _szSprite;
            }

        }

    }
}
