using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UISignUp : UIBase<UISignUp>
{
    [SerializeField] private TMP_InputField _inputId;
    [SerializeField] private TMP_InputField _inputPS;
    [SerializeField] private TMP_InputField _inputNickname;
    [SerializeField] private Button _btnConfirm;
    [SerializeField] private Button _btnBack;
    
    // Start is called before the first frame update
    void Start()
    {
        _btnConfirm.onClick.AddListener(Login);
        _btnBack.onClick.AddListener(Back);
    }

    void Login()
    {
        string id = _inputId.text;
        string ps = _inputPS.text;
        string nickname = _inputNickname.text;

        UserManager.Instance.SignUp(id, ps, nickname);
    }

    void Back()
    {
        CloseUI();
        UIManager.Instance.ShowUI<UILogin>();
    }
}
