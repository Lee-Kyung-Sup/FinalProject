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
            Debug.Log("���̺�üũ����Ʈ");
        }
        else
        {
            Debug.Log("���̺����");
        }
    }


}
