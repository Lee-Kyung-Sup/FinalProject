using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;


[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;

    public List<int> itemNumber = new List<int>();
    public List<string> displayName = new List<string>();
    public List<string> description = new List<string>();
}
//[System.Serializable]
//public class ItemLoad
//{
//    public int itemnumber, quantity;

//    public ItemLoad(int ITEMNUMBER, int QUANTITY)
//    {   
//        itemnumber = ITEMNUMBER;
//        quantity = QUANTITY;
//    }
//}



    public class SaveNLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();
    
    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private PlayerController thePlayer;
    private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if(!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theInventory = FindObjectOfType<Inventory>(); //인벤토리 찾아옴

        //---------------------------------------------------------------
        saveData.playerPos = thePlayer.transform.position; //위치값저장


        //---------------------------------------------------------------


        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindObjectOfType<PlayerController>();
            //theInventory = FindObjectOfType<Inventory>(); //인벤토리 찾기

            thePlayer.transform.position = saveData.playerPos;

            for(int i=0; i<saveData.displayName.Count; i++)
            {
                //theInventory.LoadToInventory(saveData.itemNumber[i], saveData.displayName[i], saveData.description[i]);
            }


            Debug.Log("로드 완료");
        }
        else
            Debug.Log("세이브 파일이 없습니다");
    }


}
