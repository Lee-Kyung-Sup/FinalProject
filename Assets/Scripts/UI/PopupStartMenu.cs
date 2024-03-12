using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupStartMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private GameObject Information;
    [SerializeField] private GameObject SelectCharacter;

    public void OnclickCharacter()
    {
        Information.SetActive(false);
        SelectCharacter.SetActive(true);
    }

    public void OnClickJoin()
    {
        if(!(2 < inputField.text.Length && inputField.text.Length < 10))
        {
            return;
        }

        playerName.text = inputField.text;

        Destroy(gameObject);

    }


}
