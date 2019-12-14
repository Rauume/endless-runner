using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	public GameObject StartGamePanel;
	public GameObject GameplayPanel;
	public GameObject EndGamePanel;

	[Header("UI Elements")]
	public TextMeshProUGUI m_distanceText;
	public TextMeshProUGUI m_coinCountText;

	private void Awake()
	{
		ShowMainMenu();
	}

	private void FixedUpdate()
	{
		UpdateGameUI();
	}

	public void ShowMainMenu()
	{
		StartGamePanel.SetActive(true);
		GameplayPanel.SetActive(false);
		EndGamePanel.SetActive(false);
	}

	public void ShowGameScreen()
	{
		StartGamePanel.SetActive(false);
		GameplayPanel.SetActive(true);
		EndGamePanel.SetActive(false);
	}

	public void ShowDeathScreen()
	{
		StartGamePanel.SetActive(false);
		GameplayPanel.SetActive(false);
		EndGamePanel.SetActive(true);
	}

	public void UpdateGameUI()
	{
		if (GameManager.Instance.isGameRunning())
		{
			m_distanceText.text = "test";
			m_coinCountText.text = GameManager.Instance.getCoinsCollected().ToString();
		}
	}


}
