using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyTime", 2.0f);
    }

    // Update is called once per frame
    void DestroyTime()
    {
        Destroy(gameObject);
    }
}
