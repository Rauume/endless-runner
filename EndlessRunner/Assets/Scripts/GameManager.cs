using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
	//singleton, since theres only one game manager.
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

	public UIManager m_UIManager;

	[Header("Game Variables")]
	public GameObject m_player;
	[HideInInspector] public Player2D playerController;

	//protected EventsManager m_eventManager;
	protected bool m_isGameRunning = false;
	protected int coinsCollected = 0;

	public float m_globalScrollSpeed = 5.4f;
	public const float m_beginningScrollSpeed = 5.4f;
	protected float m_distanceRan = 0;
	public const int m_playAreaHeight = 10;

	[Header("Start and end spawning positions")]
	public Transform m_startPoint;
	public Transform m_endPoint;

	//Protected Variables;
	protected PlaceGameElements gameElementSpawner;



	private void Awake()
	{
		Time.timeScale = 0;

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
		playerController = m_player.GetComponent<Player2D>();
		gameElementSpawner = GetComponent<PlaceGameElements>();
	}

	private void Update()
	{
		if (isGameRunning())
		{
			m_distanceRan += m_globalScrollSpeed * Time.deltaTime;
		}
	}

	//Start the run
	public void StartGame()
	{
		Time.timeScale = 1;
		m_globalScrollSpeed = m_beginningScrollSpeed;

		m_isGameRunning = true;
		playerController.BeginRunning();
		gameElementSpawner.BeginSpawningElements();
	}

	//Stop the run
	public void EndGame()
	{
		m_globalScrollSpeed = 0;
		playerController.KillPlayer();
		m_isGameRunning = false;

		gameElementSpawner.StopSpawningElements();

		m_UIManager.ShowDeathScreen();

		Time.timeScale = 0;
	}

	//reset without starting the run.
	public void ResetGame()
	{
		gameElementSpawner.ReturnGameElementsToPool();
		coinsCollected = 0;
		m_distanceRan = 0;

	}

	public void ToggleGravityDirection()
	{
		playerController.Input_SwitchDirections();

		if (IsGravityDown()) 
		{ //If switching to upside down
			m_startPoint.position += Vector3.up * -m_playAreaHeight;
			m_startPoint.eulerAngles += Vector3.right * 180;
		}
		else
		{ //if switching back to normal.
			m_startPoint.position += Vector3.up * m_playAreaHeight;
			m_startPoint.eulerAngles += Vector3.right * -180;

		}
	}
	
	public bool IsGravityDown()
	{
		return playerController.m_gravityDown;
	}

	//Simple getters and setters.
	public bool isGameRunning()
	{
		return m_isGameRunning;
	}

	public float GetCurrentDistance()
	{
		return m_distanceRan;
	}

	//Coin counter.
	public void AddCoin()
	{
		coinsCollected++;
	}

	public int GetCoinsCollected()
	{
		return coinsCollected;
	}

}
