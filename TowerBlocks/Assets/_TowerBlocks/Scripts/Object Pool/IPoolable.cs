using UnityEngine;

namespace TowerBlocks
{
	public delegate void Hide(GameObject caller);

	public interface IPoolable
	{
		event Hide OnHide;

		void Reveal();
	}
}