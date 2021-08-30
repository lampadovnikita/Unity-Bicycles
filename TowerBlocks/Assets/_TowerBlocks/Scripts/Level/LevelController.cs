using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	public class LevelController : MonoBehaviour
	{
		public delegate void LevelChanged(LevelController sender);
		public event LevelChanged OnLevelChanged;

		public delegate void ProgressChanged(LevelController sender);
		public event ProgressChanged OnProgressChanged;

		public delegate void ProgressComplete(LevelController sender);
		public event ProgressComplete OnProgressComplete;

		[SerializeField]
		private LevelsOptions levelsOptions = default;

		[SerializeField]
		private BlockSpawner blockSpawner = default;

		private int currentLevelIndex;

		private int currentProgress;

		private int previousLevelsBlockAmount;

		public int CurrentLevelIndex
		{
			get
			{
				return currentLevelIndex;
			}
			private set
			{
				Assert.IsTrue(value >= 0);

				if (value < levelsOptions.LevelsCollection.Count)
				
				{
					currentLevelIndex = value;
					
					OnLevelChanged?.Invoke(this);
				}
			}
		}

		public LevelData CurrentLevelOptions =>
			levelsOptions.LevelsCollection[CurrentLevelIndex];
		
		public int CurrentProgress
		{
			get
			{
				return currentProgress;
			}
			private set
			{
				currentProgress = value;

				OnProgressChanged?.Invoke(this);
			}
		}

		public int TotalBlockAmount => 
			previousLevelsBlockAmount + currentProgress;

		private bool isProgressCompleted =>
			CurrentProgress >= levelsOptions.LevelsCollection[CurrentLevelIndex].blockAmountGoal;

		private void Awake()
		{
			Assert.IsNotNull(levelsOptions);
			Assert.IsNotNull(blockSpawner);

		}

		private void Start()
		{
			LoadData();
		}

		private void OnEnable()
		{
			blockSpawner.OnBlockSpawned += OnBlockSpawned;

		}

		private void OnDisable()
		{
			blockSpawner.OnBlockSpawned -= OnBlockSpawned;
		}

		// Load data about game progress from a file
		public void LoadData()
		{
			SaveData saveData = SaveLoader.Load();

			CurrentLevelIndex = saveData.gameProgressData.levelIndex;
			previousLevelsBlockAmount = saveData.gameProgressData.previousLevelsBlocksAmount;

			CurrentProgress = 0;
		}

		public void SetLevel(int levelIndex)
		{
			if (isProgressCompleted == true)
			{
				previousLevelsBlockAmount += currentProgress;
			}

			CurrentProgress = 0;

			CurrentLevelIndex = levelIndex;
		}

		private void OnBlockSpawned(BlockSpawner blockSpawner, Block block)
		{
			block.OnStateChanged += OnBlockStateChanged;
		}

		private void OnBlockStateChanged(Block block)
		{
			if (block.State == BlockState.TOWER_MOUNTED)
			{
				CurrentProgress++;

				if (isProgressCompleted == true)
				{
					OnProgressComplete?.Invoke(this);
				}
			}
		}
	}
}

