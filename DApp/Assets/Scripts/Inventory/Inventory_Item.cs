using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Inventory_Item : MonoBehaviour {

    public Item selectedItem;

    GameObject detailView;

    private void Awake()
    {
        GameObject item = Resources.Load("Inventory/Item_Item") as GameObject;

        GameObject gridObj = gameObject.transform.Find("WeaponScrollView").transform.Find("Grid").gameObject;
        UIGrid grid = gridObj.GetComponent<UIGrid>();
        detailView = gameObject.transform.Find("DetailView").gameObject;

        for (int i = 0; i < GlobalDdataManager.MyItemList.Count; ++i)
        {
            Item child = GlobalDdataManager.MyItemList[i];

            NGUITools.AddChild(gridObj, item);
            GameObject newObj = grid.GetChild(grid.GetChildList().Count - 1).gameObject;
            
            newObj.transform.Find("Label_WeaponRank").GetComponent<UILabel>().text = child.Name;
            newObj.GetComponent<Item_Item>().Push_Item(this, child, detailView);
        }
        grid.Reposition();
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Push_CloseInventory()
    {
        EasyManager.Instance.GetObj("GameManager").GetComponent<GameManager>().Close_PopUp();
    }
}
