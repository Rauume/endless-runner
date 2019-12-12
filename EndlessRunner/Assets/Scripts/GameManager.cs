using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float objectScrollSpeed = 5.4f;


	public GameObject player;
	public EventsManager eventManager;

	public ObjectPool backgroundObjectPool;

	public List<ScriptedEventCollection> scriptedEventCollections = new List<ScriptedEventCollection>();

	private void Start()
	{
		if (!eventManager)
			eventManager = GetComponent<EventsManager>();

		queueNewEvent(scriptedEventCollections[0]);
	}

	protected void queueNewEvent(ScriptedEventCollection collection)
	{
		eventManager.AddScriptedEventCollection(collection);
	}

	private void Update()
	{
		
	}
}
