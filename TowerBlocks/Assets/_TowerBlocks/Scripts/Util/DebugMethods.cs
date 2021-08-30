using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	public class DebugMethods : MonoBehaviour
	{
#if UNITY_EDITOR

		[SerializeField]
		private Transform screenParentTransform = default;

		[SerializeField]
		private Block blockPrefab = default;

		[SerializeField]
		private float speed = 0.5f;

		[SerializeField]
		private KeyCode moveUp = KeyCode.W;

		[SerializeField]
		private KeyCode moveDown = KeyCode.S;

		[SerializeField]
		private KeyCode resetKey = KeyCode.R;

		[SerializeField]
		private KeyCode spawnBlockKey = KeyCode.E;

		private Vector3 initialPosition;

		private Vector2 mousePosition;

		private void Awake()
		{
			Assert.IsNotNull(screenParentTransform);
			Assert.IsNotNull(blockPrefab);

			mousePosition = new Vector2();
		}

		private void Start()
		{
			initialPosition = screenParentTransform.position;
		}

		private void Update()
		{
			if (Input.GetKey(moveUp))
			{
				screenParentTransform.position += transform.up * speed;
			}

			if (Input.GetKey(moveDown))
			{
				screenParentTransform.position -= transform.up * speed;
			}

			if (Input.GetKey(resetKey))
			{
				screenParentTransform.position = initialPosition;
			}

			if (Input.GetKeyDown(spawnBlockKey) == true)
			{
				mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				Instantiate(blockPrefab, mousePosition, Quaternion.identity);
			}
		}
#endif
	}
}