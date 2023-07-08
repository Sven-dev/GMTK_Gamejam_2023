using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SideSwitcher : MonoBehaviour
{
    public static SideSwitcher Instance;

    [SerializeField] private int SwitchTime = 1;
    [Space]
    [SerializeField] private UnitySideEvent OnRoleSwitch;
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
                timer -= Time.deltaTime;
                yield return null;
            }
          
            if (Side == Sides.Left)
            {
                Side = Sides.Right;
            }
            else // if (PlayerTurn == Sides.Right)
            {
                Side = Sides.Left;
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