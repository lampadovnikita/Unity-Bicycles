using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	// Observe blocks amount and update amount text
	public class BlocksAmountUI : MonoBehaviour
	{
		[SerializeField]
		private LevelController levelSystem = default;
		
		[SerializeField]
		private TextMeshProUGUI blocksAmountUGUI = default;

		private void Awake()
		{
			Assert.IsNotNull(levelSystem);
			Assert.IsNotNull(blocksAmountUGUI);
		}

		private void Start()
		{
			levelSystem.OnProgressChanged += OnProgressChanged;
		}

		private void OnProgressChanged(LevelController levelSystem)
		{
			blocksAmountUGUI.text = levelSystem.TotalBlockAmount.ToString();
		}
	}
}