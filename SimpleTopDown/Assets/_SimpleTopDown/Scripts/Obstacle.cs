using UnityEngine;

namespace SimpleTopDown
{
	public class Obstacle : MonoBehaviour, IHittable
	{
		public HitResultType Hit(GameObject hitter)
		{
			return HitResultType.RICOCHET;
		}
	}
}

