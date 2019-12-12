using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	protected Animator m_animator;
	protected CharacterController m_characterController;

	public LayerMask floorMask;
	public Vector3 persistentVelocity = Vector3.zero;
	public Vector3 localGravity = Vector3.zero;

	public float jumpHeight = 5f;
	public float forwardVelocity = 1f;
	public float maxFallVelocity;
	public float gravityMultiplier;

	protected RaycastHit groundHit;
	public float groundDetectionLength;

	public bool GravityIsDown = true;

	// Start is called before the first frame update
	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_characterController = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GravityIsDown)
			localGravity = Physics.gravity / 2 * gravityMultiplier;
		else
			localGravity = -Physics.gravity / 2 * gravityMultiplier;

		persistentVelocity += localGravity * Time.deltaTime;
		persistentVelocity.y = Mathf.Clamp(persistentVelocity.y, maxFallVelocity, -maxFallVelocity);


		if (IsGrounded()) //if is on the ground
		{
			m_animator.SetBool("Jumping", false); //set the animation state
			persistentVelocity.y = groundHit.point.y - transform.position.y; //set velocity to the ground.
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Input_Jump();
			Input_SwitchDirections();
		}
	}

	//control the root motion via script (Probs don't need, nothing is animation controlled anymore)
	private void OnAnimatorMove() 
	{
		Vector3 outcomeVelocity = (persistentVelocity);

		//keep the animation from deviating from the running line
		outcomeVelocity.z = 0; 
		//constant fowards velocity
		outcomeVelocity.x = forwardVelocity; 

		//apply the velocity to the character.
		m_characterController.Move(outcomeVelocity * Time.deltaTime); 
	}

	public bool IsGrounded()
	{
		//raycast downwards to detect the edges of the screen.
		Ray ray = new Ray(transform.position, -transform.up);
		bool value = Physics.Raycast(transform.position + 1f * transform.up, -transform.up, out groundHit, groundDetectionLength);

		//if within distance of the ground
		if (Vector3.Distance(groundHit.point, transform.position) < 0.1)
			return true;
		else //else, still in air.
			return false;
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
			transform.eulerAngles += Vector3.forward * 180;
		}
		else
		{
			GravityIsDown = true;
			transform.eulerAngles += Vector3.forward * -180;
		}
	}

}
