namespace TowerBlocks
{
	[System.Serializable]
	public struct GameProgressData
	{
		public int levelIndex;

		public int previousLevelsBlocksAmount;

		public GameProgressData(int levelIndex, int previousLevelsBlocksAmount)
		{
			this.levelIndex = levelIndex;
			this.previousLevelsBlocksAmount = previousLevelsBlocksAmount;
		}
	}
}