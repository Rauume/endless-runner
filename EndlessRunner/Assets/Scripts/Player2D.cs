using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
	protected Animator m_animator;
	protected Rigidbody2D m_rigidbody;
	public Transform m_groundDetection;

	public LayerMask m_floorMask;

	[Header("Jump Variables")]
	public float m_jumpHeight = 4f;
	public float m_minJumpHeight = 1f;
	protected bool jumpKeyHeld = false;


	[Tooltip("Time the player can still jump after starting to fall")]
	public float m_coyoteTime = 0.2f;
	public float m_timeInAir = 0f;

	public bool m_gravityIsDown = true;
	public bool m_playerRunning = true;



	// Start is called before the first frame update
	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();

		//todo: only set this on actual game start.
		m_playerRunning = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (IsGrounded()) //if is on the ground
		{
			m_animator.SetBool("Jumping", false); //set the animation state
			m_timeInAir = 0;
		}
		else
		{ //update the time in air
			m_timeInAir += Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jumpKeyHeld = true;
			Input_Jump();
			//Input_SwitchDirections();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			jumpKeyHeld = false;
		}
	}

	private void FixedUpdate()
	{
		//if currently jumping, and jump is not held, apply force onto the player
		//(Results in variable jump, the longer its held, the higher the player can jump)
		if (m_animator.GetBool("Jumping"))
		{
			if (!jumpKeyHeld && Vector2.Dot(m_rigidbody.velocity, Vector2.up) > 0)
			{
				m_rigidbody.AddForce((Physics2D.gravity * m_rigidbody.gravityScale) * m_rigidbody.mass);
			}
		}
	}

	public bool IsGrounded()
	{
		if (m_rigidbody.velocity.y <= 0)
		{
			//raycast downwards to detect the edges of the screen.		
			RaycastHit2D groundHit = Physics2D.Raycast((Vector2)transform.position, -(Vector2)transform.up, 5f, m_floorMask);

			if (groundHit)
			{
				//if within distance of the ground
				return (Vector2.Distance(groundHit.point, (Vector2)m_groundDetection.position) < 0.1);
			}
			else //todo: cleanup.
				return false;
		}
		else
			return false;
	}

	public void Input_Jump()
	{
		if (IsGrounded() || m_timeInAir < m_coyoteTime && !m_animator.GetBool("Jumping"))
		{
			m_animator.SetBool("Jumping", true);
			//add a pulse of force for the jump.
			m_rigidbody.AddForce(Vector2.up * CalculateJumpForce(-Physics2D.gravity.y * m_rigidbody.gravityScale, m_jumpHeight) * m_rigidbody.mass, ForceMode2D.Impulse);
		}
	}

	//calculate force from gravity and height.
	public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
	{
		//h = v^2/2g
		//2gh = v^2
		//sqrt(2gh) = v
		return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
	}

	//change the gravity depending on the previous direction
	public void Input_SwitchDirections()
	{
		if (m_playerRunning)
		{
			if (m_gravityIsDown)
			{
				m_gravityIsDown = false;
				transform.eulerAngles += Vector3.right * 180;
				m_rigidbody.gravityScale *= -1;
			}
			else
			{
				m_gravityIsDown = true;
				transform.eulerAngles += Vector3.right * -180;
				m_rigidbody.gravityScale *= -1;

			}
		}
	}

	//stop the player from moving.
	public void StopRunning()
	{
		if (!m_gravityIsDown)
			Input_SwitchDirections();

		m_playerRunning = false;
		m_animator.SetBool("IsDead", true);		
	}
}
