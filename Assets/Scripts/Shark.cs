using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    private bool isCatched;
    private const string PLAYER_TAG = "Player";
    private Transform playerTransform;
    bool isGameOver;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCatched)
            CatchToPlayer();
    }

    private void CatchToPlayer()
    {
        Vector3 targetPoint = playerTransform.position;
        Quaternion lookQuaternion = Quaternion.LookRotation(targetPoint);
        float rotateSpeed = 5f;
        float catchSpeed =7f;
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookQuaternion, Time.deltaTime * rotateSpeed);
        transform.LookAt(targetPoint);
        transform.position = Vector3.Lerp(transform.position,targetPoint, Time.deltaTime*catchSpeed);
        if (Vector3.Distance(transform.position, targetPoint) < 0.25f)
        {
            if(!isGameOver)
            {
                GameManager.Instance.loseEvent?.Invoke();
                isGameOver = true;
            }
            
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(PLAYER_TAG))
            isCatched = true;
    }
}
