using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Collections;
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
        public int mapNumber;//�ʹ޾ƿ���

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


    public void CallSave()
    { 
        theDatabase = FindObjectOfType<DataBaseManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayerStat = FindObjectOfType<PlayerStatus>();
        theEquip = FindObjectOfType<Equipment>();
        theInven = FindObjectOfType<Inventory>();
        data.mapNumber = MapMaker.Instance.curMapId;

        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y; //�� ���� ������ ���� y��ǥ�� 3 ���ϱ�
        data.playerZ = thePlayer.transform.position.z;

        data.sceneName = thePlayerStat.currentSceneName;

        Debug.Log("���� ������ ����");

        data.playerItemInventory.Clear(); //�ε���¿��� �����Ͽ� �ߺ����� ���� ������ ���� ����
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
            Debug.Log("�κ��丮�� ������ ���� �Ϸ� :" + itemList[i].itemID);
            data.playerItemInventory.Add(itemList[i].itemID);
            data.playerItemInventoryCount.Add(itemList[i].itemCount);
        }

        for(int i = 0; i < theEquip.equipItemPack.Length; i++)
        {
            data.playerEquipItem.Add(theEquip.equipItemPack[i].itemID);
            Debug.Log("������ ������ ���� �Ϸ� : " + theEquip.equipItemPack[i].itemID);
        }



        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "�� ��ġ�� �����߽��ϴ�.");


    }
    public void CallLoad()
    {

        StartCoroutine(Te());
        
    }


    IEnumerator Te()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);
            SceneManager.LoadScene(data.sceneName);
            yield return null;
            theDatabase = FindObjectOfType<DataBaseManager>();
            thePlayerStat = FindObjectOfType<PlayerStatus>();
            theEquip = FindObjectOfType<Equipment>();
            theInven = FindObjectOfType<Inventory>();

            thePlayerStat.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);

            theDatabase.var = data.varNumberList.ToArray();
            theDatabase.var_name = data.varNameList.ToArray();
            theDatabase.switches = data.swList.ToArray();
            theDatabase.switch_name = data.swNameList.ToArray();

            for (int i = 0; i < theEquip.equipItemPack.Length; i++)
            {
                for (int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if (data.playerEquipItem[i] == theDatabase.itemList[x].itemID)
                    {
                        theEquip.equipItemPack[i] = theDatabase.itemList[x];
                        Debug.Log("������ �������� �ε��߽��ϴ� :" + theEquip.equipItemPack[i].itemID);
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
                        Debug.Log("�κ��丮 �������� �ε��߽��ϴ� :" + theDatabase.itemList[x].itemID);
                        break;
                    }
                }
            }

            for (int i = 0; i < data.playerItemInventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerItemInventoryCount[i];
            }

            theInven.LoadItem(itemList);


            //UIManager theGM = FindObjectOfType<UIManager>();
            //theGM.LoadStart();
            MapMaker.Instance.StartMake(data.mapNumber);
        }
        else
        {
            Debug.Log("����� ���̺� ������ �����ϴ�.");
        }

        file.Close();

    }




}