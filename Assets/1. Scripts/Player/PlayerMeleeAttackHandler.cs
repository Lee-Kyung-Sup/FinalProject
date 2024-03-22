using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject meleeHitEffect; // ��Ʈ ȿ�� ������


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹�� �༮��? : " + collision.gameObject.name);

            if (collision.CompareTag("Monster"))
            {
                Debug.Log("���Ϳ��� ���� ����!");
                Instantiate(meleeHitEffect, collision.transform.position, Quaternion.identity);

                //collision.SendMessage("TakeDamage", 1);  // ���Ϳ��� ������
            }
      
    }
}
