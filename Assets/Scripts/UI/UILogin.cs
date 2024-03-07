using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : UIBase<UILogin>
{
    [SerializeField] private TMP_InputField _inputId;
    [SerializeField] private TMP_InputField _inputPS;
    [SerializeField] private Button _btnConfirm;
    [SerializeField] private Button _btnSignUp;
    
    // Start is called before the first frame update
    void Start()
    {
        _btnConfirm.onClick.AddListener(Login);
        _btnSignUp.onClick.AddListener(SignUp);
    }

    void Login()
    {
        string id = _inputId.text;
        string ps = _inputPS.text;

        UserManager.Instance.Login(id, ps, OpenMain);
    }

    void SignUp()
    {
        UIManager.Instance.ShowUI<UISignUp>();
        CloseUI();
    }

    void OpenMain()
    {
        UIManager.Instance.ShowUI<UIMain>();
        CloseUI();
    }
}
