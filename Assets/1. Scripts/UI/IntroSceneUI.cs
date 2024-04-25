using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IntroSceneUI : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject CharacterMenu;

    void Start()
    {
        UIManager.Instance.OnFadeOut();
        AudioManager.Instance.PlayBGM("Intro");
    }

    void Update()
    {

    }

    public void StartOption()
    {
        startPanel.SetActive(true);
    }

    public void ExitStartOption()
    {
        startPanel.SetActive(false);
    }

    public void LoadOption()
    {
        startPanel.SetActive(true);
    }

    public void ExitLoadOption()
    {
        startPanel.SetActive(false);
    }


    public void PopUpCharacterMenu()
    {
        CharacterMenu.SetActive(true);
    }


    public void IntroOptions()
    {
        UIManager.Instance.OpenOptions();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
