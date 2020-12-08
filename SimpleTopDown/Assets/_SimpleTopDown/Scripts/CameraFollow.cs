using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{ 
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField]
		private Transform target = default;

		[SerializeField]
		private float smoothness = 10f;

		private float invSmoothness;

		private Vector3 zOffset;

		private void Awake()
		{
			Assert.IsNotNull(target);

			invSmoothness = 1f / smoothness;

			zOffset = new Vector3(0f, 0f, transform.position.z);
		}

		private void Start()
		{
			transform.position = target.position;
			transform.position += zOffset;
		}

		private void FixedUpdate()
		{
			transform.position = Vector2.Lerp(transform.position, target.position, invSmoothness);
			transform.position += zOffset;
		}
	}
}

