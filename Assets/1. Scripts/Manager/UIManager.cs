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
    [SerializeField] GameObject OptionPanel;

    public SaveNLoad theSaveNLoad; //�����׽�Ʈ

    private void Awake()
    {
        theSaveNLoad = GetComponent<SaveNLoad>();
    }



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
        OptionPanel.SetActive(true);
        //AudioManager.Instance.PlaySFX("Click");
    }

    public void CanelSoundPanel()
    {
        OptionPanel.SetActive(false);
        //AudioManager.Instance.PlaySFX("Click");
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
        OptionPanel.SetActive(false);
    }

    public void ClickLoad() //�ҷ����� �׽�Ʈ
    {
        Debug.Log("�ҷ�����");
        StartCoroutine(LoadCoroutine()); //�ҷ����⿡ ����Ǵ� ���޼���
    }

    IEnumerator LoadCoroutine()  //�ҷ����⿡ ����Ǵ� ���޼���
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("2. GameScene");

        while(!operation.isDone)
        {
            yield return null;
        }
        //theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theSaveNLoad.LoadData();
        //Destroy(gameObject);
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
