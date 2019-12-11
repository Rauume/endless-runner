using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	protected Animator m_animator;
	protected CharacterController m_characterController;

	public LayerMask floorMask;
	protected bool jumping;
	public Vector3 appliedVelocity = Vector3.zero;

	public float jumpHeight = 5f;

	protected RaycastHit groundHit;
	public float groundDetectionLength;

	// Start is called before the first frame update
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_characterController = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		appliedVelocity += Physics.gravity / 2 * Time.deltaTime;


		if (Input.GetKeyDown(KeyCode.Space))
		{
			Input_Jump();
		}

		if (IsGrounded()) //if is on the ground
		{
			m_animator.SetBool("Jumping", false); //set the animation state
		}

		m_characterController.Move(appliedVelocity * Time.deltaTime);
	}


	private void OnAnimatorMove()
	{
		Vector3 outcomeVelocity = (appliedVelocity + m_animator.velocity);
		outcomeVelocity.z = 0;


		m_characterController.Move(outcomeVelocity * Time.deltaTime);
	}

	public bool IsGrounded()
	{
		Ray ray = new Ray(transform.position, Vector3.down);
		bool value = Physics.Raycast(transform.position + 0.5f * Vector3.up, Vector3.down, out groundHit, groundDetectionLength);

		//if moving upwards.
		if (appliedVelocity.y > 0)
			return false;

		return value;
	}


	public void Input_Jump()
	{
		if (IsGrounded())
		{
			appliedVelocity.y = Mathf.Sqrt(-2.0f * Physics.gravity.y * jumpHeight);

			m_animator.SetBool("Jumping", true);
			jumping = true;
		}
	}

}
