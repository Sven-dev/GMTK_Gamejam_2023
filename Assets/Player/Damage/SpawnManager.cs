using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [HideInInspector] public BodyController Body;
    [HideInInspector] public HeadController Head;

    [SerializeField] private List<Checkpoint> Checkpoints;
    private int ActiveCheckpoint = 0;

    [SerializeField] private BodyController PlayerPrefab;

    private Room[] Rooms;

    private void Awake()
    {
        Instance = this;

        //Temp code, replace this with a hardcoded list of checkpoint before release.
        //Checkpoints = new List<Checkpoint>();
        //Checkpoints.AddRange(FindObjectsOfType<Checkpoint>());

        //Load latest respawn position from save file
        ActiveCheckpoint = SavefileManager.Instance.Checkpoint;
    }

    private void Start()
    {
        Rooms = transform.GetComponentsInChildren<Room>(true);
        foreach(Room room in Rooms)
        {
            room.Leave();
        }

        SpawnPlayer();
    }

    public void Death()
    {
        if (Body != null)
        {
            Destroy(Body.gameObject);
        }

        if (Head != null)
        {
            Destroy(Head.gameObject);
        }

        foreach (Room room in Rooms)
        {
            room.Reset();
            room.Leave();
        }

        SpawnPlayer();        
    }

    public void SpawnPlayer()
    {
        //Spawn the player prefab at the loaded checkpoint
        //Keep reference to both controllers so we can destroy them when one gets damaged
        Checkpoint checkpoint = Checkpoints[ActiveCheckpoint];
        Body = Instantiate(PlayerPrefab, checkpoint.RespawnPivot.position, Quaternion.identity);
        
        checkpoint.Spawn();
    }

    public void UpdateCheckPoints(Checkpoint activeCheckpoint)
    {
        for (int i = 0; i < Checkpoints.Count; i++)
        {
            if (Checkpoints[i] == activeCheckpoint)
            {
                ActiveCheckpoint = i;
                SavefileManager.Instance.UpdateCheckpoint(i);
            }
            else
            {
                Checkpoints[i].Deactivate();
            }
        }
    }
}