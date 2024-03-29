using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionBtn : MonoBehaviour, IPointerEnterHandler
{
    public void StartOption()
    {
        GameManager.instance.ToggleSoundPanel();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("Cursor");
    }
}
