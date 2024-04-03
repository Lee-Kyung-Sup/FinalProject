using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;




public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.001f;
    private float lastCheckTime;
    public float maxCheckDistance;
    [SerializeField] private LayerMask layerMask;

    private GameObject curInteractGameobject;


    public TextMeshProUGUI promptText;
    private Camera _camera;
    [SerializeField] private LayerMask pickitem;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    //void FixedUpdate()
    //{
    //    Checkitem();
    //}


    private void Checkitem()
    {
        Debug.DrawRay(transform.position, new Vector3(3, -2, 0), new Color(1, 1, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(3,-2,0));
        if (hit.collider.Equals("Interactable"))
        {
            promptText.gameObject.SetActive(true);
            promptText.text = string.Format("<b>[!]</b> {0}");

        }

    }
}