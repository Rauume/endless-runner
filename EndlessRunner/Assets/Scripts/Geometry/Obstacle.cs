using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPoolObject
{
	public void returnToPool()
	{
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player2D")
		{
			GameManager.Instance.EndGame();
		}
	}
}
