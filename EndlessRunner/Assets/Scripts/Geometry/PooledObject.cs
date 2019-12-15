using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
	private void Update()
	{
		//when at the end, disable self.
		if (transform.position.x < GameManager.Instance.m_endPoint.position.x)
		{
			gameObject.SetActive(false);
		}
	}


}
