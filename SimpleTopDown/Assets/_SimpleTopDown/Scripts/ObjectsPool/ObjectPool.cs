using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{
	public class ObjectPool : MonoBehaviour
	{
		// Prefab to pool
		[SerializeField]
		private GameObject prefab = default;

		[SerializeField]
		private int initialAmount = 1;

		private List<GameObject> objects = default;

		private void Awake()
		{
			Assert.IsNotNull(prefab);
			Assert.IsNotNull(prefab.GetComponent<IPoolable>());

			objects = new List<GameObject>(initialAmount);

			GameObject obj;
			for (int i = 0; i < initialAmount; i++)
			{
				obj = CreateObject();
				objects.Add(obj);
			}
		}

		public GameObject Get()
		{
			GameObject obj;

			if (objects.Count > 0)
			{
				obj = objects[0];
				objects.RemoveAt(0);
			}
			else
			{
				obj = CreateObject();
			}

			IPoolable poolable = obj.GetComponent<IPoolable>();
			// Do some calculations as needed to restore the state of an object
			poolable.Reveal();

			return obj;
		}

		private GameObject CreateObject()
		{
			GameObject obj = Instantiate(prefab);
			obj.transform.parent = transform;
			obj.SetActive(false);

			IPoolable poolable = obj.GetComponent<IPoolable>();
			poolable.OnHide += Recycle;

			return obj;
		}

		// Prepare an object before inserting into the pool
		private void Recycle(GameObject caller)
		{
			if (objects.Contains(caller) == false)
			{
				caller.SetActive(false);
				caller.transform.parent = transform;
				objects.Add(caller);
			}
		}
	}
}

