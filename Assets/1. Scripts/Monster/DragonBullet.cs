using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBullet : MonoBehaviour
{
    public int dmg;
    public bool isAttack;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    { 
         anim.SetTrigger("DragonProjectile"); 
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }
}
