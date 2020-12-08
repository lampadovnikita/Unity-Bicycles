using UnityEngine;

namespace SimpleTopDown
{ 
	// Object that can be hitten
	public interface IHittable
	{
		// Return a result to a hitter to determine behavior
		HitResultType Hit(GameObject hitter);
	}
}

