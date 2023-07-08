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


    private Sides Side = Sides.Left;

    private IEnumerator TimerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        TimerCoroutine = _Timer();
        StartCoroutine(TimerCoroutine);
    }

    private IEnumerator _Timer()
    {       
        while (true)
        {
            float timer = SwitchTime;
            while (timer > 0)
            {
                timer -= 1;

                float timePassed = timer / SwitchTime;
                OnTimerChange?.Invoke(timePassed);

                yield return new WaitForSeconds(1);

            }
          
            if (Side == Sides.Left)
            {
                Side = Sides.Right;
            }
            else // if (PlayerTurn == Sides.Right)
            {
                Side = Sides.Left;
            }

            foreach (CharacterController character in Characters)
            {
                character.SideSwitch(Side);
            }

            yield return new WaitForSeconds(1f);

            print("Switching sides!");
            SwitchRoles(Side);

            //Wait for side transition?
            yield return new WaitForSeconds(3f);
        }
    }

    private void SwitchRoles(Sides side)
    {
        OnRoleSwitch?.Invoke(side);
    }

    public void OnGameWin()
    {
        StopCoroutine(TimerCoroutine);
        OnVictory?.Invoke();
        print("You win");
    }
}

public enum Sides
{
    Left,
    Right
}