using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponinventory: MonoBehaviour
{
  
    public GameObject[] inven = new GameObject[12];
    public Button[] InventoryButton = new Button[12];


    public void addItem(GameObject item){
        bool itemAdded = false;
        //find the first open slot in the inventory
        for (int i = 0; i < inven.Length;i++){
            if(inven[i] == null){
                inven[i] = item;
                //update the UI, change the image of the button
                InventoryButton[i].image.overrideSprite = item.GetComponent<SpriteRenderer>().sprite;
                Debug.Log(item.name + "was added");
                itemAdded = true;

                //item.SendMessage("DoInteraction");
                break;
            }
        }
        // if the item not added
        if(!itemAdded){
            Debug.Log("Inventory is Full, Item not added");
        }
    }

    public void RemoveItem(GameObject item){
        for (int i = 0; i < inven.Length;i++){
            if(inven[i] == item){
                inven[i] = null;
                //update the UI, change the image of the button
                InventoryButton[i].image.overrideSprite = null;
                Debug.Log(item.name + "was removed");
                break;
            }
        }
    }

    public bool FindItem(GameObject item) { //this use for using the items in the inventory later on
        for (int i = 0; i < inven.Length;i++){
            if(inven[i] == item){ //find the item
                return true;
            }
        }
        return false;  //item not found
    }
}
