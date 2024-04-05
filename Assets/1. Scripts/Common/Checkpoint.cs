using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SaveNLoad theSaveNLoad;


    private void Start()
    {
        theSaveNLoad = UIManager.Instance.theSaveNLoad;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            theSaveNLoad.SaveData();
            Debug.Log("세이브체크포인트");
        }
        else
        {
            Debug.Log("세이브실패");
        }
    }


}
