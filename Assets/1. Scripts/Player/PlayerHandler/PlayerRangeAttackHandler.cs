using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject RangeHitEffect; // ��Ʈ ȿ�� ������
    [SerializeField] private bool isChargeShot = false; // ������ �������� true

    PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyTime", 7.0f);
        playerStatus = GetComponent<PlayerStatus>();
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            if (!isChargeShot) // �Ϲ� ���� ��쿡�� ���̳� �÷����� �ε����� �� �����
            {
                Instantiate(RangeHitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }

        else if (collision.gameObject.CompareTag("Monster"))
        {
            Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // ��Ʈ ȿ�� ����
            // collision.SendMessage("Demaged", 1); // Demaged �Լ� ȣ��, ���Ÿ� ���ݷ�(1, �ӽ�)��ŭ ����  TODO
            
            if (!isChargeShot) 
            {
                Destroy(gameObject);
            }
        }
    }
}
