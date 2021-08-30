using UnityEngine;

namespace TowerBlocks
{
	public class Parallax : MonoBehaviour
	{
		[SerializeField]
		[Range(0f, 1f)]
		private float parallaxCoeff = 1f;

		[SerializeField]
		private Transform target = default;

		private Vector2 lastCameraPosition;

		private void Start()
		{
			lastCameraPosition = target.position;
		}

		private void LateUpdate()
		{
			Vector2 deltaPosition = (Vector2)target.position - lastCameraPosition;
			lastCameraPosition = target.position;

			transform.position += new Vector3(0f, deltaPosition.y * parallaxCoeff, 0f);
		}
	}
}
