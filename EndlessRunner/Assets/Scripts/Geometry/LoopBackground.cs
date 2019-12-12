using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ScrollObject
{
	public string comment;
	public GameObject m_object;
	public float m_scrollSpeed;

	//For instantiating via script.
	public ScrollObject(GameObject obj, float speed)
	{
		m_object = obj;
		m_scrollSpeed = speed;
	}
}

/// <summary>
/// Obtained this script from https://pressstart.vip/tutorials/2019/04/15/93/endless-2d-background.html
/// Modified it so that the objects scroll instead of the camera to work with my character controller.
/// </summary>
public class LoopBackground : MonoBehaviour
{
	public ScrollObject[] levels;
	private Camera mainCamera;
	private Vector2 screenBounds;
	public float choke;

	void Start()
	{
		mainCamera = Camera.main;
		screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

		foreach (ScrollObject obj in levels)
		{
			loadChildObjects(obj.m_object);
		}
	}

	//create enough sprites for the tiling background.
	void loadChildObjects(GameObject obj)
	{
		float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
		int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
		GameObject clone = Instantiate(obj) as GameObject;

		for (int i = 0; i <= childsNeeded; i++)
		{
			GameObject c = Instantiate(clone) as GameObject;
			c.transform.SetParent(obj.transform);
			c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
			c.name = obj.name + i;
		}

		Destroy(clone);
		Destroy(obj.GetComponent<SpriteRenderer>());
	}

	//update the order and position of the objects when reaching the end.
	void repositionChildObjects(GameObject obj)
	{
		Transform[] children = obj.GetComponentsInChildren<Transform>();
		if (children.Length > 1)
		{
			//assign the first and last child
			GameObject firstChild = children[1].gameObject;
			GameObject lastChild = children[children.Length - 1].gameObject;
			float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;

			if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
			{
				firstChild.transform.SetAsLastSibling();
				firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
			} 
			else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
			{
				lastChild.transform.SetAsFirstSibling();
				lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
			}
		}
	}

	void Update()
	{
		//Scroll the objects along the screen.
		foreach (ScrollObject scrollObject in levels)
		{
			//Scroll by the desired amount each second
			scrollObject.m_object.transform.Translate((Vector3.right * -scrollObject.m_scrollSpeed) * Time.deltaTime);
		}
	}

	void LateUpdate()
	{
		foreach (ScrollObject scrollObject in levels)
		{
			repositionChildObjects(scrollObject.m_object);
		}
	}
}
