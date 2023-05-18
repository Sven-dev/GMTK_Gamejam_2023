using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDowntheRabbit : MonoBehaviour
{
    public Transform cop;
    public Transform character;
    public float copspeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction= Vector3.MoveTowards(cop.position, character.position, copspeed*Time.deltaTime);
        cop.position = direction;
    }
}
