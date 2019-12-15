using UnityEngine;
using TMPro;

public class HighScores : MonoBehaviour
{
	[Header("UI Elements")]
	public TextMeshProUGUI HighScoreText;
	public TextMeshProUGUI CurrentScoreText;


	private void OnEnable()
	{
		int currentScore = Mathf.RoundToInt(GameManager.Instance.GetCurrentDistance());

		if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
		{
			PlayerPrefs.SetInt("HighScore", currentScore);
			HighScoreText.text = "High Score: " + currentScore.ToString();
			
		}
		else
		{
			HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

		}

		CurrentScoreText.text = "Score: " + currentScore.ToString();
	}
}
