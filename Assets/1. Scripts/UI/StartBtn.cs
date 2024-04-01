using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour, IPointerEnterHandler
{

    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM("FirstChapter");
        SceneManager.LoadScene("2. GameScenes");
    }

    //"2. GameScenes"
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("Cursor");
    }
}
