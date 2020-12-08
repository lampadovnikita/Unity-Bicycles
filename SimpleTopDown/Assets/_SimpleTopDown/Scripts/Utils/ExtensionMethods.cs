using UnityEngine;

namespace SimpleTopDown
{
	public static class ExtensionMethods
	{
		public static bool Contains(this LayerMask layerMask, int layerNum)
		{
			if ((layerMask | (1 << layerNum)) == layerMask)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}