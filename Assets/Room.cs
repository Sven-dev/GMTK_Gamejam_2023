using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public bool Powered = false;

    [SerializeField] private Camera Camera;
    [SerializeField] private CameraBounds Bounds;
    [Space]
    [SerializeField] private List<Powerable> Powerables;
    [SerializeField] private List<PowerButton> Buttons;
    [SerializeField] private List<PowerPlate> Plates;
    [Space]
    [SerializeField] private SpriteRenderer Background;
    [SerializeField] private List<Tilemap> Tilemaps;

    // Start is called before the first frame update
    private void Awake()
    {
        //Get all mechanics in the room and add them to lists to control them
        Powerables = transform.GetComponentsInChildren<Powerable>(true).ToList();
        Buttons = transform.GetComponentsInChildren<PowerButton>(true).ToList();
        Plates = transform.GetComponentsInChildren<PowerPlate>(true).ToList();
    }

    /// <summary>
    /// Enter a room through a checkpoint (respawning/fasttravelling)
    /// </summary>
    public void Enter()
    {
        SetupRoom();
    }

    /// <summary>
    /// Enter a room through a door
    /// </summary>
    public void Enter(Door door)
    {
        SetupRoom();
        Camera.transform.position = Bounds.RestrictCamera(door.transform.position - Vector3.forward * 10);
    }

    public void Leave()
    {
        Reset();
        gameObject.SetActive(false);
    }

    public void DeathLeave()
    {
        Reset();
        TurnOff();
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        foreach (PowerPlate plate in Plates)
        {
            plate.Reset();
        }
    }

    public void TurnOn()
    {
        Powered = true;

        //Make room green
        Background.color = ColorDictionary.Instance.BackgroundPowered;
        foreach (Tilemap tilemap in Tilemaps)
        {
            tilemap.color = ColorDictionary.Instance.Powered;
        }

        //Autopower all powerables
        foreach (Powerable powerable in Powerables)
        {
            powerable.AutoPower = true;
        }

        //Enable/Disable all buttons in the room based on their Powertype
        foreach(PowerButton button in Buttons)
        {
            if (button.AutoPower == true)
            {
                button.Press();
            }
            else
            {
                button.Depress();
            }
        }
    }

    public void TurnOff()
    {
        Powered = false;

        //Make room pink
        Background.color = ColorDictionary.Instance.Background;
        foreach (Tilemap tilemap in Tilemaps)
        {
            tilemap.color = ColorDictionary.Instance.Unpowered;          
        }

        //Unpower all powerables
        foreach (Powerable powerable in Powerables)
        {
            powerable.AutoPower = false;
        }

        //Enable/Disable all buttons in the room based on their Powertype
        foreach (PowerButton button in Buttons)
        {
            if (button.AutoPower == false)
            {
                button.Press();
            }
            else
            {
                button.Depress();
            }
        }
    }

    private void SetupRoom()
    {
        gameObject.SetActive(true);
        foreach (PowerButton button in Buttons)
        {
            button.SetState(Powered);
        }
    }
}
