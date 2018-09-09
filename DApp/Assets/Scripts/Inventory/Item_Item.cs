using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LegendFramework;

public class Item_Item : MonoBehaviour {

    Inventory_Item window;
    Item item;
    GameObject detailView;

    public void Push_Item(Inventory_Item popup_Window, Item w, GameObject view)
    {
        window = popup_Window;
        item = w;
        detailView = view;
        
        switch (item.Type)
        {
            case ITEM_TYPE.TEETH:
                gameObject.transform.Find("Sprite_Weapon").GetComponent<UISprite>().spriteName = "item_thumb_01";
                break;
            case ITEM_TYPE.MEAT:
                gameObject.transform.Find("Sprite_Weapon").GetComponent<UISprite>().spriteName = "item_thumb_11";
                break;
            case ITEM_TYPE.BONE:
                gameObject.transform.Find("Sprite_Weapon").GetComponent<UISprite>().spriteName = "item_thumb_21";
                break;
            case ITEM_TYPE.SKIN:
                gameObject.transform.Find("Sprite_Weapon").GetComponent<UISprite>().spriteName = "item_thumb_31";
                break;
            case ITEM_TYPE.TOENAIL:
                gameObject.transform.Find("Sprite_Weapon").GetComponent<UISprite>().spriteName = "item_thumb_41";
                break;
            case ITEM_TYPE.TREASURE:
                gameObject.transform.Find("Sprite_Weapon").GetComponent<UISprite>().spriteName = "item_thumb_51";
                break;
        }
        
        gameObject.transform.Find("Label_WeaponRank").GetComponent<UILabel>().text = item.Name;

    }

    public void Click()
    {
        window.selectedItem = item;
        
        switch(item.Type)
        {
            case ITEM_TYPE.TEETH:
                detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = "item_thumb_01";
                break;
            case ITEM_TYPE.MEAT:
                detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = "item_thumb_11";
                break;
            case ITEM_TYPE.BONE:
                detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = "item_thumb_21";
                break;
            case ITEM_TYPE.SKIN:
                detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = "item_thumb_31";
                break;
            case ITEM_TYPE.TOENAIL:
                detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = "item_thumb_41";
                break;
            case ITEM_TYPE.TREASURE:
                detailView.transform.Find("Sprite_Icon").GetComponent<UISprite>().spriteName = "item_thumb_51";
                break;
        }
        
        detailView.transform.Find("Label_Name").GetComponent<UILabel>().text = item.Name;

        int rand = Random.Range(0, 5);
        string str = "";
        switch(rand)
        {
            case 0:
                str = "[ID]:SSASD159aefaw5WWFgasdwafsaWASFW873wSAW12";
                break;

            case 1:
                str = "[ID]:awWAD5asWD564WADasdaDWs513WDSAWADs53AWDs";
                break;

            case 2:
                str = "[ID]:HJK156DHFJHHw53wdjsadklWADA56JEDNF<W4156";
                break;

            case 3:
                str = "[ID]:neonnahanteNwoni1bon2Bon3Bonjeongdabuens";
                break;

            case 4:
                str = "[ID]:gundansWasaW46WAaW3WAS31WDsaWD84sW83sW8s";
                break;

            case 5:
                str = "[ID]:zasoseoYem89byeongha546NAgaDuizy456awASD";
                break;

        }
        detailView.transform.Find("Label_ID").GetComponent<UILabel>().text = str;

        detailView.transform.Find("Label_Property").GetComponent<UILabel>().text = item.Property;
    }
}
