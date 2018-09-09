using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Inventory_Weapon : MonoBehaviour {

    public Weapon selectedWeapon;

    GameObject detailView;

    private void Awake()
    {
        GameObject item = Resources.Load("Inventory/Item_Weapon") as GameObject;

        GameObject gridObj = gameObject.transform.Find("WeaponScrollView").transform.Find("Grid").gameObject;
        UIGrid grid = gridObj.GetComponent<UIGrid>();
        detailView = gameObject.transform.Find("DetailView").gameObject;

        for (int i = 0; i < GlobalDdataManager.MyWeaponList.Count; ++i)
        {
            Weapon w = GlobalDdataManager.MyWeaponList[i];

            NGUITools.AddChild(gridObj, item);
            GameObject newObj = grid.GetChild(grid.GetChildList().Count - 1).gameObject;

            int rank = (int)w.Power;
            newObj.transform.Find("Label_WeaponRank").GetComponent<UILabel>().text = "Rank" + rank;
            newObj.GetComponent<Item_Weapon>().Push_ItemWeapon(this, w, detailView);
        }
        grid.Reposition();
        
        if (EasyManager.Instance._bChooseWeapon)
            detailView.transform.Find("Button_Select").gameObject.SetActive(true);
        else
            detailView.transform.Find("Button_Select").gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Push_CloseInventory()
    {
        EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().Close_PopUp();
    }

    public void Push_SelectWeapon()
    {
        if (null != selectedWeapon)
        {
            EasyManager.Instance.Weapon_Select(selectedWeapon);
            EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().Close_PopUp();
        }
    }
}
