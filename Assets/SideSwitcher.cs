using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SideSwitcher : MonoBehaviour
{
    public static SideSwitcher Instance;

    [SerializeField] private int SwitchTime = 1;
    [SerializeField] private List<CharacterController> Characters;
    [Space]
    [SerializeField] private UnitySideEvent OnRoleSwitch;
    [Space]
    [SerializeField] private UnityFloatEvent OnTimerChange;
    [Space]
    [SerializeField] private UnityEvent OnVictory;

    private Input Input;
    private Sides Side = Sides.Left;

    //private IEnumerator TimerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Input = new Input();
        Input.Switching.Switch.started += SwitchRoles;
        Input.Switching.Enable();
    }

    private void OnDestroy()
    {
        Input.Switching.Disable();
    }

    private void SwitchRoles(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StartCoroutine(_SwitchRoles());
    }

    private IEnumerator _SwitchRoles()
    {
        if (Side == Sides.Left)
        {
            Side = Sides.Right;
            OnRoleSwitch?.Invoke(Side);

            Characters[0].SideSwitch(Side);
            yield return new WaitForSeconds(1f);
            Characters[1].SideSwitch(Side);
        }
        else //if (PlayerTurn == Sides.Right)
        {
            Side = Sides.Left;
            OnRoleSwitch?.Invoke(Side);

            Characters[1].SideSwitch(Side);
            yield return new WaitForSeconds(1f);
            Characters[0].SideSwitch(Side);
        }  
    }

    public void OnGameWin()
    {
        OnVictory?.Invoke();
        print("You win");
    }
}

public enum Sides
{
    Left,
    Right
}