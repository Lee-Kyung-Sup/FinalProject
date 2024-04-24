using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IntroSceneUI : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject loadGamePanel;
    [SerializeField] GameObject CharacterMenu;

    void Start()
    {
        UIManager.Instance.OnFadeOut();
        AudioManager.Instance.PlayBGM("Intro");
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("Cursor");
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


    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM("FirstChapter");

        StartCoroutine(LoadSceneWithFadeIn());

        //SceneManager.LoadScene("GameScenePJH");
        //SceneManager.LoadScene("2. GameScene");
    }

    private IEnumerator LoadSceneWithFadeIn()
    {
        UIManager.Instance.OnFadeIn(); 
        yield return new WaitForSeconds(1.0f); 

        AsyncOperation operation = SceneManager.LoadSceneAsync("2. GameScene");

        while (!operation.isDone)
        {
            yield return null;
        }
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
