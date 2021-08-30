using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	// Observe level number and update number text
	public class LevelNumberUI : MonoBehaviour
	{
		[SerializeField]
		private LevelController levelSystem = default;

		[SerializeField]
		private TextMeshProUGUI levelNumberUGUI = default;

		[SerializeField]
		private string preNumberText = "LEVEL ";

		private void Awake()
		{
			Assert.IsNotNull(levelSystem);
			Assert.IsNotNull(levelNumberUGUI);
		}

		private void Start()
		{
			levelSystem.OnLevelChanged += OnLevelChanged;
		}

		private void OnLevelChanged(LevelController levelSystem)
		{
			levelNumberUGUI.text = preNumberText + (levelSystem.CurrentLevelIndex + 1);
		}
	}
}

