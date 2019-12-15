using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pixelplacement;

public class UIManager : MonoBehaviour
{
	public DisplayObject m_startGamePanel;
	public DisplayObject m_gameplayPanel;
	public DisplayObject m_endGamePanel;

	[Header("UI Elements")]
	public TextMeshProUGUI m_distanceText;
	public TextMeshProUGUI m_coinCountText;

	private void Awake()
	{
		m_startGamePanel.SetActive(true);
		ShowMainMenu();
	}

	private void Update()
	{
		UpdateGameUI();
	}

	public void ShowMainMenu()
	{
		m_startGamePanel.Solo();

	}

	public void ShowGameScreen()
	{
		m_gameplayPanel.Solo();
	}

	public void ShowDeathScreen()
	{
		m_endGamePanel.Solo();
	}

	public void UpdateGameUI()
	{
		if (GameManager.Instance.isGameRunning())
		{
			m_coinCountText.text = GameManager.Instance.GetCoinsCollected().ToString();
			m_distanceText.text = Mathf.RoundToInt(GameManager.Instance.GetCurrentDistance()).ToString() + "m";
		}
	}

}
