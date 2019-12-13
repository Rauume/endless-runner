﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public static ObjectPool pool;

	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	public bool shouldExpand = true;

	private void Awake()
	{
		pool = this;

		pooledObjects = new List<GameObject>();
		for (int i = 0; i < amountToPool; i++)
		{
			GameObject obj = (GameObject)Instantiate(objectToPool, this.transform);
			obj.SetActive(false);
			obj.GetComponent<PooledObject>();
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}

		if (shouldExpand)
		{
			GameObject obj = (GameObject)Instantiate(objectToPool);
			obj.SetActive(false);
			pooledObjects.Add(obj);
			return obj;
		}
		else
		{
			return null;
		}

	}



}
