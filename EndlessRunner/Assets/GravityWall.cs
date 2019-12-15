using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWall : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			//GameManager.Instance.playerController.Input_SwitchDirections();
			GameManager.Instance.ToggleGravityDirection();
		}
	}

	private void Update()
	{
		transform.Translate((Vector3.right * -GameManager.Instance.m_globalScrollSpeed) * Time.deltaTime);
	}

}
