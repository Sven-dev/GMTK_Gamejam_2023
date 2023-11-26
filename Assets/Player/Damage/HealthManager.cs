using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnManager.Instance.DestroyPlayer();
        Destroy(gameObject);
    }
}