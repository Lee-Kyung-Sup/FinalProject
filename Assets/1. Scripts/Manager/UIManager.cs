using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>, IPointerEnterHandler
{
    [Header("Options")]
    [SerializeField] private Image FadeImage;
    [SerializeField] GameObject soundPanel;

    private SaveNLoad theSaveNLoad; //저장테스트


    private void Start()
    {
        OnFadeOut();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("Cursor");
    }

    // --------------------------------------------------------------

    public void OpenOptions()
    {
        ToggleSoundPanel();
    }

    public void ToggleSoundPanel()
    {
        soundPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void CanelSoundPanel()
    {
        soundPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료 : 빌드 된 게임에서 실제 종료 됨.");
        Application.Quit();
    }

    public void ClickLoad() //불러오기 테스트
    {
        Debug.Log("불러오기");
        StartCoroutine(LoadCoroutine()); //불러오기에 선행되는 대기메서드
    }

    IEnumerator LoadCoroutine()  //불러오기에 선행되는 대기메서드
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScenePJH");

        while(!operation.isDone)
        {
            yield return null;
        }
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theSaveNLoad.LoadData();
        Destroy(gameObject);
    }


    // -------------------------Fade In & Out--------------------------------
    public void OnFadeOut()
    {
        StartCoroutine(FadeOut(1.0f));
    }
    public void OnFadeIn()
    {
        StartCoroutine(FadeIn(1.0f));
    }

    private IEnumerator FadeOut(float duration)
    {
        float count = 0;
        Color imageColor = FadeImage.color;

        while ( count < duration)
        {
            count += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1, 0, count/duration);
            FadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }
        FadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn(float duration)
    {
        FadeImage.gameObject.SetActive(true);
        float count = 0;
        Color imageColor = FadeImage.color;

        while (count < duration)
        {
            count += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0, 1, count / duration);
            FadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }
    }

    // ------------------------------------------------------------------



}
