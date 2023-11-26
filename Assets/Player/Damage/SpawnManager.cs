using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private Transform RespawnLocation;
    [Space]
    [SerializeField] private CameraManager Camera;
    [Space]
    [SerializeField] private BodyController PlayerPrefab;

    private BodyController Body;
    private HeadController Head;

    private void Awake()
    {
        Instance = this;

        //Load latest respawn position from save file     

    }

    private void Start()
    {
        SpawnPlayer();
    }

    public void DestroyPlayer()
    {
        if (Body != null)
        {
            Destroy(Body.gameObject);
        }

        if (Head != null)
        {
            Destroy(Head.gameObject);
        }

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        //Spawn the player prefab at the loaded checkpoint
        //Keep reference to both controllers so we can destroy them when one gets damaged
        Body = Instantiate(PlayerPrefab, RespawnLocation.position, Quaternion.identity);
        Head = Body.GetComponentInChildren<HeadController>();

        foreach(Transform child in Body.transform)
        {
            print(child.name);
            Head = child.GetComponent<HeadController>();
            if (Head != null)
            {
                break;
            }
        }

        Camera.SetTarget(Head.transform);
    }

    public void UpdateCheckPoint(Transform location)
    {
        RespawnLocation = location;

        //Save updated respawn position to save file
    }
}