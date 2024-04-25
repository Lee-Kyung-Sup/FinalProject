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
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject optionPanel;
    public GameObject escOptionPanel;

    public SaveNLoad theSaveNLoad; //�����׽�Ʈ

    private PlayerStatus thePlayerStat; //저장을 위한 추가작성 JHP

    private void Awake()
    {
        theSaveNLoad = GetComponent<SaveNLoad>();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escOptionPanel.SetActive(!escOptionPanel.activeSelf);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("Cursor");
    }

    // --------------------------------------------------------------

    public void OpenOptions()
    {
        optionPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
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

        //theSaveNLoad = FindObjectOfType<SaveNLoad>(); //추가작성
        //theSaveNLoad.CallLoad();
    }

    public void ReturnToMainScene()
    {
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.StopBGM();
        StartCoroutine(LoadMainSceneWithFadeIn());
    }

    private IEnumerator LoadMainSceneWithFadeIn()
    {
        escOptionPanel.SetActive(false);
        UIManager.Instance.OnFadeIn();
        yield return new WaitForSeconds(1.0f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("1. IntroScene");

        while (!operation.isDone)
        {
            yield return null;
        }
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ExitOption()
    {
        optionPanel.SetActive(false);
    }

    public void ExitEscOption()
    {
        escOptionPanel.SetActive(false);
    }


    public void LoadStart() //저장을 위한 추가작성 JHP
    {
        StartCoroutine(LoadWaitCoroutine());
    }

    IEnumerator LoadWaitCoroutine()
    {
        //UIManager.Instance.OnFadeIn();
        yield return new WaitForSeconds(1.0f);

        theSaveNLoad.CallLoad(); //재확인
        yield return new WaitForSeconds(0.5f);
        thePlayerStat = FindObjectOfType<PlayerStatus>();

        //UIManager.Instance.OnFadeOut();
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
        Color imageColor = fadeImage.color;

        while ( count < duration)
        {
            count += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1, 0, count/duration);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn(float duration)
    {
        fadeImage.gameObject.SetActive(true);
        float count = 0;
        Color imageColor = fadeImage.color;

        while (count < duration)
        {
            count += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0, 1, count / duration);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }
    }

    // ------------------------------------------------------------------



}
