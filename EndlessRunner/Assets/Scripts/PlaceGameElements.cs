using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGameElements : MonoBehaviour
{
	//public List<GameplayPrefab> gamePrefabs = new List<GameplayPrefab>();
	public GameObject[] objects;

	[Header("Object Pools")]
	public List<ObjectPool> objectPools = new List<ObjectPool>();

	public void BeginSpawningElements()
	{
		InvokeRepeating("SpawnRandomGameSegment", 0, 5);
	}

	public void SpawnRandomGameSegment()
	{
		ProbabilityUtils.Picker picker = new ProbabilityUtils.Picker(0, 5, objects);


		GameObject objectToSpawn = picker.GetNext() as GameObject;

		foreach (Transform t in objectToSpawn.transform)
		{
			GameObject laser = GetPool(t.tag).GetPooledObject();
			laser.transform.position = t.position + GameManager.Instance.m_startPoint.position;
			laser.SetActive(true);
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


