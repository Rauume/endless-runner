using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player2D")
		{
			Debug.Log("Player hit me");
			GameManager.Instance.EndGame();
		}
	}
}
