using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	// Class that controll crane axis oscillation
	public class CraneMovement : MonoBehaviour
	{
		[SerializeField]
		private LevelController levelController = default;

		private CraneOptions currentCraneOptions;

		private Vector3 initialLocalPostion;

		private Vector3 offset;

		private float lifeTime;

		private void Awake()
		{
			Assert.IsNotNull(levelController);

			lifeTime = 0f;
		}

		private void Start()
		{
			initialLocalPostion = transform.localPosition;
			currentCraneOptions = levelController.CurrentLevelOptions.craneOptions;
			
			Assert.IsNotNull(currentCraneOptions);
		}

		private void OnEnable()
		{
			levelController.OnLevelChanged += OnLevelChanged;
		}

		private void OnDisable()
		{
			levelController.OnLevelChanged -= OnLevelChanged;
		}

		private void FixedUpdate()
		{
			lifeTime += Time.deltaTime;
			
			// Change rotation according to oscillation parameters
			float rotationDegree =
				Mathf.Sin(2f * Mathf.PI * lifeTime * currentCraneOptions.RotationOscillation.frequency)
				* currentCraneOptions.RotationOscillation.amplitude;

			transform.rotation = Quaternion.AngleAxis(rotationDegree, transform.forward);

			// Change position according to oscillation parameters
			offset.x = 
				Mathf.Cos(2 * Mathf.PI * lifeTime * currentCraneOptions.HorizontalOscillation.frequency)
				* currentCraneOptions.HorizontalOscillation.amplitude;

			offset.y = 
				Mathf.Cos(2 * Mathf.PI * lifeTime * currentCraneOptions.VerticalOscillation.frequency)
				* currentCraneOptions.VerticalOscillation.amplitude;

			transform.localPosition = initialLocalPostion + offset;
		}

		private void OnDrawGizmos()
		{
#if UNITY_EDITOR
			Handles.Label(transform.position, "Crane axis");
#endif
		}

		private void OnLevelChanged(LevelController levelController)
		{
			currentCraneOptions = levelController.CurrentLevelOptions.craneOptions;
			Assert.IsNotNull(currentCraneOptions);
		}
	}
}
