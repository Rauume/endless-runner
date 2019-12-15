using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject 
{
	//easier to read, and can search for all pool objects when clearing screen.
	void returnToPool();
}
