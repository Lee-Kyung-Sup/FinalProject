using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayMove : MonoBehaviour
{
    public static bool canMove = false;
    [SerializeField] float speed;
    Collision2D pColl;
    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.position += -transform.right * speed * Time.fixedDeltaTime;
            if (transform.position.x < -6f)
            {
                GoBack();
                transform.position = new Vector3(35.82f, transform.position.y,transform.position.z);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IsGroundable>(out IsGroundable ound) && ound.IsGround())
        {
            collision.transform.SetParent(null);
            pColl = collision;
            pColl.transform.SetParent(transform);
        }
    }
    void GoBack()
    {
        pColl?.transform.SetParent(null);
    }
    public void EndMove()
    {
        canMove = false;
        pColl?.transform.SetParent(null);
    }
}
