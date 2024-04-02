using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : SingletonBase<UIManager>, IPointerEnterHandler
{
    [Header ("Scene UI Canvas")]
    [SerializeField] private GameObject canvas_IntroScene;
    [SerializeField] private GameObject canvas_GameScene;
    [SerializeField] private GameObject canvas_LastScene;

    [Header("Options")]
    [SerializeField] GameObject soundPanel;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("�ε� ��: " + scene.name);
        switch (scene.name)
        {
            case "1. IntroScene":
                Debug.Log("��Ʈ�� ĵ������ Ȱ��ȭ");
                ActivateCanvas(canvas_IntroScene);
                break;
            case "2. GameScene":
                Debug.Log("���� ĵ���� Ȱ��ȭ");
                ActivateCanvas(canvas_GameScene);
                break;
            case "3. LastScene":
                Debug.Log("��Ʈ ĵ���� Ȱ��ȭ");
                ActivateCanvas(canvas_LastScene);
                break;
            default:
                Debug.Log("�ش� ���� ��Ī�Ǵ� ĵ������ �����ϴ�: " + scene.name);
                break;
        }
    }

    private void ActivateCanvas(GameObject canvasActivate)
    {
        canvas_IntroScene.SetActive(canvasActivate == canvas_IntroScene);
        canvas_GameScene.SetActive(canvasActivate == canvas_GameScene);
        canvas_LastScene.SetActive(canvasActivate == canvas_LastScene);
    }
    // ------------------------------------------------------------------


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

    public void GameOptions()
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


}
