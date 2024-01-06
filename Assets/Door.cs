using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Room Room;
    [SerializeField] private Room SwitchesTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnManager.Instance.Body.transform.parent = null;
        if (SpawnManager.Instance.Head.transform.parent != SpawnManager.Instance.Body.transform)
        {
            SpawnManager.Instance.Head.transform.parent = null;
        }

        Room.Leave();       
        SwitchesTo.Enter();
    }
}