using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pixelplacement;

public class UIManager : MonoBehaviour
{
	public DisplayObject StartGamePanel;
	public DisplayObject GameplayPanel;
	public DisplayObject EndGamePanel;

	[Header("UI Elements")]
	public TextMeshProUGUI m_distanceText;
	public TextMeshProUGUI m_coinCountText;

	private void Awake()
	{
		StartGamePanel.SetActive(true);
		ShowMainMenu();
	}

	private void FixedUpdate()
	{
		UpdateGameUI();
	}

	public void ShowMainMenu()
	{
		StartGamePanel.Solo();

	}

	public void ShowGameScreen()
	{
		GameplayPanel.Solo();
	}

	public void ShowDeathScreen()
	{
		EndGamePanel.Solo();
	}

	public void UpdateGameUI()
	{
		if (GameManager.Instance.isGameRunning())
		{
			m_distanceText.text = "test";
			m_coinCountText.text = GameManager.Instance.GetCoinsCollected().ToString();
			m_distanceText.text = Mathf.RoundToInt(GameManager.Instance.GetCurrentDistance()).ToString() + "m";
		}
	}
}
