using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//singleton, since theres only one game manager.
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

	public GameObject m_player;
	Player2D playerController;
	protected EventsManager m_eventManager;


	public float m_globalScrollSpeed = 5.4f;
	public const int m_playAreaHeight = 10;

	[Header("Start and end of spawning positions")]
	public Transform m_startPoint;
	public Transform m_endPoint;

	[Header("Object Pools")]
	public List<ObjectPool> objectPools = new List<ObjectPool>();

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	private void Start()
	{
		m_eventManager = GetComponent<EventsManager>();
		playerController = m_player.GetComponent<Player2D>();

		m_eventManager.AddRandomEventToQueue();
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

	public void EndRun()
	{
		m_globalScrollSpeed = 0;
		playerController.StopRunning();

		Time.timeScale = 0;
	}
}
