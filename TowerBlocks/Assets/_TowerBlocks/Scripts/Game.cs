using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	public class Game : MonoBehaviour
	{
		[SerializeField]
		private ScreenController screenController = default;

		[SerializeField]
		private BlockSpawner blockSpawner = default;

		[SerializeField]
		private Tower tower = default;

		[SerializeField]
		private LevelController levelSystem = default;

		[SerializeField]
		private InputController inputController = default;

		[SerializeField]
		private Animator interLevelAnimator = default;

		[SerializeField]
		private Animator restartLevelAnimator = default;

		private void Awake()
		{
			Assert.IsNotNull(screenController);
			
			Assert.IsNotNull(blockSpawner);
			Assert.IsNotNull(tower);
			Assert.IsNotNull(levelSystem);
			Assert.IsNotNull(inputController);

			Assert.IsNotNull(interLevelAnimator);
			Assert.IsNotNull(restartLevelAnimator);

			Time.timeScale = 1f;
		}

		private void Start()
		{
			blockSpawner.OnBlockSpawned += OnBlockSpawned;

			levelSystem.OnProgressComplete += OnLevelProgressCompleted;

			blockSpawner.Spawn();
		}

		private void OnBlockSpawned(BlockSpawner sender, Block spawnedBlock)
		{
			spawnedBlock.OnStateChanged += OnBlockStateChanged;
		}

		private void OnBlockStateChanged(Block block)
		{
			// If a block collides with the tower then spawn new block
			if (block.State == BlockState.TOWER_MOUNTED)
			{
				blockSpawner.Spawn();

				screenController.MoveScreenUpward(block);
			}
			else if (block.State == BlockState.FALL)
			{
				RestartLevel();
			}
		}

		private void RestartLevel()
		{
			StartCoroutine(ChangeLevelFade(restartLevelAnimator, levelSystem.CurrentLevelIndex));

		}

		private void OnLevelProgressCompleted(LevelController levelSystem)
		{
			StartCoroutine(ChangeLevelFade(interLevelAnimator, levelSystem.CurrentLevelIndex + 1));
		}

		// Crossfade the screen then reset levels objects then take away crossfade
		private IEnumerator ChangeLevelFade(Animator animator, int index)
		{
			// Wait for the crossfade animation finish
			animator.gameObject.SetActive(true);
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

			PrepareLevel(index);
			
			// Take away the crossfade
			animator.SetTrigger("End");
			yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
			
			animator.gameObject.SetActive(false);
		}

		private void PrepareLevel(int index)
		{
			levelSystem.SetLevel(index);

			// Save progress between level resets
			SaveLoader.Save(new SaveData(
				new GameProgressData(levelSystem.CurrentLevelIndex, levelSystem.TotalBlockAmount)));

			tower.ResetState();

			// Hide all blocks to pools
			Block[] blocks = FindObjectsOfType<Block>();
			foreach (Block block in blocks)
			{
				block.Hide();
			}

			screenController.ResetPosition();
			blockSpawner.Spawn();
		}
	}
}

