using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Range(1, 20f)]
    [SerializeField] private float MovementSmoothing = 0.5f;
    [SerializeField] private List<CharacterController> Characters;

    private CharacterController ActiveCharacter;

    // Start is called before the first frame update
    private void Start()
    {
        ActiveCharacter = Characters[0];
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        FollowCharacter();
    }

    private void FollowCharacter()
    {
        Vector3 characterPosition = ActiveCharacter.transform.position;
        characterPosition.z = transform.position.z;

        if (transform.position != characterPosition)
        {
            transform.position += (characterPosition - transform.position) / MovementSmoothing;
        }
    }

    public void SwitchActiveCharacter(Sides side)
    {
        if (side == Sides.Left)
        {
            ActiveCharacter = Characters[0];
        }
        else //if (side == Sides.Right)
        {
            ActiveCharacter = Characters[1];
        }
    }
}
