﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaserTopBottomEvent_", menuName = "Events/Laser/TopBottom")]
public class LaserTopBottom : ScriptedEvent
{
	public override void OnStart()
	{
		
	}	

	public override void Update()
	{
		Debug.Log("test");
	}

	public override void OnStop()
	{

	}
}
