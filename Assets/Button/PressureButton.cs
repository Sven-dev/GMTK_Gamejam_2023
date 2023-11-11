using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [Range(1, 10)] public int Height = 2;
    [Range(1, 10)] public int PlatformSize;
    [SerializeField] private float PressSpeed = 1;
    [SerializeField] private float UnpressSpeed = 1;
    [Space]
    [SerializeField] private List<Powerable> PoweredObjects;
    
    private float PowerValue = 0;

    [Header("Detection")]
    [SerializeField] private LayerMask DetectionLayer;
    [SerializeField] private Transform DetectionPivot;
    
    [Header("Physics")]
    [SerializeField] private Transform Platform;
    [SerializeField] private Transform UnpressedPivot;
    [SerializeField] private Transform PressedPivot;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer Background;

    private void Update()
    {
        Background.size = new Vector2(Background.size.x, Mathf.Lerp(Height, 0, PowerValue));
    }

    private void FixedUpdate()
    {
        float currentPressValue = PowerValue;

        //Check if there's any players above the button
        Collider2D[] playerColliders = Physics2D.OverlapBoxAll(DetectionPivot.position, new Vector2(PlatformSize, 2f), 0, DetectionLayer);
        if (playerColliders.Length > 0)
        {
            //Check how many players are standing on top of the button
            float pressMultiplier = 0;
            foreach (Collider2D col in playerColliders)
            {
                CharacterController character = col.GetComponent<CharacterController>();
                if (character.Grounded)
                {
                    pressMultiplier++;
                }
            }

            //Increase PressValue based on the amount of characters pressing onto it until it reaches 1
            currentPressValue = Mathf.Clamp01(currentPressValue + PressSpeed * pressMultiplier * Time.fixedDeltaTime);
        }
        else if (Platform.position != UnpressedPivot.position)
        {
            //Decrease PressValue until it is back at 0
            currentPressValue = Mathf.Clamp01(currentPressValue - UnpressSpeed * Time.fixedDeltaTime);
            
            //For some reason Clamp01 sometimes leaves the number at a very small value instead of 0, annoying.
            if (currentPressValue < 0.001f)
            {
                currentPressValue = 0;
            }
        }

        //If currentPressValue is not the same as PressValue, the position and state of the button needs to be updated
        if (currentPressValue != PowerValue)
        {
            Platform.position = Vector2.Lerp(UnpressedPivot.position, PressedPivot.position, currentPressValue);
            foreach (Powerable obj in PoweredObjects)
            {
                obj.UpdatePower(currentPressValue);
            }

            PowerValue = currentPressValue;          
        }     
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(DetectionPivot.position, new Vector2(PlatformSize, 2f));
    }
}

public abstract class Powerable: MonoBehaviour
{
    public abstract void UpdatePower(float power);
}