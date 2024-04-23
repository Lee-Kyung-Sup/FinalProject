using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }
}
