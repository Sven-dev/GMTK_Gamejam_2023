using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SideSwitcher : MonoBehaviour
{
    public static SideSwitcher Instance;

    [SerializeField] private Character ActiveCharacter = Character.Combined;
    [Space]
    [SerializeField] private CharacterController CombinedCharacter;
    [SerializeField] private CharacterController TopCharacter;
    [SerializeField] private CharacterController BottomCharacter;
    [Space]
    [SerializeField] private CameraManager CameraManager;

    private Input Input;

    private void Start()
    {
        Instance = this;

        Input = new Input();
        Input.Switch.Switch.started += SwitchRoles;
        Input.Switch.Enable();

        //temp
        ActiveCharacter = Character.SplitTop;
        BottomCharacter.DisableCharacter();
        TopCharacter.EnableCharacter();
        CameraManager.SetTarget(TopCharacter.transform);
    }

    private void OnDestroy()
    {
        Input.Switch.Disable();
    }

    /// <summary>
    /// Switches between top and bottom halves, when the character is split into halves
    /// </summary>
    public void SwitchRoles(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (ActiveCharacter == Character.SplitTop)
        {
            TopCharacter.DisableCharacter();
            BottomCharacter.EnableCharacter();

            CameraManager.SetTarget(BottomCharacter.transform);

            ActiveCharacter = Character.SplitBottom;        
        }
        else if (ActiveCharacter == Character.SplitBottom)
        {
            BottomCharacter.DisableCharacter();
            TopCharacter.EnableCharacter();

            CameraManager.SetTarget(TopCharacter.transform);

            ActiveCharacter = Character.SplitTop;
        }
    }
}

public enum Character
{
    Combined,
    SplitTop,
    SplitBottom
}