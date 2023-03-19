using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMover : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject twoHandModel;
    [SerializeField] GameObject defaultHandModel;
    MeshDrawer meshDrawer;
    public Action OnHoldAction;
    private void Awake()
    {
        meshDrawer = FindObjectOfType<MeshDrawer>();
    }

    

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.position,Time.deltaTime*5);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
           meshDrawer.isDrawing = false;
            other.gameObject.SetActive(false);
            defaultHandModel.SetActive(false);
            twoHandModel.SetActive(true);
            OnHoldAction?.Invoke();
        }
    }
}
