using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupStartMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject Information;
    //[SerializeField] private GameObject SelectCharacter;
    [SerializeField] private Image characterSprite;

    public void OnclickCharacter()
    {
        Information.SetActive(false);
        //SelectCharacter.SetActive(true);
    }

    public void OnClickJoin()
    {
        if(!(1 < inputField.text.Length && inputField.text.Length < 20))
        {
            return;
        }

        Destroy(gameObject);
    }


}
