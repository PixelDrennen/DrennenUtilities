using UnityEngine;

namespace DrennenUtilities
{
	public static class Extensions
	{
		public static Color MultiplyColor(this Color c, float mod)
		{
			return c * mod;
		}
	}
}