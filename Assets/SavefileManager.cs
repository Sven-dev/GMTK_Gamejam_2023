using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavefileManager : MonoBehaviour
{
    public static SavefileManager Instance;

    public int Checkpoint { get; private set; }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void UpdateCheckpoint(int value)
    {
        Checkpoint = value;
        Save();
    }

    private void Load()
    {
        Checkpoint = PlayerPrefs.GetInt("Checkpoint", 0);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Checkpoint", Checkpoint);
    }
}