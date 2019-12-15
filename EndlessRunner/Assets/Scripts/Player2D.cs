using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
	[Tooltip("The layer for ground detection")]
	public LayerMask m_floorMask;

	[Header("Jump Variables")]
	public float m_jumpHeight = 4f;

	[Tooltip("Time the player can still jump after starting to fall")]
	public float m_coyoteTime = 0.2f;
	protected float m_timeInAir = 0f;

	[HideInInspector] public bool m_gravityDown = true;
	[HideInInspector] public bool m_playerAlive = true;

	//protected variables;
	protected bool m_jumpKeyHeld = false;
	protected Animator m_animator;
	protected Rigidbody2D m_rigidbody;
	protected Vector3 m_initialPlayerPosition;
	protected CapsuleCollider2D m_playerCollider;



	// Start is called before the first frame update
	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_playerCollider = GetComponent<CapsuleCollider2D>();

		//todo: only set this on actual game start.
		m_playerAlive = true;
		m_initialPlayerPosition = transform.position;
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
			m_jumpKeyHeld = true;
			Input_Jump();
			//Input_SwitchDirections();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			m_jumpKeyHeld = false;
		}

		//touch controls.
		if(Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				m_jumpKeyHeld = true;
				Input_Jump();
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				m_jumpKeyHeld = false;

			}
		}
	}

	private void FixedUpdate()
	{
		//if currently jumping, and jump is not held, apply force onto the player
		//(Results in variable jump, the longer its held, the higher the player can jump)
		if (m_animator.GetBool("Jumping"))
		{
			if (!m_jumpKeyHeld && Vector2.Dot(m_rigidbody.velocity, (Vector2)transform.up) > 0)
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
				return (Vector2.Distance(groundHit.point, new Vector2(0, transform.position.y - Mathf.Sign(m_rigidbody.gravityScale) * (m_playerCollider.size.y / 2))) < 0.24);
			}
		}


		return false;
	}

	public void Input_Jump()
	{
		if (IsGrounded() || m_timeInAir < m_coyoteTime && !m_animator.GetBool("Jumping"))
		{
			m_animator.SetBool("Jumping", true);
			//add a pulse of force for the jump.
			m_rigidbody.AddForce((Vector2)transform.up * CalculateJumpForce(-Physics2D.gravity.y * m_rigidbody.gravityScale, m_jumpHeight * m_rigidbody.gravityScale) * m_rigidbody.mass, ForceMode2D.Impulse);
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
		if (m_playerAlive)
		{
			if (m_gravityDown)
			{
				m_gravityDown = false;
				transform.eulerAngles += Vector3.right * 180;
				m_rigidbody.gravityScale *= -1;
			}
			else
			{
				m_gravityDown = true;
				transform.eulerAngles += Vector3.right * -180;
				m_rigidbody.gravityScale *= -1;

			}
		}
	}

	//stop the player from moving.
	public void KillPlayer()
	{
		if (m_playerAlive)
		{
			if (!m_gravityDown)
				Input_SwitchDirections();

			m_playerAlive = false;
			m_animator.SetBool("IsDead", true);
		}
	}

	public void BeginRunning()
	{
		m_playerAlive = true;
		m_animator.Play("Running");
		m_animator.SetBool("IsDead", false);
		transform.position = m_initialPlayerPosition;
	}
}
