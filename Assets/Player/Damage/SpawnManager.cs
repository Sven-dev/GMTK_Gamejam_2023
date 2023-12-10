using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private List<Checkpoint> Checkpoints;
    private int ActiveCheckpoint = 0;

    [SerializeField] private BodyController PlayerPrefab;
    [SerializeField] private CameraManager Camera;

    private BodyController Body;
    private HeadController Head;

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

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        //Spawn the player prefab at the loaded checkpoint
        //Keep reference to both controllers so we can destroy them when one gets damaged
        Body = Instantiate(PlayerPrefab, Checkpoints[ActiveCheckpoint].RespawnPivot.position, Quaternion.identity);
        
        //For some reason I can't get the headcontroller out of body easily.
        //This could be improved but works for now.
        //Head = Body.GetComponentInChildren<HeadController>();
        foreach(Transform child in Body.transform)
        {
            Head = child.GetComponent<HeadController>();
            if (Head != null)
            {
                break;
            }
        }

        Camera.SetTarget(Head.transform);
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