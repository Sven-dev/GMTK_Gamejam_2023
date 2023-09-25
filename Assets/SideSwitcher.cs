using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SideSwitcher : MonoBehaviour
{
    public static SideSwitcher Instance;

    [SerializeField] private Character1Controller Character1;
    [SerializeField] private Character2Controller Character2;
    [SerializeField] private CameraManager CameraManager;
    [Space]
    [SerializeField] private UnityBoolEvent OnCharacterSwitch;
    [Space]
    [SerializeField] private UnityFloatEvent OnTimerChange;
    [Space]
    [SerializeField] private UnityEvent OnVictory;

    private Input Input;
    private bool Player1 = true;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Input = new Input();
        Input.Switch.Switch.started += SwitchRoles;
        Input.Switch.Enable();
    }

    private void OnDestroy()
    {
        Input.Switch.Disable();
    }

    public void SwitchRoles(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StartCoroutine(_SwitchRoles());
    }

    private IEnumerator _SwitchRoles()
    {
        Player1 = !Player1;
        OnCharacterSwitch?.Invoke(Player1);

        if (Player1)
        {
            CameraManager.SetTarget(Character1.transform);

            Character2.DisableCharacter();
            yield return new WaitForSeconds(1f);
            Character1.EnableCharacter();
        }
        else //if (!player1)
        {
            CameraManager.SetTarget(Character2.transform);

            Character1.DisableCharacter();
            yield return new WaitForSeconds(1f);
            Character2.EnableCharacter();
        }  
    }

    public void OnGameWin()
    {
        OnVictory?.Invoke();
    }
}