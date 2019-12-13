using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

[CreateAssetMenu(fileName = "LaserEvent", menuName = "Events/Laser/testr")]
public class LaserEvent : ScriptedEvent
{
	public int lasersRequired = 1;
	public float margin = 0;

	protected List<GameObject> localLasers = new List<GameObject>();

	public override void OnStart()
	{
		for(int i = 0; i < lasersRequired; i++)
		{
			GameObject laser = GameManager.Instance.GetPool("Laser").GetPooledObject();
			float distance = (GameManager.m_playAreaHeight - margin) / (lasersRequired);

			laser.transform.position = GameManager.Instance.m_startPoint.position + (Vector3.up * (distance * (i + 1)));
			laser.SetActive(true);
			localLasers.Add(laser);
		}
	}

	public override void Update()
	{

		foreach(GameObject laser in localLasers)
		{
			laser.transform.Translate((Vector3.right * -GameManager.Instance.m_globalScrollSpeed) * Time.deltaTime);
		}
	}

	public override void OnStop()
	{

	}
}
