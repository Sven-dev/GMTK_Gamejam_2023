using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        float distance = Vector3.Distance(cop.position, character.position);
        if (distance<1)
        {
            print("end");
            //Application.Quit();

            SceneManager.LoadScene(1);

        }
    }
}
