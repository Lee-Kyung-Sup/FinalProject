using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject lastobject;
    // Start is called before the first frame update
    void Start()
    {
        sr = lastobject.GetComponent<SpriteRenderer>();

        StartCoroutine("FadeIn");

        StartCoroutine("Fadeout", 3f);

    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Fadeout()
    {   
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(2f);
        }
    }




}
