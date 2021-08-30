using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace TowerBlocks
{
	// ScriptableObject for setting parameters of all levels of the game
	[CreateAssetMenu]
	public class LevelsOptions : ScriptableObject
	{
		[SerializeField]
		private List<LevelData> levelsCollection = default;

		public ReadOnlyCollection<LevelData> LevelsCollection { get; private set; }

		private void OnEnable()
		{
			LevelsCollection = new ReadOnlyCollection<LevelData>(levelsCollection);
		}
	}
}
