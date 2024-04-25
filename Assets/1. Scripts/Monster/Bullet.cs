using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public bool isRotate;
  
    void Update()
    {
        if(isRotate)
        {
            transform.Rotate(Vector3.forward * 10);
        }    

      
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            if (gameObject.layer == 20)
            {
                gameObject.layer = 12;
            }
            gameObject.SetActive(false);
        }
    }
}
