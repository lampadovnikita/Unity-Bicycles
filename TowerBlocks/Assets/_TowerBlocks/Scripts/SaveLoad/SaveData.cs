namespace TowerBlocks
{
	// Сan consist of many structures with different parameters
	[System.Serializable]
	public struct SaveData
	{
		public GameProgressData gameProgressData;

		public SaveData(GameProgressData gameProgressData)
		{
			this.gameProgressData = gameProgressData;
		}
	}
}

