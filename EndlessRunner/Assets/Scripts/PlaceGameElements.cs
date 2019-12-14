using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGameElements : MonoBehaviour
{
	public List<GameplayPrefab> gamePrefabs = new List<GameplayPrefab>();



    // Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("SpawnRandomGameSegment", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {

	}

	public void SpawnRandomGameSegment()
	{
		GameObject objectToSpawn = GetRandomGameElement();

		foreach (Transform t in objectToSpawn.transform)
		{
			GameObject laser = GameManager.Instance.GetPool(t.tag).GetPooledObject();
			laser.transform.position = t.position + GameManager.Instance.m_startPoint.position;
			laser.SetActive(true);
		}
	}

	public GameObject GetRandomGameElement()
	{
		float accumulatedWeight = 0;

		//calculate the total weight.
		foreach (GameplayPrefab gameplayPrefab in gamePrefabs)
		{
			gameplayPrefab.accumulatedWeight = 0;

			accumulatedWeight += gameplayPrefab.weight;
			//bcp.accumulatedWeight += accumulatedWeight;
		}

		float randomNo = Random.Range(0, accumulatedWeight);
		float total = 0;

		//pick the object with the closest weight to the random.
		foreach (GameplayPrefab gameplayPrefab in gamePrefabs)
		{
			randomNo = randomNo - gameplayPrefab.accumulatedWeight;

			total += gameplayPrefab.weight;

			if (total >= randomNo)
			{
				return gameplayPrefab.gameElement;
			}
		}

		//only occurs if no entries are present
		return null;

	}	
}

	[System.Serializable]
public class GameplayPrefab
{
	public string comment;

	[Header("Game element prefab")]
	public GameObject gameElement;

	
	[Header("Random weighted chance")]
	[Tooltip("Lower numbers are less likely, but its all relative.")]
	public float weight = 0;

	[HideInInspector]
	public float accumulatedWeight = 0;
}
