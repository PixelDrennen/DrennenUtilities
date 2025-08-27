/*
    This script was created by Drennen Dooms, 2022.
*/

using UnityEngine;


namespace Drennen.Math
{
	public enum Interpolation
	{
		sine, circ, bounce, expo, back, elastic, cubic, quad, quint, quart, linear
	}
	public enum Direction
	{
		In, Out, Both
	}
	public static class Easing
	{

		public static float Ease(float x, Interpolation interpolation, Direction direction)
		{
			if (interpolation == Interpolation.bounce)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInBounce(x);
					case Direction.Out:
						return EaseOutBounce(x);
					case Direction.Both:
						return EaseInOutBounce(x);
				}
			}
			if (interpolation == Interpolation.linear)
			{
				return EaseInOutLin(x);
			}
			if (interpolation == Interpolation.circ)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInCirc(x);
					case Direction.Out:
						return EaseOutCirc(x);
					case Direction.Both:
						return EaseInOutCirc(x);
				}
			}
			if (interpolation == Interpolation.sine)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInSine(x);
					case Direction.Out:
						return EaseOutSine(x);
					case Direction.Both:
						return EaseInOutSine(x);
				}
			}
			if (interpolation == Interpolation.expo)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInExpo(x);
					case Direction.Out:
						return EaseOutExpo(x);
					case Direction.Both:
						return EaseInOutExpo(x);
				}
			}
			if (interpolation == Interpolation.back)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInBack(x);
					case Direction.Out:
						return EaseOutBack(x);
					case Direction.Both:
						return EaseInOutBack(x);
				}
			}
			if (interpolation == Interpolation.elastic)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInElastic(x);
					case Direction.Out:
						return EaseOutElastic(x);
					case Direction.Both:
						return EaseInOutElastic(x);
				}
			}
			if (interpolation == Interpolation.cubic)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInCubic(x);
					case Direction.Out:
						return EaseOutCubic(x);
					case Direction.Both:
						return EaseInOutCubic(x);
				}
			}
			if (interpolation == Interpolation.quad)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInQuad(x);
					case Direction.Out:
						return EaseOutQuad(x);
					case Direction.Both:
						return EaseInOutQuad(x);
				}
			}
			if (interpolation == Interpolation.quint)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInQuint(x);
					case Direction.Out:
						return EaseOutQuint(x);
					case Direction.Both:
						return EaseInOutQuint(x);
				}
			}
			if (interpolation == Interpolation.quart)
			{
				switch (direction)
				{
					case Direction.In:
						return EaseInQuart(x);
					case Direction.Out:
						return EaseOutQuart(x);
					case Direction.Both:
						return EaseInOutQuart(x);
				}
			}

			Debug.LogError($"Interpolation could not be parsed. {interpolation.ToString()} {direction.ToString()}");
			return 0f;
		}


		#region Bounce
		public static float EaseInBounce(float x)
		{
			return 1f - EaseOutBounce(x);
		}

		public static float EaseOutBounce(float x)
		{
			float n1 = 7.5625f;
			float d1 = 2.75f;

			if (x < 1 / d1)
			{
				return n1 * x * x;
			}
			else if (x < 2 / d1)
			{
				return n1 * (x -= 1.5f / d1) * x + 0.75f;
			}
			else if (x < 2.5 / d1)
			{
				return n1 * (x -= 2.25f / d1) * x + 0.9375f;
			}
			else
			{
				return n1 * (x -= 2.625f / d1) * x + 0.984375f;
			}
		}

		public static float EaseInOutBounce(float x)
		{
			return (x < 0.5f) ?
			(1f - EaseOutBounce(1f - 2f * x)) / 2f :
			(1f + EaseOutBounce(2f * x - 1f)) / 2f;
		}
		#endregion
		#region Linear
		public static float EaseInOutLin(float x)
		{
			return x;
		}

		#endregion

		#region Circ
		public static float EaseInOutCirc(float x)
		{
			float exp = (-2f * x + 2f);
			exp *= exp;

			if (x < 0.5f) return (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f;
			else return (Mathf.Sqrt(1f - exp) + 1f) / 2f;
		}


		public static float EaseOutCirc(float x)
		{
			if (x <= 0f) return 0f;
			// Debug.Log(x);
			// x = Mathf.Clamp01(x);

			x = x - 1f;
			// Debug.Log(x);

			x = Mathf.Sqrt(1f - x * x);
			// Debug.Log($"return {x}");

			return x;
		}


		public static float EaseInCirc(float x)
		{
			if (x <= 0f) return 0f;
			return 1f - Mathf.Sqrt(1f - x * x);
		}

		#endregion

		#region Sine

		public static float EaseInSine(float x)
		{
			return 1 - Mathf.Cos((x * Mathf.PI) / 2);
		}

		public static float EaseOutSine(float x)
		{
			return Mathf.Sin((x * Mathf.PI) / 2);
		}

		public static float EaseInOutSine(float x)
		{
			return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
		}

		#endregion

		#region Cubic
		public static float EaseInCubic(float x)
		{
			return x * x * x;
		}

		public static float EaseOutCubic(float x)
		{
			return 1f - Mathf.Pow(1 - x, 3);
		}
		public static float EaseInOutCubic(float x)
		{
			return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
		}
		#endregion

		#region Quint
		public static float EaseInQuint(float x)
		{
			return x * x * x * x * x;
		}
		public static float EaseOutQuint(float x)
		{
			return 1 - Mathf.Pow(1f - x, 5f);
		}
		public static float EaseInOutQuint(float x)
		{
			return (x < 0.5f) ? 16f * x * x * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 5f) / 2f;
		}
		#endregion

		#region Elastic

		public static float EaseInElastic(float x)
		{
			float c4 = (2f * Mathf.PI) / 3f;

			return (x == 0) ? 0 : (x == 1) ? 1 : -Mathf.Pow(2f, 10 * x - 10f) * Mathf.Sin((x * 10f - 10.75f) * c4);
		}

		public static float EaseOutElastic(float x)
		{
			float c4 = (2f * Mathf.PI) / 3f;

			return (x == 0) ? 0 : (x == 1) ? 1 : Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * c4) + 1;
		}

		public static float EaseInOutElastic(float x)
		{
			float c5 = (2 * Mathf.PI) / 4.5f;

			return (x == 0f) ? 0 :
				   (x == 1f) ? 1 :
				   (x < 0.5f) ? -(Mathf.Pow(2f, 20f * x - 10f) * Mathf.Sin((20f * x - 11.125f) * c5)) / 2f :
				   (Mathf.Pow(2f, -20f * x + 10f) * Mathf.Sin((20f * x - 11.125f) * c5)) / 2 + 1;
		}

		#endregion

		#region Quad

		public static float EaseInQuad(float x)
		{
			return x * x;
		}
		public static float EaseOutQuad(float x)
		{
			return 1f - (1f - x) * (1f - x);
		}
		public static float EaseInOutQuad(float x)
		{
			return (x < 0.5f) ? 2f * x * x : 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;
		}
		#endregion

		#region Quart

		public static float EaseInQuart(float x)
		{
			return x * x * x * x;
		}
		public static float EaseOutQuart(float x)
		{
			return 1f - Mathf.Pow(1f - x, 4f);
		}
		public static float EaseInOutQuart(float x)
		{
			return (x < 0.5f) ? 8 * x * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 4f) / 2f;
		}

		#endregion

		#region Expo

		public static float EaseInExpo(float x)
		{
			return (x == 0) ? 0 : Mathf.Pow(2f, 10f * x - 10f);
		}
		public static float EaseOutExpo(float x)
		{
			return (x == 1) ? 1 : 1 - Mathf.Pow(2f, -10f * x);
		}
		public static float EaseInOutExpo(float x)
		{
			return (x == 0) ? 0 : (x == 1) ? 1 :
				   (x < 0.5f) ? Mathf.Pow(2f, 20f * x - 10f) / 2f :
				   (2f - Mathf.Pow(2f, -20f * x + 10f)) / 2f;
		}

		#endregion

		#region Back

		public static float EaseInBack(float x)
		{
			float c1 = 1.70158f;
			float c3 = c1 + 1f;

			return c3 * x * x * x - c1 * x * x;
		}

		public static float EaseOutBack(float x)
		{
			float c1 = 1.70158f;
			float c3 = c1 + 1f;

			return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
		}

		public static float EaseInOutBack(float x)
		{
			float c1 = 1.70158f;
			float c2 = c1 * 1.525f;

			return (x < 0.5f) ?
					(Mathf.Pow(2f * x, 2f) * ((c2 + 1f) * 2f * x - c2)) / 2f :
					(Mathf.Pow(2f * x - 2f, 2f) * ((c2 + 1f) * (x * 2f - 2f) + c2) + 2f) / 2f;
		}

		#endregion
	}
}
