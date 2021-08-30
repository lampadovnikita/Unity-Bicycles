using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	public class BlockSpawner : MonoBehaviour
	{
		public delegate void BlockSpawned(BlockSpawner sender, Block spawnedBlock);
		public event BlockSpawned OnBlockSpawned;

		[SerializeField]
		private BlockFactory blockFactory = default;

		private Block currentBlock;

		private void Awake()
		{
			Assert.IsNotNull(blockFactory);
		}

		private void OnDrawGizmos()
		{
#if UNITY_EDITOR
			Handles.Label(transform.position, "Spawn point");
#endif
		}

		public void AttemptFreeBlock()
		{
			currentBlock.AtemptFree();
		}

		// Get random block and spawn
		public void Spawn()
		{
			currentBlock = blockFactory.GetRandom();
			
			currentBlock.transform.parent = transform;
			currentBlock.transform.position = transform.position;
			currentBlock.transform.rotation = transform.rotation;

			currentBlock.IsFreezed = true;

			OnBlockSpawned?.Invoke(this, currentBlock);
		}
	}
}