using UnityEngine;

namespace TowerBlocks
{
	[CreateAssetMenu]
	public class CraneOptions : ScriptableObject
	{
		[SerializeField]
		private OscillationData rotationOscillation = default;

		[SerializeField]
		private OscillationData horizontalOscillation = default;

		[SerializeField]
		private OscillationData verticalOscillation = default;

		public OscillationData RotationOscillation => rotationOscillation;

		public OscillationData HorizontalOscillation => horizontalOscillation;

		public OscillationData VerticalOscillation => verticalOscillation;

	}
}
