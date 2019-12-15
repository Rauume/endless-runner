using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
		transform.Translate((Vector3.right * -GameManager.Instance.m_globalScrollSpeed) * Time.deltaTime);
	}
}
