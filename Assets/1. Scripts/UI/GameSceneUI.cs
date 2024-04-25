using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    public Button EscButton;


    void Start()
    {
        MapMaker.Instance.StartMake();
        UIManager.Instance.OnFadeOut();
        EscButton.onClick.AddListener(ToggleEscPanel);
    }

    void ToggleEscPanel()
    {
        bool isActive = UIManager.Instance.escOptionPanel.activeSelf;
        UIManager.Instance.escOptionPanel.SetActive(!isActive);
    }

}
