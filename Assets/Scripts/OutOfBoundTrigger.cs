using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.loseEvent?.Invoke();
        }
    }
}
