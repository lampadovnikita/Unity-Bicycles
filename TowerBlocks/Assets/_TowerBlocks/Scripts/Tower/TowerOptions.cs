using UnityEngine;

namespace TowerBlocks
{ 
	[CreateAssetMenu]
    public class TowerOptions : ScriptableObject
    {
		[SerializeField]
		private OscillationData maxRotationOscillation = default;

		[SerializeField]
		[Tooltip("Amount of dynamic blocks at the top of the tower")]
		private int dynamicBlocksAmount = 3;

		[SerializeField]
		[Tooltip("Delta X Between a previous top block and a new top block" +
				 " to trigger perfect mounting")]
		private float perfectDeltaX = 0.1f;

		[SerializeField]
		[Tooltip("Delta X Between a previous top block and a new top block" +
				 " to trigger rotation frequency slow down")]
		private float slowDownFrequencyDeltaX = 0.2f;

		public OscillationData MaxRotationOscillation =>
			maxRotationOscillation;

		public int DynamicBlocksAmount => 
			dynamicBlocksAmount;

		public float PerfectDeltaX =>
			perfectDeltaX;

		public float SlowDownFrequencyDeltaX =>
			slowDownFrequencyDeltaX;
	}
}
