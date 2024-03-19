using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSaveMenu : MonoBehaviour
{
    [SerializeField] private GameObject SaveInformation;

    public void OnclickSavefile()
    {
        SaveInformation.SetActive(true);



    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveInformation.SetActive(false);
        }
    }




}
