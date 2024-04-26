using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name,typeof(T));
                    instance = go.GetComponent<T>();
                }
            }
            return instance;
        }
    }
}
