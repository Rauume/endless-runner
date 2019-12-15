using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGameElements : MonoBehaviour
{
	public GameObject[] objects;

	

	[Header("Object Pools")]
	public List<ObjectPool> objectPools = new List<ObjectPool>();

	public void BeginSpawningElements()
	{
		InvokeRepeating("SpawnRandomGameSegment", 0,6);
	}

	public void SpawnRandomGameSegment()
	{
		ProbabilityUtils.Picker picker = new ProbabilityUtils.Picker(0, 5, objects);


		GameObject objectToSpawn = picker.GetNext() as GameObject;

		foreach (Transform t in objectToSpawn.transform)
		{
			GameObject placedObject = GetPool(t.tag).GetPooledObject();
			placedObject.transform.position = t.position + (Vector3.right * GameManager.Instance.m_startPoint.position.x);
			placedObject.transform.localScale = Vector3.one; //return scale to normal before placing.

			//if gravity is down
			//todo: this can be neater, shouldn't need an if statement for gravity direction.
			if (!GameManager.Instance.IsGravityDown())
			{
				//Invert its rotation.
				placedObject.transform.localEulerAngles += Vector3.right * GameManager.Instance.m_startPoint.localEulerAngles.x;

				Vector3 newPosition = placedObject.transform.position;
				newPosition.y *= -1; //Invert the y position
				newPosition.y += GameManager.Instance.m_startPoint.position.y; //Add the start position.

				placedObject.transform.localScale += Vector3.up * -2;

				placedObject.transform.position = newPosition;
			}

			placedObject.SetActive(true);
		}
	}

	public void StopSpawningElements()
	{
		CancelInvoke();
	}

	public void ReturnGameElementsToPool()
	{
		foreach(ObjectPool pool in objectPools)
		{
			foreach (GameObject obj in pool.pooledObjects)
			{
				obj.SetActive(false);
			}
		}
	}

	public ObjectPool GetPool(string tag)
	{
		foreach (ObjectPool pool in objectPools)
		{
			if (pool.objectToPool.tag == tag)
				return pool;
		}

		return null;
	}



}


