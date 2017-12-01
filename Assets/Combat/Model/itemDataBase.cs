using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson; //take a Json object and convert to c# or vice versa
using System.IO;

public class itemDataBase : MonoBehaviour
{
    private List<Item> database = new List<Item>(); //grab json object and store in database list
    private JsonData itemData; //get the Json object from the stream

    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json")); //dictionary
        ConstructItemDataBase();
        Debug.Log(GetItembyID(0).Ammo);
    }

    public Item GetItembyID(int id){
        for (int i = 0; i < database.Count;i++){
            if(database[i].ID == id){
                return database[i];
            }
        }
        return null;
    }

    void ConstructItemDataBase(){
        for (int i = 0; i < itemData.Count;i++){
            database.Add(new Item((int)itemData[i]["id"],itemData[i]["title"].ToString(),(int)itemData[i]["power"],
                                  (int)itemData[i]["ammo"],itemData[i]["description"].ToString()));
        }
    }
}

public class Item{  //title, id for the items
    public int ID { get; set; }
    public string Title { get; set;}
    public int Power { get; set;}
    public int Ammo { get; set;}
    public string Description { get; set;}

    public Item(int id,string title,int power,int ammo,string description){
        this.ID = id;
        this.Title = title;
        this.Power = power;
        this.Ammo = ammo;
        this.Description = description;
    }

    public Item(){ //create empty item for future if want to delete an item then use this with no parameter
        this.ID = -1; //will never get to this id
    }
}
