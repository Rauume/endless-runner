using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float objectScrollSpeed = 5.4f;


	public GameObject player;
	public EventsManager eventManager;

	public ObjectPool backgroundObjectPool;


	private void Start()
	{
		if (!eventManager)
			eventManager = GetComponent<EventsManager>();

		//queueNewEvent(scriptedEventCollections[0]);
		eventManager.AddRandomEventToQueue();
	}

	//protected void queueNewEvent(ScriptedEventCollection collection)
	//{
	//	//eventManager.AddScriptedEventCollection(collection);
	//}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Debug.Log(eventManager.getFromEventPool(typeof(LaserTopBottom)).name);
		}
	}
}
