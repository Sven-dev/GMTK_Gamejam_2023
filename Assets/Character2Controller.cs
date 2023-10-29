using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Controller : MonoBehaviour
{
	public bool MovementAllowed = true;
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
	[Space]
	[SerializeField] private LayerMask GroundCheckLayer;
	[SerializeField] private Transform GroundCheckTransform;
	[SerializeField] private Vector2 GroundCheckSize;

	private float CoyoteTime = 0.2f;
	private float LastTimeOnGround = 0f;
	private float JustJumped = 0.2f;

	private float BaseGravity;
	private Vector2 FrozenVelocity = Vector2.zero;

	private Input Input;

	[Space]
	[SerializeField] private Rigidbody2D Rigidbody;
	[SerializeField] private Animator Animator;
	[SerializeField] private SpriteRenderer Renderer;

	private void Start()
	{
		Input = new Input();
		Input.Player2.Jump.started += OnJumpInput;
		Input.Player2.Jump.canceled += OnJumpUpInput;

		BaseGravity = Rigidbody.gravityScale;

		FrozenVelocity = Rigidbody.velocity;
		Rigidbody.velocity = Vector2.zero;
		Rigidbody.gravityScale = 0;
		Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	private void OnDestroy()
	{
		Input.Player2.Disable();
		Input.Player2.Jump.started -= OnJumpInput;
		Input.Player2.Jump.canceled -= OnJumpUpInput;
		
		Input = null;
	}

	private void Update()
	{
		LastTimeOnGround -= Time.deltaTime;
		JustJumped -= Time.deltaTime;

		//Getting movement input
		MovementInput = Input.Player2.Movement.ReadValue<Vector2>().x;

		//Groundcheck
		if (JustJumped < 0 && Physics2D.OverlapBox(GroundCheckTransform.position, GroundCheckSize, 0, GroundCheckLayer))
		{
			if (LastTimeOnGround < -0.01f)
			{
				Animator.SetTrigger("Land");
				//just landed
			}

			LastTimeOnGround = CoyoteTime;
			Grounded = true;
		}
		else
        {
			Grounded = false;
			print("not on ground");
        }
	}

	private void FixedUpdate()
	{
		Walk();
	}

	private void Walk()
	{
		//Calculate the direction we want to move in and our desired velocity
		float targetSpeed = MovementInput * Speed;

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
		if (MovementInput > 0 || Rigidbody.velocity.x > 1)
		{
			Renderer.flipX = false;
		}
		else if (MovementInput < 0 || Rigidbody.velocity.x < -1)
		{
			Renderer.flipX = true;
		}

		//Animation
		Animator.SetFloat("Vel X", Mathf.Abs(Rigidbody.velocity.x));
		Animator.SetFloat("Vel Y", Rigidbody.velocity.y);

		//Reset triggers
		Animator.ResetTrigger("Jump");
		Animator.ResetTrigger("Land");
	}

	public void OnJumpInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
	{
		if (LastTimeOnGround >= 0)
		{
			print("jump");
			JustJumped = CoyoteTime;

			Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
			Rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Force);
			LastTimeOnGround -= 10;

			//Animation
			Animator.SetTrigger("Jump");

			//Sound
			AudioManager.Instance.RandomizePitch("Jump", 0.9f, 1.1f);
			AudioManager.Instance.Play("Jump");
		}
	}

	public void OnJumpUpInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
	{
		if (Rigidbody.velocity.y >= 0)
		{
			Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y * JumpCutWhenLetGo);
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
			Rigidbody.gravityScale = Mathf.Lerp(0, BaseGravity, progress);
			yield return null;
		}

		Rigidbody.gravityScale = BaseGravity;
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

	public void EnableCharacter()
    {
		Rigidbody.velocity = FrozenVelocity;
		Rigidbody.gravityScale = BaseGravity;
		Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

		Renderer.color = new Color(0.66f, 0.66f, 0.66f, 1f);

		Input.Player2.Enable();
	}

	public void DisableCharacter()
    {
		FrozenVelocity = Rigidbody.velocity;
		Rigidbody.velocity = Vector2.zero;
		Rigidbody.gravityScale = 0;
		Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

		Renderer.color = new Color(0.33f, 0.33f, 0.33f, 1f);

		Input.Player2.Disable();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
			SideSwitcher.Instance.OnGameWin();
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