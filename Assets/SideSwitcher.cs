using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSwitcher : MonoBehaviour
{
    [SerializeField] private int SwitchTime = 1;
    [Space]
    [SerializeField] private UnitySideEvent OnRoleSwitch;

    private Sides Side = Sides.Left;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Timer());
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

            print("Switching sides!");
            SwitchRoles(Side);

            //Wait for side transition?
            yield return null;
        }
    }

    private void SwitchRoles(Sides side)
    {
        OnRoleSwitch?.Invoke(side);
    }
}

public enum Sides
{
    Left,
    Right
}