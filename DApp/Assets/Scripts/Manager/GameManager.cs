using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Tab1Manager tab1Manager;
    Tab2Manager tab2Manager;
    Tab3Manager tab3Manager;

    Transform[] bottomBTNs;

    public Transform _trPopup;

    void Awake()
    {
        tab1Manager = GameObject.Find("Tab1Manager").GetComponent<Tab1Manager>();
        tab2Manager = GameObject.Find("Tab2Manager").GetComponent<Tab2Manager>();
        tab3Manager = GameObject.Find("Tab3Manager").GetComponent<Tab3Manager>();

        bottomBTNs = new Transform[3];
        bottomBTNs[0] = GameObject.Find("QuestList").transform;
        bottomBTNs[1] = GameObject.Find("HunterList").transform;
        bottomBTNs[2] = GameObject.Find("GachaList").transform;

        _GetAuthKeys();
        _GetPlayerName();
    }

    void Update()
    {

    }

    private void _GetAuthKeys()
    {
        // Private Key, Public Key가 저장되어 있지 않을 경우 생성
        // 저장되어 있을 경우 로드하여 서버에 접속
        // (PlayerPrefs)
    }

    private void _GetPlayerName()
    {
        // 플레이어 이름이 저장되어 있지 않을 경우 입력받기
        // 저장되어 있는 경우엔 로드
        // (PlayerPrefs)
    }

    public void CallTab1Manager()
    {
        bottomBTNs[0].localPosition = new Vector2(bottomBTNs[0].localPosition.x, -570f);
        bottomBTNs[1].localPosition = new Vector2(bottomBTNs[1].localPosition.x, -630f);
        bottomBTNs[2].localPosition = new Vector2(bottomBTNs[2].localPosition.x, -630f);

        tab1Manager.CallManager();
        tab2Manager.DropManager();
        tab3Manager.DropManager();
    }
    public void CallTab2Manager()
    {
        bottomBTNs[0].localPosition = new Vector2(bottomBTNs[0].localPosition.x, -630f);
        bottomBTNs[1].localPosition = new Vector2(bottomBTNs[1].localPosition.x, -570f);
        bottomBTNs[2].localPosition = new Vector2(bottomBTNs[2].localPosition.x, -630f);

        tab1Manager.DropManager();
        tab2Manager.CallManager();
        tab3Manager.DropManager();
    }
    public void CallTab3Manager()
    {
        bottomBTNs[0].localPosition = new Vector2(bottomBTNs[0].localPosition.x, -630f);
        bottomBTNs[1].localPosition = new Vector2(bottomBTNs[1].localPosition.x, -630f);
        bottomBTNs[2].localPosition = new Vector2(bottomBTNs[2].localPosition.x, -570f);

        tab1Manager.DropManager();
        tab2Manager.DropManager();
        tab3Manager.CallManager();
    }

    public void Show_Popup(GameObject obj)
    {
        if(_trPopup.childCount != 0)
        {
            Transform child = _trPopup.GetChild(0);

            Destroy(child.gameObject);
        }

        Instantiate(obj, Vector3.zero, Quaternion.identity, _trPopup);
    }
}
