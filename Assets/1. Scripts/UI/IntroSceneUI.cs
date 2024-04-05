using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IntroSceneUI : MonoBehaviour, IPointerEnterHandler
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("Cursor");
    }

    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM("FirstChapter");

        SceneManager.LoadScene("2. GameScene");
    }
    //2. GameScene


    public void IntroOptions()
    {
        UIManager.Instance.OpenOptions();
    }


}
