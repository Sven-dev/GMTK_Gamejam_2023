﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
	private float Windup = 0;
	public Direction Facing = Direction.Right;
	public bool WindingUp = false;

	public bool Active = false;
	public bool Grounded = false;

	[Header("Walking")]
	[SerializeField] private float Speed = 5f;
	[Range(1, 20)]
	[SerializeField] private float Acceleration = 10;
	[Range(1, 20)]
	[SerializeField] private float Deceleration = 10;

	[Range(0, 1)]
	[SerializeField] private float MovementControl = 1f;
	private float MovementInput = 0;

	[Header("Jumping")]
	[SerializeField] private float JumpForce = 1000f;
	[Range(0, 1)]
	[SerializeField] private float JumpCutWhenLetGo = .25f;
	[SerializeField] private AudioSource JumpSound;
	[Space]
	[SerializeField] private LayerMask GroundCheckLayer;
	[SerializeField] private Transform GroundCheckTransform;
	[SerializeField] private Vector2 GroundCheckSize;

	private float CoyoteTime = 0.2f;
	private float LastTimeOnGround = 0f;
	private float JustJumped = 0.2f;

	private Input Input;

	[Space]
	[SerializeField] private Rigidbody2D Rigidbody;
	[SerializeField] private Animator Animator;
	[SerializeField] private SpriteRenderer Renderer;

	[Header("Winding up")]
	[SerializeField] private PlayerClockController ClockController;
	[SerializeField] private Transform CrankPivot;
	[SerializeField] private Animator CrankAnimator;
	[SerializeField] private AudioSource CrankSound;

	private void Start()
	{
		Input = new Input();

		Input.Bigguy.Jump.started += OnJumpInput;
		Input.Bigguy.Jump.canceled += OnJumpUpInput;

		Input.Bigguy.Windup.performed += OnWindupStart;
		Input.Bigguy.Windup.canceled += OnWindupStop;

		Input.Bigguy.Enable();
	}

	private void OnDestroy()
	{
		Input.Bigguy.Disable();

		Input.Bigguy.Jump.started -= OnJumpInput;
		Input.Bigguy.Jump.canceled -= OnJumpUpInput;

		Input.Bigguy.Windup.performed -= OnWindupStart;
		Input.Bigguy.Windup.canceled -= OnWindupStop;

		Input = null;
	}

	private void Update()
	{
		//Getting movement input
		MovementInput = Input.Bigguy.Movement.ReadValue<Vector2>().x;
	}

	private void FixedUpdate()
	{
		GroundCheck();
		Walk();

		if (!WindingUp)
		{
			Windup -= Time.fixedDeltaTime;
			CrankAnimator.SetFloat("Power", Windup);
			ClockController.UpdateClock(Windup);
		}
	}

	private void GroundCheck()
	{
		//Groundcheck
		Collider2D ground = Physics2D.OverlapBox(GroundCheckTransform.position, GroundCheckSize, 0, GroundCheckLayer);

		//Groundcheck (logic)
		if (ground != null)
		{
			Grounded = true;

			//Set the parent to the object it is grounded on so it moves with platforms and stuff
			transform.SetParent(ground.transform);

			//If the object is no longer active, disable the physics
			if (!Active)
			{
				Rigidbody.velocity = Vector2.zero;
				Rigidbody.bodyType = RigidbodyType2D.Kinematic;
			}
		}
		else
		{
			transform.SetParent(null);
			Grounded = false;
		}

		//Groundcheck (physics)
		LastTimeOnGround -= Time.deltaTime;
		JustJumped -= Time.deltaTime;
		if (JustJumped < 0 && ground != null)
		{
			if (LastTimeOnGround < -0.01f)
			{
				Animator.SetTrigger("Land");
				//just landed
			}

			LastTimeOnGround = CoyoteTime;
		}
		else
		{
			//print("not on ground");
		}
	}

	private void Walk()
	{
		float direction = 0;
		if (Windup > 0 && !WindingUp)
		{
			//Calculate the direction
			if (MovementInput > 0)
			{
				Facing = Direction.Right;
				direction = 1;
			}
			else if (MovementInput < 0)
			{
				Facing = Direction.Left;
				direction = -1;
			}
			else
			{
				if (Facing == Direction.Right)
				{
					direction = 1;
				}
				else //if (Facing == Direction.Left)
				{
					direction = -1;
				}
			}
		}

		//Calculate the direction we want to move in and our desired velocity
		float targetSpeed = direction * Speed;

		// We can reduce our control using Lerp(). This smooths changes to our direction and speed
		targetSpeed = Mathf.Lerp(Rigidbody.velocity.x, targetSpeed, MovementControl);

		//Gets an acceleration value based on if we are accelerating (includes turning) 
		//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
		float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Acceleration : Deceleration;

		//Calculate difference between current velocity and desired velocity
		float speedDif = targetSpeed - Rigidbody.velocity.x;

		//Calculate force along x-axis to apply to the player
		float movement = speedDif * accelRate;

		//Convert this to a vector and apply to rigidbody
		Rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}

	private void LateUpdate()
	{
		//Direction (flips the renderer if the player velocity is smaller than 0)
		if (Facing == Direction.Right)
		{
			Renderer.flipX = false;
			CrankPivot.transform.localScale = new Vector3(1, 1, 1);
		}
		else if (Facing == Direction.Left)
		{
			Renderer.flipX = true;
			CrankPivot.transform.localScale = new Vector3(-1, 1, 1);
		}

		/*
		if (WindingUp)
		{
			Animator.SetTrigger("Winding Up");
		}
		else
		{
		*/

			//Animation
			Animator.SetFloat("Vel X", Mathf.Abs(Rigidbody.velocity.x));
			Animator.SetFloat("Vel Y", Rigidbody.velocity.y);

			//Reset triggers
			Animator.ResetTrigger("Jump");
			Animator.ResetTrigger("Land");
		//}
	}

	public void OnJumpInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
	{
		if (Windup > 0 && !WindingUp && LastTimeOnGround >= 0)
		{
			JustJumped = CoyoteTime;

			Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
			Rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Force);
			LastTimeOnGround -= 10;

			//Animation
			Animator.SetTrigger("Jump");

			//Sound
			JumpSound.pitch = Random.Range(0.95f, 1.05f);
			JumpSound.Play();
		}
	}

	public void OnJumpUpInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
	{
		if (Rigidbody.velocity.y >= 0)
		{
			Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y * JumpCutWhenLetGo);
		}
	}

	public void OnWindupStart(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
		WindingUp = true;
		StartCoroutine(_WindUp());
		
    }

	public void OnWindupStop(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
		WindingUp = false;
		CrankAnimator.Play("WindDown");
	}

	private IEnumerator _WindUp()
    {
		float timer = 0.5f;		
		while (WindingUp)
        {		
			if (timer <= 0)
            {
				if (Windup < 8)
				{				
					Windup = Mathf.Clamp(Windup + 2, 0, 8);
					ClockController.UpdateClock(Windup);

					CrankAnimator.Play("WindUp");
					CrankSound.Play();

					print("Wind up! Current wind: " + Windup);
				}
				else
				{
					CrankAnimator.Play("Off");
				}

				timer = 1f;
            }

			timer -= Time.deltaTime;
			yield return null;
		}
    }

	public void ApplyPhysicsForce(Vector2 force)
	{
		Rigidbody.velocity = Vector2.zero;
		Rigidbody.gravityScale = 0;
		MovementControl = 0;

		Rigidbody.AddForce(force, ForceMode2D.Force);

		StartCoroutine(_ApplyPhysicsForce());
		StartCoroutine(_RegainMovementControl());
	}

	private IEnumerator _ApplyPhysicsForce()
	{
		float progress = 0;
		while (progress < 1)
		{
			progress = Mathf.Clamp01(progress + 2 * Time.deltaTime);
			Rigidbody.gravityScale = Mathf.Lerp(0, 3, progress);
			yield return null;
		}

		Rigidbody.gravityScale = 3;
	}

	private IEnumerator _RegainMovementControl()
	{
		yield return new WaitForSeconds(0.2f);

		float progress = 0;
		while (progress < 1)
		{
			progress += 5 * Time.deltaTime;
			MovementControl = Mathf.Lerp(0, 1, progress);

			yield return null;
		}
	}

    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
	{
		if (GroundCheckTransform)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(GroundCheckTransform.position, GroundCheckSize);
		}
	}
	#endregion
}