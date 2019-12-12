using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCameraFollow : MonoBehaviour
{
	public Transform followObject;
	public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.right * followObject.position.x + offset;

	}
}
