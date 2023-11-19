using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MoveBlockEditor : MonoBehaviour
{
    [SerializeField] private MoveBlock MoveBlock;

    private Vector3 Position;

    private void Update()
    {
        /*
        if (MoveBlock.transform.position != Position)
        {
            MoveBlock.transform.position = new Vector3(
                    (int)MoveBlock.transform.position.x,
                    (int)MoveBlock.transform.position.y,
                    (int)MoveBlock.transform.position.z);

            Position = MoveBlock.transform.position;
        }
        */
    }
}
