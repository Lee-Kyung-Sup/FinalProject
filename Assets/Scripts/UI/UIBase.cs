using System;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private void OnEnable()
    {
        OpenUI();
    }

    private void OnDisable()
    {
        CloseUI();
    }

    public virtual void OpenUI()
    {
    }
        
    public virtual void CloseUI()
    {
    }
}