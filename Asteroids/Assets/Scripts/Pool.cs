using System.Collections.Generic;
using UnityEngine;


public class Pool : MonoBehaviour
{
	[SerializeField]
	private GameObject objectPrefab = default;

	[SerializeField]
	private int numberOfPregenerated = 0;

	private List<GameObject> objects;

	private void Awake()
	{
		objects = new List<GameObject>();

		Pregenerate();
	}

	public GameObject GetObject()
	{
		foreach (GameObject obj in objects)
		{
			if (obj.activeInHierarchy == false)
			{
				obj.SetActive(true);
				return obj;
			}
		}

		GameObject newObject = CreateObject();

		return newObject;
	}

	private void Pregenerate()
	{
		GameObject tmpObject;
		for (int i = 0; i < numberOfPregenerated; i++)
		{
			tmpObject = CreateObject();
			tmpObject.SetActive(false);
		}
	}

	private GameObject CreateObject()
	{
		GameObject newObject = Instantiate(objectPrefab);
		newObject.transform.parent = gameObject.transform;
		objects.Add(newObject);

		return newObject;
	}
}
