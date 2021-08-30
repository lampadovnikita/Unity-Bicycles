using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	public class ScreenController : MonoBehaviour
	{
		private const float EPSILON = 0.01f;

		[SerializeField]
		private Transform contentParentTransform = default;

		[SerializeField]
		private Camera screenCamera = default;

		// It means only screenPortionForBlocks % of the screen will be occupied by blocks
		[SerializeField]
		[Range(0f, 1f)]
		private float screenPortionForBlocks = 0.33f;

		[SerializeField]
		private float screenSpeed = 0.5f;

		public float ScreenHalfWidth => screenCamera.orthographicSize * screenCamera.aspect;

		public float ScreenHalfHeight => screenCamera.orthographicSize;

		public Vector3 ScreenCenter => contentParentTransform.position;

		// The height of a line that must positions above a top block
		private float HeightAboveTopBlock
		{
			get
			{
				// Get the height of the screen bottom
				float height = ScreenCenter.y - ScreenHalfHeight;
				// Calculate height of line position
				height += ScreenHalfHeight * 2 * screenPortionForBlocks;

				return height;
			}
		}

		private Vector3 currentVelocity;

		private Vector3 initialPosition;

		private void Awake()
		{
			Assert.IsNotNull(contentParentTransform);
			Assert.IsNotNull(screenCamera);

			currentVelocity = new Vector3();
		}

		private void Start()
		{
			initialPosition = transform.position;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;

			// Draw the HeightAboveTopBlock line to see the heigh
			Gizmos.DrawLine(
				new Vector3(ScreenCenter.x - ScreenHalfWidth, HeightAboveTopBlock, ScreenCenter.z),
				new Vector3(ScreenCenter.x + ScreenHalfWidth, HeightAboveTopBlock, ScreenCenter.z));
		}

		public void MoveScreenUpward(Block block)
		{
			float differenceY = block.MaxWorldY - HeightAboveTopBlock;

			// If a blocks max Y above HeightAboveTopBlock
			// Then we shift the screen so that the HeightAboveTopBlock is just above the block
			if (differenceY > 0f)
			{
				StopAllCoroutines();

				float timeToReach = screenSpeed / differenceY;
				Vector3 destination = transform.position + new Vector3(0f, differenceY, 0f);

				StartCoroutine(SmoothMove(destination, timeToReach));
			}
		}

		public void ResetPosition()
		{
			StopAllCoroutines();

			transform.position = initialPosition;
		}

		private IEnumerator SmoothMove(Vector3 destination, float timeToReach)
		{
			currentVelocity = Vector3.zero;

			while (Mathf.Abs(transform.position.sqrMagnitude - destination.sqrMagnitude) > EPSILON)
			{
				transform.position =
					Vector3.SmoothDamp(transform.position, destination, ref currentVelocity, timeToReach);

				yield return null;
			}
		}
	}
}
