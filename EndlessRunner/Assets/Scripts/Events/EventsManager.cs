using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventsManager : MonoBehaviour
{
	public GameObject player;
	[HideInInspector]
	public GameManager gameManager;

	//All events
	public List<ScriptedEventCollection> scriptedEventCollections = new List<ScriptedEventCollection>();

	protected List<ScriptedEvent> ScriptedEventsPool = new List<ScriptedEvent>();

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
				//tell it to terminate before removing.
				scriptedEventCollectionQueue[0].Terminate();
				scriptedEventCollectionQueue.RemoveAt(0);
			}
		}
	}

	
	public ScriptedEvent getFromEventPool(System.Type type)
	{
		return ScriptedEventsPool.FirstOrDefault(obj => obj.GetType() == type && obj.isComplete());
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
			//get a copy of the object, or make one.
			ScriptedEvent newEvent = getFromEventPool(scriptedEvent.GetType());
			if (!newEvent)
				newEvent = Instantiate(scriptedEvent);

			newEvent.Initialise(this, collection.objects);
			newEvents.Add(newEvent);
			ScriptedEventsPool.Add(newEvent);
		}
		ScriptedEventCollection eventCollection = new ScriptedEventCollection(newEvents, collection.objects, collection.timeToComplete, collection.isRunning);

		scriptedEventCollectionQueue.Add(eventCollection);
	}

	public void AddRandomEventToQueue()
	{
		//todo: roulette selection
		AddScriptedEventCollection(scriptedEventCollections[0]);
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

	public void Terminate()
	{
		foreach(ScriptedEvent scriptedEvent in events)
		{
			scriptedEvent.Terminate();
		}
	}
}