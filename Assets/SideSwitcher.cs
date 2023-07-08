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

    public void OnGameOver()
    {

    }

    public void OnGameWin()
    {
        StopCoroutine(_Timer());
    }
}

public enum Sides
{
    Left,
    Right
}