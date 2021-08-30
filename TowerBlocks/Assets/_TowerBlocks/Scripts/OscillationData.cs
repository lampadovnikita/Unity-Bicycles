namespace TowerBlocks
{
	[System.Serializable]
	public struct OscillationData
	{
		public float frequency;

		public float amplitude;

		public OscillationData(float frequency, float amplitude)
		{
			this.frequency = frequency;
			this.amplitude = amplitude;
		}
	}
}

