using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupStartMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject Information;
    [SerializeField] private GameObject SelectCharacter;
    [SerializeField] private Image characterSprite;

    //private CharacterType characterType;

    public void OnclickCharacter()
    {
        Information.SetActive(false);
        SelectCharacter.SetActive(true);
    }

    public void OnclickSelectCharacter(int index)
    {
        //characterType = (CharacterType)index;
        //var character = GameManager.instance.CharacterList.Find(item => item.CharacterType == characterType);
        //characterSprite.sprite = character.CharacterSprite;
        //characterSprite.SetNativeSize();

        //Information.SetActive(true);
        //SelectCharacter.SetActive(false);
    }

    public void OnClickJoin()
    {
        if(!(2 < inputField.text.Length && inputField.text.Length < 10))
        {
            return;
        }

        //GameManager.Instance.SetCharacter(characterType, inputField.text);

        Destroy(gameObject);
    }


}
