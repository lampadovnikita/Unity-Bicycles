using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	public class Tower : MonoBehaviour
	{
		public delegate void PerfectMounting(Tower sender, Block block);
		public event PerfectMounting OnPerfectMounting;

		[SerializeField]
		private BlockSpawner blockSpawner = default;

		[SerializeField]
		private Transform staticBlocksParent = default;

		[SerializeField]
		private Transform dynamicBlocksParent = default;

		[SerializeField]
		private AudioClip blockMountedClip = default;

		[SerializeField]
		private LevelController levelController = default;

		private TowerOptions currentTowerOptions;

		private OscillationData currentRotationOscillation;

		private float desiredFrequency;

		private List<Block> blocks;

		private Queue<Block> dynamicBlocksQueue;

		private Quaternion initialRotation;

		private float lifeTime;

		private void Awake()
		{
			Assert.IsNotNull(blockSpawner);
			Assert.IsNotNull(dynamicBlocksParent);
			Assert.IsNotNull(blockMountedClip);

			if (staticBlocksParent == null)
			{
				staticBlocksParent = transform;
			}

			blocks = new List<Block>();

			desiredFrequency = 0f;

			lifeTime = 0f;
		}

		private void Start()
		{
			blockSpawner.OnBlockSpawned += OnBlockSpawned;
			
			initialRotation = transform.rotation;

			currentTowerOptions = levelController.CurrentLevelOptions.towerOptions;

			dynamicBlocksQueue = new Queue<Block>(currentTowerOptions.DynamicBlocksAmount);

			currentRotationOscillation =
				new OscillationData(0f, currentTowerOptions.MaxRotationOscillation.amplitude);
		}

		private void FixedUpdate()
		{
			if (blocks.Count > currentTowerOptions.DynamicBlocksAmount)
			{
				lifeTime += Time.deltaTime;

				// Rotate tower accurding to the current parameters
				float rotationDegree =
					Mathf.Sin(2f * Mathf.PI * lifeTime * currentRotationOscillation.frequency)
					* currentRotationOscillation.amplitude;

				transform.rotation = Quaternion.AngleAxis(rotationDegree, transform.forward);
				
				// Smoothly adjust current frequency according to the desired frequency
				currentRotationOscillation.frequency = Mathf.SmoothStep(
					currentRotationOscillation.frequency,
					desiredFrequency,
					Time.deltaTime);
			}
		}

		public void ResetState()
		{
			blocks.Clear();
			dynamicBlocksQueue.Clear();

			transform.rotation = initialRotation;
		}

		private void OnBlockSpawned(BlockSpawner blockSpawner, Block block)
		{
			block.OnStateChanged += OnBlockStateChanged;
		}

		private void OnBlockStateChanged(Block block)
		{
			// If blocks collides with a tower
			if (block.State == BlockState.TOWER_MOUNTED)
			{
				AudioController.Instance.SFXAudioSource.PlayOneShot(blockMountedClip);

				// If the tower already contains any blocks
				if (blocks.Count > 0)
				{
					// Offset in x axis between new and the last top block
					float lastBlockDeltaX = Mathf.Abs(
						block.transform.position.x - blocks[blocks.Count - 1].transform.position.x);

					if (lastBlockDeltaX < currentTowerOptions.PerfectDeltaX)
					{
						OnPerfectMounting?.Invoke(this, block);
					}
					// If offset is good enought to slow down frequency of the tower rotation
					else if (lastBlockDeltaX < currentTowerOptions.SlowDownFrequencyDeltaX)
					{
						desiredFrequency = Mathf.Lerp(desiredFrequency, 0f, 0.2f);
					}
					// Else speed up frequency
					else
					{
						desiredFrequency =
							Mathf.Lerp(desiredFrequency, currentTowerOptions.MaxRotationOscillation.frequency, 0.2f);
					}
				}

				blocks.Add(block);

				AddToDynamic(block);
			}
		}

		private void AddToDynamic(Block block)
		{
			// If dynamic queue is full
			if (dynamicBlocksQueue.Count >= currentTowerOptions.DynamicBlocksAmount)
			{
				// Release bottom blocks
				Block dequeued = dynamicBlocksQueue.Dequeue();

				// And made it static
				AddToStatic(dequeued);
			}

			// Add top block to the dynamic queue
			dynamicBlocksQueue.Enqueue(block);

			block.IsFreezed = false;

			block.transform.parent = dynamicBlocksParent;
		}

		private void AddToStatic(Block block)
		{
			block.IsFreezed = true;

			block.transform.parent = staticBlocksParent;
		}
	}
}