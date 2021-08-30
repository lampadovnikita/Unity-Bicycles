using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TowerBlocks
{
	public class BlockFactory : MonoBehaviour
	{
		[SerializeField]
		private List<ObjectPool> pools = default;

		private void Awake()
		{
			Assert.IsNotNull(pools);
			Assert.IsTrue(pools.Count > 0);
		}

		public Block GetRandom()
		{
			GameObject obj = pools[Random.Range(0, pools.Count)].Get();

			Block block = obj.GetComponent<Block>();
			Assert.IsNotNull(block);

			return block;
		}
	}
	
}