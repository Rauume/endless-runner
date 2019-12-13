using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptedEvent : ScriptableObject
{
	protected EventsManager eventManager;
	protected List<GameObject> objects;
	protected bool eventComplete = false;

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

		eventComplete = false;
	}

	public virtual void Terminate()
	{
		eventComplete = true;
		OnStop();
	}

	public abstract void OnStart();

	public abstract void Update();

	public abstract void OnStop();

	public bool isComplete()
	{
		return eventComplete;
	}
}
