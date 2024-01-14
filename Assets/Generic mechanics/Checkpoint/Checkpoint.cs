using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool Active;
    public Transform RespawnPivot;
    [Space]
    [SerializeField] private LayerMask DetectionMask;
    [SerializeField] private Transform DetectionPivot;
    [Space]
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Room Room;

    private void FixedUpdate()
    {
        if (Active)
        {
            return;
        }

        Collider2D[] playerColliders = Physics2D.OverlapBoxAll(DetectionPivot.position, new Vector2(1, 0.1f), 0, DetectionMask);
        if (playerColliders.Length > 0)
        {
            Activate();
            SpawnManager.Instance.UpdateCheckPoints(this);
        }
    }

    public void Activate()
    {
        Active = true;
        Renderer.color = ColorDictionary.Instance.Powered;
    }

    public void Deactivate()
    {
        Active = false;
        Renderer.color = Renderer.color = ColorDictionary.Instance.Unpowered;
    }

    public void Spawn()
    {
        Room.Enter();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(DetectionPivot.position, new Vector2(1, 0.1f));
    }
}
