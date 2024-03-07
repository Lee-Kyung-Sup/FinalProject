using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletoneBase<UIManager>
{
    private List<UIBase> popups = new List<UIBase>();

    public UIBase ShowUI(string uiName)
    {
        var obj = Resources.Load("UI/" + uiName, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning("Failed to ShowPopup({0})");
            return null;
        }
        return ShowPopupWithPrefab(obj, uiName);
    }

    public T ShowUI<T>() where T : UIBase
    {
        return ShowUI(typeof(T).Name) as T;
    }

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName)
    {
        var obj = Instantiate(prefab);
        return ShowUI(obj, popupName);
    }

    public UIBase ShowUI(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        popups.Insert(0, popup);

        obj.SetActive(true);
        return popup;
    }



    public T CreateUI<T>(Transform parent = null) where T : UIBase
    {
        var obj = Resources.Load<T>("UI/" + typeof(T).Name);
        if (!obj)
        {
            Debug.LogWarning("Failed to ShowPopup({0})");
            return null;
        }

        return Instantiate(obj, parent);
    }
}