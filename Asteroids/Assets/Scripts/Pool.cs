using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pool : ScriptableObject
{
	[SerializeField]
	private GameObject objectPrefab = default;

	[SerializeField]
	private int numberOfPregenerated = 0;

	private List<GameObject> objects;

	public void Initialize()
	{
		Initialize(null);
	}

	public void Initialize(GameObject parentObject)
	{
		objects = new List<GameObject>();

		GameObject tmpObject;
		for (int i = 0; i < numberOfPregenerated; i++)
		{
			tmpObject = CreateObject(parentObject);
			tmpObject.SetActive(false);
		}
	}

	public GameObject GetObject()
	{
		return GetObject(null);
	}

	public GameObject GetObject(GameObject parentObject)
	{
		foreach (GameObject obj in objects)
		{
			if (obj.activeInHierarchy == false)
			{
				if (parentObject != null)
				{ 
					obj.transform.parent = parentObject.transform;
				}

				obj.SetActive(true);
				return obj;
			}
		}

		GameObject newObject = CreateObject(parentObject);

		return newObject;
	}

	private GameObject CreateObject(GameObject parentObject)
	{
		GameObject newObject = Instantiate(objectPrefab);

		if (parentObject != null)
		{ 
			newObject.transform.parent = parentObject.transform;
		}

		objects.Add(newObject);

		return newObject;
	}
}
