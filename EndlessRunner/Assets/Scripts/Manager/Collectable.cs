using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Collectable : MonoBehaviour
{
	[Header("Collection Settings")]
	[Tooltip("Time after collection before moving to the player")]
	public float m_delayAfterCollection = 1f;

	[Tooltip("Animation curve, how it scopes back to the player")]
	public AnimationCurve m_tweenStyle = Tween.EaseOut;
	
	protected float m_timeSinceInteraction = 0;
	protected bool m_collected = false;
	protected Transform m_collector = null;

	private void OnEnable()
	{
		m_collected = false;
		m_timeSinceInteraction = 0;
	}

	//when the player passes through
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player2D")
		{
			m_collected = true;
			Tween.LocalPosition(transform, new Vector3(0, GameManager.m_playAreaHeight + 2, 0), 0.5f, 0, m_tweenStyle);
			m_collector = collision.transform;
		}
	}


	void Update()
	{ 
		//automatically scroll along the screen.
		transform.Translate((Vector3.right * -GameManager.Instance.m_globalScrollSpeed) * Time.deltaTime);

		//update time since collection.
		if (m_collected)
			m_timeSinceInteraction += Time.deltaTime;

		//if collected and close enough to the player, disable.
		if (m_timeSinceInteraction > m_delayAfterCollection)
		{
			GameManager.Instance.AddCoin();
			gameObject.SetActive(false);
		}
	}
}
