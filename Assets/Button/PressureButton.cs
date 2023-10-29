using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [Range(1, 10)] public int Height = 2;
    [SerializeField] private float PressSpeed = 1;
    [SerializeField] private float UnpressSpeed = 1;
    [Space]
    [SerializeField] private UnityFloatEvent OnButtonUpdate;
    
    private float PressValue = 0;

    [Header("Detection")]
    [SerializeField] private LayerMask DetectionLayer;
    [SerializeField] private Transform DetectionPivot;
    
    [Header("Physics")]
    [SerializeField] private Transform Platform;
    [SerializeField] private Transform UnpressedPivot;
    [SerializeField] private Transform PressedPivot;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer Background;

    private void Start()
    {
        OnButtonUpdate?.Invoke(PressValue);
    }

    private void Update()
    {
        Background.size = new Vector2(Background.size.x, Mathf.Lerp(Height, 0, PressValue));
    }

    private void FixedUpdate()
    {
        float currentPressValue = PressValue;

        //Check if there's any players above the button
        Collider2D[] playerColliders = Physics2D.OverlapBoxAll(DetectionPivot.position, Vector2.one * 2, 0, DetectionLayer);
        if (playerColliders.Length > 0)
        {
            //Check how many players are standing on top of the button
            float pressMultiplier = 0;
            foreach (Collider2D col in playerColliders)
            {
                //The charactercontrollers need to be rewritten to only have one so this can be a bit easier.
                CharacterController player1 = col.GetComponent<CharacterController>();
                if (player1 != null && player1.Grounded)
                {
                    pressMultiplier++;
                }

                /*
                Character2Controller player2 = col.GetComponent<Character2Controller>();
                if (player2 != null)
                {
                    pressMultiplier++;
                    continue;
                }
                */
            }

            //Increase PressValue based on the amount of characters pressing onto it until it reaches 1
            currentPressValue = Mathf.Clamp01(currentPressValue + PressSpeed * pressMultiplier * Time.fixedDeltaTime);
        }
        else if (Platform.position != UnpressedPivot.position)
        {
            //Decrease PressValue until it is back at 0
            currentPressValue = Mathf.Clamp01(currentPressValue - UnpressSpeed * Time.fixedDeltaTime);
        }

        //If currentPressValue is not the same as PressValue, the position and state of the button needs to be updated
        if (currentPressValue != PressValue)
        {
            Platform.position = Vector2.Lerp(UnpressedPivot.position, PressedPivot.position, currentPressValue);
            OnButtonUpdate?.Invoke(currentPressValue);

            PressValue = currentPressValue;          
        }     
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(DetectionPivot.position, new Vector3(2, 2, 0));
    }
}