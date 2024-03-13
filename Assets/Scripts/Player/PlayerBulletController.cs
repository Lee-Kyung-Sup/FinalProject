using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyTime", 2.0f);
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("���� �浹!");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("���Ϳ��� �浹!");
            // collision.SendMessage("Demaged", 10); // Demaged �Լ� ȣ��, ���Ÿ� ���ݷ�(10, �ӽ�)��ŭ ����  TODO
            Destroy(gameObject);
        }
    }
}
