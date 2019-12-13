using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
	protected Animator m_animator;
	protected Rigidbody2D rigidbody;
	public Transform groundDetection;

	public LayerMask floorMask;
	public Vector3 persistentVelocity = Vector3.zero;
	public Vector3 localGravity = Vector3.zero;

	public float jumpHeight = 5f;
	public float maxFallVelocity;
	public float gravityMultiplier;

	protected RaycastHit2D groundHit;
	public float groundDetectionLength;

	public bool GravityIsDown = true;

	// Start is called before the first frame update
	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (IsGrounded()) //if is on the ground
		{
			m_animator.SetBool("Jumping", false); //set the animation state
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Input_Jump();
			Input_SwitchDirections();
		}
	}

	public bool IsGrounded()
	{
		//raycast downwards to detect the edges of the screen.		
		groundHit = Physics2D.Raycast((Vector2)transform.position, -(Vector2)transform.up, 5f, floorMask);

		//if within distance of the ground
		return (Vector2.Distance(groundHit.point, (Vector2)groundDetection.position) < 0.1);
	}


	public void Input_Jump()
	{
		if (IsGrounded())
		{
			//Jump!
			m_animator.SetBool("Jumping", true);
		}
	}

	//change the gravity depending on the previous direction
	public void Input_SwitchDirections()
	{
		if (GravityIsDown)
		{
			GravityIsDown = false;
			transform.eulerAngles += Vector3.right * 180;
			rigidbody.gravityScale *= -1;
		}
		else
		{
			GravityIsDown = true;
			transform.eulerAngles += Vector3.right * -180;
			rigidbody.gravityScale *= -1;

		}
	}
}
