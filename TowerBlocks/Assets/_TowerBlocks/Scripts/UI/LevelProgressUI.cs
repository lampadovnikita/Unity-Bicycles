using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TowerBlocks
{ 
	// Observe level progress and update progress bar
	public class LevelProgressUI : MonoBehaviour
	{
		[SerializeField]
		private LevelController levelSystem = default;

		[SerializeField]
		private Slider levelProgressSlider = default;

		private void Awake()
		{
			Assert.IsNotNull(levelSystem);
			Assert.IsNotNull(levelProgressSlider);
		}

		private void Start()
		{
			levelSystem.OnProgressChanged += OnProgressChanged;
		}

		private void OnProgressChanged(LevelController levelSystem)
		{
			levelProgressSlider.value =
				((float) levelSystem.CurrentProgress / levelSystem.CurrentLevelOptions.blockAmountGoal);
		}
	}
}


