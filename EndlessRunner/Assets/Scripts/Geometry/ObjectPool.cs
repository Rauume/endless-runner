using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public List<GameObject> m_pooledObjects;
	public GameObject m_objectToPool;
	public int m_amountToPool;

	public bool m_shouldExpand = true;

	private void Awake()
	{

		m_pooledObjects = new List<GameObject>();
		for (int i = 0; i < m_amountToPool; i++)
		{
			GameObject obj = (GameObject)Instantiate(m_objectToPool, this.transform);
			obj.SetActive(false);
			m_pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < m_pooledObjects.Count; i++)
		{
			if (!m_pooledObjects[i].activeInHierarchy)
			{
				return m_pooledObjects[i];
			}
		}

		if (m_shouldExpand)
		{
			GameObject obj = (GameObject)Instantiate(m_objectToPool);
			obj.SetActive(false);
			m_pooledObjects.Add(obj);
			return obj;
		}
		else
		{
			return null;
		}

	}



}
