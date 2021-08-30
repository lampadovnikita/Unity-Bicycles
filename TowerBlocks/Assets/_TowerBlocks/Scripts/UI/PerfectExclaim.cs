using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{ 
	public class PerfectExclaim : MonoBehaviour
	{
		[SerializeField]
		private Animator exclaimAnimator = default;

		[SerializeField]
		private Tower tower = default;

		private void Awake()
		{
			Assert.IsNotNull(exclaimAnimator);
			Assert.IsNotNull(tower);
		}

		private void Start()
		{
			tower.OnPerfectMounting += OnPerfectMounting;
		}

		private void OnPerfectMounting(Tower tower, Block block)
		{
			// This line restart exclaim animation
			exclaimAnimator.Play("Exclaim", -1, 0);
		}
	}
}


