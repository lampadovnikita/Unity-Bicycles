using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleTopDown
{
	public class Game : MonoBehaviour
	{
		[SerializeField]
		private Character player = default;

		[SerializeField]
		private Character enemy = default;

		[SerializeField]
		private bool doResetPositions = true;

		private Vector3 playerInitialPosition;
		private Vector3 enemyInitialPosition;

		private void Awake()
		{
			Assert.IsNotNull(player);
			Assert.IsNotNull(enemy);
		}

		private void Start()
		{
			player.OnCharacterHit += OnPlayerHit;
			playerInitialPosition = player.transform.position;

			enemy.OnCharacterHit += OnEnemyHit;
			enemyInitialPosition = enemy.transform.position;
		}

		private void OnPlayerHit(Character player)
		{
			enemy.Score += 1;

			ResetPositions();
		}

		private void OnEnemyHit(Character enemy)
		{
			player.Score += 1;

			ResetPositions();
		}

		private void ResetPositions()
		{
			if (doResetPositions)
			{
				player.transform.position = playerInitialPosition;
				enemy.transform.position = enemyInitialPosition;
			}
		}
	}
}