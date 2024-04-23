using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
//using System.Security.Cryptography;
//using Unity.VisualScripting;


public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public List<int> playerItemInventory;
        public List<int> playerItemInventoryCount;
        public List<int> playerEquipItem;

        public string sceneName;

        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;


    }

    private PlayerController thePlayer;
    private PlayerStatus thePlayerStat;
    private DataBaseManager theDatabase;
    private Inventory theInven;
    private Equipment theEquip;

    public Data data;
    private Vector3 vector;

    public void SaveData()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    public void CallSave()
    { 
        theDatabase = FindObjectOfType<DataBaseManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayerStat = FindObjectOfType<PlayerStatus>();
        theEquip = FindObjectOfType<Equipment>();
        theInven = FindObjectOfType<Inventory>();

        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y;
        data.playerZ = thePlayer.transform.position.z;

        data.sceneName = thePlayerStat.currentSceneName;

        Debug.Log("기초 데이터 성공");

        data.playerItemInventory.Clear(); //로드상태에서 저장하여 중복으로 인한 아이템 복사 방지
        data.playerItemInventoryCount.Clear();
        data.playerEquipItem.Clear();

        for(int i = 0; i < theDatabase.var_name.Length; i++)
        {
            data.varNameList.Add(theDatabase.var_name[i]);
            data.varNumberList.Add(theDatabase.var[i]);
        }
        for (int i = 0; i < theDatabase.switch_name.Length; i++)
        {
            data.swNameList.Add(theDatabase.switch_name[i]);
            data.swList.Add(theDatabase.switches[i]);
        }

        List<Item> itemList = theInven.SaveItem();

        for(int i = 0; i < itemList.Count; i++)
        {
            Debug.Log("인벤토리의 아이템 저장 완료 :" + itemList[i].itemID);
            data.playerItemInventory.Add(itemList[i].itemID);
            data.playerItemInventoryCount.Add(itemList[i].itemCount);
        }

        for(int i = 0; i < theEquip.equipItemPack.Length; i++)
        {
            data.playerEquipItem.Add(theEquip.equipItemPack[i].itemID);
            Debug.Log("장착된 아이템 저장 완료 : " + theEquip.equipItemPack[i].itemID);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }
    public void CallLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

        if(file != null && file.Length > 0 )
        {
            data = (Data)bf.Deserialize(file);

            theDatabase = FindObjectOfType<DataBaseManager>();
            thePlayer = FindObjectOfType<PlayerController>();
            thePlayerStat = FindObjectOfType<PlayerStatus>();
            theEquip = FindObjectOfType<Equipment>();
            theInven = FindObjectOfType<Inventory>();

            thePlayerStat.currentSceneName = data.sceneName;

            vector.Set(data.playerX, data.playerY, data.playerZ);
            thePlayer.transform.position = vector;

            theDatabase.var = data.varNumberList.ToArray();
            theDatabase.var_name = data.varNameList.ToArray();
            theDatabase.switches = data.swList.ToArray();
            theDatabase.switch_name = data.swNameList.ToArray();

            for (int i = 0; i < theEquip.equipItemPack.Length; i++)
            {
                for(int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if (data.playerEquipItem[i] == theDatabase.itemList[x].itemID)
                    {
                        theEquip.equipItemPack[i] = theDatabase.itemList[x];
                        Debug.Log("장착된 아이템을 로드했습니다 :" + theEquip.equipItemPack[i].itemID);
                        break;
                    }
                }
            }

            List<Item> itemList = new List<Item>();

            for (int i = 0; i < data.playerItemInventory.Count; i++)
            {
                for (int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if (data.playerItemInventory[i] == theDatabase.itemList[x].itemID)
                    {
                        itemList.Add(theDatabase.itemList[x]);
                        Debug.Log("인벤토리 아이템을 로드했습니다 :" + theDatabase.itemList[x].itemID);
                        break;
                    }
                }
            }

            for(int i = 0; i < data.playerItemInventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerItemInventoryCount[i];
            }

            theInven.LoadItem(itemList);


            UIManager theGM = FindObjectOfType<UIManager>();
            theGM.LoadStart();

            SceneManager.LoadScene(data.sceneName);
            //
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다.");
        }

        file.Close();



        
    }







}