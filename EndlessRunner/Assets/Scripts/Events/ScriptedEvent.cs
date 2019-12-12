using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptedEvent : ScriptableObject
{

	protected EventsManager eventManager;
	protected List<GameObject> objects;

	public virtual void Initialise(EventsManager manager, List<GameObject> newPositions)
	{
		eventManager = manager;
		objects = new List<GameObject>();

		if (newPositions.Count > 0)
		{
			foreach (GameObject go in newPositions)
			{
				objects.Add(go);
			}
		}
		else
		{
			objects.Add(manager.gameObject);
		}
	}

	public abstract void OnStart();

	public abstract void Update();
}
