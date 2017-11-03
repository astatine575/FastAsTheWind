using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{
    private Savedata data = new Savedata();

    public void OnClickLoadData()
    {
        Load();
    }

    public void OnClickSaveData()
    {
        Save();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/saves/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/saves/SaveData.dat", FileMode.Open);

            data = (Savedata)bf.Deserialize(file);

            CopyLoadData();

            file.Close();

            SceneManager.LoadScene(SceneIndexes.WorldMap());
        }

        
    }

    public void Save()
    {
        if (!Directory.Exists(Application.dataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.dataPath + "/saves");
        }
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.dataPath + "/saves/SaveData.dat");

        CopySaveData();

        bf.Serialize(file, data);

        file.Close();
    }

    //public static SerGameItem GameItemToSerGameItem(GameItem item)
    //{

    //}

    public static SerVector3 Vector3ToSerVector3(Vector3 v3)
    {
        SerVector3 sv3 = new SerVector3
        {
            x = v3.x,
            y = v3.y,
            z = v3.z
        };

        return sv3;
    }

    public static Vector3 SerVector3ToVector3(SerVector3 sv3)
    {
        Vector3 v3 = new Vector3
        {
            x = sv3.x,
            y = sv3.y,
            z = sv3.z
        };

        return v3;
    }


    public void CopySaveData()
    {
        //Player Globals
        data.ResourcesCount = PlayerStatus.ResourcesCount;
        data.GoldCount = PlayerStatus.GoldCount;
        data.ShipPos = Vector3ToSerVector3(PlayerStatus.ShipPos);
        data.ShipHealthMax = PlayerStatus.ShipHealthMax;
        data.ShipHealthCurrent = PlayerStatus.ShipHealthCurrent;
        data.AmmoCount = PlayerStatus.AmmoCount;

        //island globals
        data.islandLocations = new List<SerVector3>();

        foreach (Vector3 v3 in IslandStats.IslandLocations)
        {
            data.islandLocations.Add(Vector3ToSerVector3(v3));
        }

        data.visitedIslands = PlayerStatus.VisitedIslands;

        //Enemy Globals
        data.enemyShipHelthMax = EnemyStatus.ShipHealthMax;
        data.enemyShipHealthCurrent = EnemyStatus.ShipHealthCurrent;
        data.enemyGoldCount = EnemyStatus.GoldCount;
        data.enemyResourceCount = EnemyStatus.ResourcesCount;
        data.enemyAmmoCount = EnemyStatus.AmmoCount;

        //Quest Status Globals
        data.questStatusTest = QuestsStatus.testQuestStatus;

        //Ship Globals
        data.ship = PlayerStatus.Ship;
        //Inventory Globals
        data.inventory = new List<GameItem>();
        foreach(GameItem item in PlayerStatus.Inventory.inventoryList)
        {
            data.inventory.Add(item);
        }

        //copy globals into save data here
    }

    public void CopyLoadData()
    {
        //Player Globals
        PlayerStatus.ResourcesCount = data.ResourcesCount;
        PlayerStatus.GoldCount = data.GoldCount;
        PlayerStatus.ShipPos = SerVector3ToVector3(data.ShipPos);
        PlayerStatus.ShipHealthMax = data.ShipHealthMax;
        PlayerStatus.ShipHealthCurrent = data.ShipHealthCurrent;
        PlayerStatus.AmmoCount = data.AmmoCount;


        //Island Globals
        IslandStats.IslandLocations = new List<Vector3>();

        foreach(SerVector3 sv3 in data.islandLocations)
        {
            IslandStats.IslandLocations.Add(SerVector3ToVector3(sv3));
        }

        PlayerStatus.VisitedIslands = data.visitedIslands;

        //Enemy Globals
        EnemyStatus.ShipHealthMax = data.enemyShipHelthMax;
        EnemyStatus.ShipHealthCurrent = data.enemyShipHealthCurrent;
        EnemyStatus.GoldCount = data.enemyGoldCount;
        EnemyStatus.ResourcesCount = data.enemyResourceCount;
        EnemyStatus.AmmoCount = data.enemyResourceCount;

        //Quest Status Globals
        QuestsStatus.testQuestStatus = data.questStatusTest;

        //Ship Globals
        PlayerStatus.Ship = data.ship;

        //Inventory Globals
        Inventory inventory = new Inventory();
        foreach (GameItem item in data.inventory)
        {
            inventory.inventoryList.Add(item);
        }
        PlayerStatus.Inventory = inventory;


        //copy globals to globals from save data here
    }
}
[System.Serializable]
public class SerVector3
{
    public float x;
    public float y;
    public float z;
}


[System.Serializable]
public class Savedata
{
    //Player Stat Globals
    public  int ResourcesCount;
    public  int GoldCount;
    public SerVector3 ShipPos;
    public int ShipHealthMax;
    public int ShipHealthCurrent;
    public int AmmoCount;

    //Island Stat globals
    public List<SerVector3> islandLocations;
    public ArrayList visitedIslands;

    //Enemy Stat Globals
    public int enemyShipHelthMax;
    public int enemyShipHealthCurrent;
    public int enemyGoldCount;
    public int enemyResourceCount;
    public int enemyAmmoCount;

    //Quest Status Globals
    public int questStatusTest;

    //Ship Globals
    public Ship ship;

    //Inventory Globals
    public List<GameItem> inventory;
    
}
