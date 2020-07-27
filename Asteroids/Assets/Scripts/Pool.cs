using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
	[SerializeField]
	private GameObject templateObject = default;

	[SerializeField]
	private int numberOfPregenerated = 0;

	private List<GameObject> objects;

	private void Awake()
	{
		templateObject.SetActive(false);

		objects = new List<GameObject>();

		GameObject tmpObject;
		for (int i = 0; i < numberOfPregenerated; i++)
		{
			tmpObject = CreateObject();
			tmpObject.SetActive(false);
		}
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

	private GameObject CreateObject()
	{
		GameObject newObject = Instantiate(templateObject);
		newObject.transform.parent = templateObject.transform.parent;

		objects.Add(newObject);

		return newObject;
	}
}
