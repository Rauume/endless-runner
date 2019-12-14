using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Collectable : MonoBehaviour
{
	[Header("Collection Settings")]
	[Tooltip("How close to the player before disappearing")]
	public float m_collectionTolerance = 0.1f;
	[Tooltip("Time after collection before moving to the player")]
	public float m_delayOnCollection = 0.8f;

	[Tooltip("Animation curve, how it scopes back to the player")]
	public AnimationCurve tweenStyle = Tween.EaseOut;
	
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
			Tween.LocalPosition(transform, collision.transform.position, 0.5f, m_delayOnCollection, tweenStyle);
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
		if (m_collected && transform.position.x >= m_collector.position.x - m_collectionTolerance && m_timeSinceInteraction > m_delayOnCollection)
		{
			GameManager.Instance.AddCoin();
			gameObject.SetActive(false);
		}
	}


}
