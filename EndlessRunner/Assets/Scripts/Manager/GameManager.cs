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
	[HideInInspector] public Player2D m_playerController;

	protected bool m_isGameRunning = false;
	protected int m_coinsCollected = 0;

	public float m_globalScrollSpeed = 5.4f;
	public const float m_beginningScrollSpeed = 5.4f;
	public float m_scrollSpeedIncreaseModifier = 0.2f;
	protected float m_distanceRan = 0;
	public const int m_playAreaHeight = 10;

	[Header("Start and end spawning positions")]
	public Transform m_startPoint;
	public Transform m_endPoint;

	//Protected Variables;
	protected PlaceGameElements m_gameElementSpawner;



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
		m_playerController = m_player.GetComponent<Player2D>();
		m_gameElementSpawner = GetComponent<PlaceGameElements>();
	}

	private void Update()
	{
		if (isGameRunning())
		{
			m_distanceRan += m_globalScrollSpeed * Time.deltaTime;
			m_globalScrollSpeed += Time.deltaTime * m_scrollSpeedIncreaseModifier;
			Time.timeScale = m_globalScrollSpeed - m_beginningScrollSpeed + 1;
		}
	}

	//Start the run
	public void StartGame()
	{
		Time.timeScale = 1;
		m_globalScrollSpeed = m_beginningScrollSpeed;

		m_isGameRunning = true;
		m_playerController.BeginRunning();
		m_gameElementSpawner.BeginSpawningElements();
	}

	//Stop the run
	public void EndGame()
	{
		m_globalScrollSpeed = 0;
		m_playerController.KillPlayer();
		m_isGameRunning = false;

		m_gameElementSpawner.StopSpawningElements();

		m_UIManager.ShowDeathScreen();

		Time.timeScale = 0;
	}

	//reset without starting the run.
	public void ResetGame()
	{
		m_gameElementSpawner.ReturnGameElementsToPool();
		m_coinsCollected = 0;
		m_distanceRan = 0;

	}

	public void ToggleGravityDirection()
	{
		Vector3 newStartPointPosition = m_startPoint.position;

		if (IsGravityDown()) 
		{ //If switching to upside down
			newStartPointPosition += Vector3.up * m_playAreaHeight;

			m_startPoint.eulerAngles += Vector3.right * 180;
		}
		else
		{ //if switching back to normal.
			newStartPointPosition.y = 0;


			m_startPoint.eulerAngles += Vector3.right * -180;

		}

		m_playerController.Input_SwitchDirections();


		//This is fixing a logic error causing the start position being placed above the play area.
		newStartPointPosition.y = Mathf.Clamp(newStartPointPosition.y, 0, GameManager.m_playAreaHeight);
		m_startPoint.position = newStartPointPosition;
	}
	
	public bool IsGravityDown()
	{
		return m_playerController.m_gravityDown;
	}

	//Simple getters and setters.
	public bool isGameRunning()
	{
		return m_isGameRunning;
	}

	//this is a float and not an int due to a really weird bug
	//It wouldn't increment during run, but worked fine during debugging w/ visual studio
	public float GetCurrentDistance()
	{
		return m_distanceRan;
	}

	//Coin counter.
	public void AddCoin()
	{
		m_coinsCollected++;
	}

	public int GetCoinsCollected()
	{
		return m_coinsCollected;
	}

}
