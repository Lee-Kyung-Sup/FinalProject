using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEffect : MonoBehaviour
{
    SpriteRenderer sp;
    [Header("HitEffect")]
    [SerializeField] Material hitMat;
    Material origin;
    WaitForSeconds waitTime;
    private void Start()
    {
        sp = GetComponentInChildren<SpriteRenderer>();
        origin = sp.material;
        waitTime = new WaitForSeconds(0.2f);
    }
    public IEnumerator HitFlash()
    {
        sp.material = hitMat;
        yield return waitTime;
        sp.material = origin;
    }
}
