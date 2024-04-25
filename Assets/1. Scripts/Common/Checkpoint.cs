using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SaveNLoad theSaveNLoad;


    private void Start()
    {
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            theSaveNLoad.CallSave();
            Debug.Log("���̺�üũ����Ʈ");
        }
        else
        {
            //Debug.Log("���̺����");
        }
    }


}
