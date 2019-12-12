using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{

	private void OnBecameInvisible()
	{
		gameObject.SetActive(false);
	}

	
}
