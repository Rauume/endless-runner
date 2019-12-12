using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
	public GameObject player;
	protected GameManager gameManager;

    //Events
    [Header("Scripted Event Collection")]
	protected List<ScriptedEventCollection> scriptedEventCollectionQueue;

	private void Awake()
	{
		scriptedEventCollectionQueue = new List<ScriptedEventCollection>();
		gameManager = GetComponent<GameManager>();
	}

	private void Update()
	{
        //run through scripted events queue 
        if (scriptedEventCollectionQueue.Count > 0)
		{
			//start Events
			if (!scriptedEventCollectionQueue[0].isRunning)
			{
				foreach (ScriptedEvent scriptedEvent in scriptedEventCollectionQueue[0].events)
				{
					scriptedEvent.OnStart();
				}
				scriptedEventCollectionQueue[0].isRunning = true;
			}
			 
			//Update Events			
			foreach (ScriptedEvent scriptedEvent in scriptedEventCollectionQueue[0].events)
			{
				scriptedEvent.Update();
			}
			
			//update safe time before finished.
			scriptedEventCollectionQueue[0].timeToComplete -= Time.deltaTime;
			
			//if complete, remove self from queue
			if (scriptedEventCollectionQueue[0].timeToComplete < 0)
			{
				scriptedEventCollectionQueue.RemoveAt(0);
			}
		}
	}

	public bool isFinished()
	{
		if (scriptedEventCollectionQueue.Count < 1)
		{
			return true;
		}

		return false;
	}

	//add a collection of scripted events onto the queue
	public void AddScriptedEventCollection(ScriptedEventCollection collection)
	{
		List<ScriptedEvent> newEvents = new List<ScriptedEvent>();
		foreach (ScriptedEvent scriptedEvent in collection.events)
		{
			//create a copy of the scriptableObject
			ScriptedEvent newEvent = Instantiate(scriptedEvent);
			newEvent.Initialise(this, collection.objects);
			newEvents.Add(newEvent);
		}
		ScriptedEventCollection newTimedCondition = new ScriptedEventCollection(newEvents, collection.objects, collection.timeToComplete, collection.isRunning);

		scriptedEventCollectionQueue.Add(newTimedCondition);
	}
}

[System.Serializable]
public class ScriptedEventCollection
{
	public string comment;
	public List<ScriptedEvent> events = new List<ScriptedEvent>();
	public List<GameObject> objects = new List<GameObject>();
	public float timeToComplete = 1;

	public bool isRunning = false;

	public ScriptedEventCollection(List<ScriptedEvent> newEvents, List<GameObject> objects, float newtimeToComplete, bool newIsRunning)
	{
		events = newEvents;
		this.objects = objects;
		timeToComplete = newtimeToComplete;
		isRunning = newIsRunning;
	}
}