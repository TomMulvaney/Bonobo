using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public static class MathHelpers 
	{
		public static bool IsSameSign(float a, float b)
		{
			return Mathf.Approximately(a * b, Mathf.Abs(a) * Mathf.Abs(b));
		}

		public static bool IsSameSign(int a, int b)
		{
			return a * b == Mathf.Abs (a) * Mathf.Abs (b);
		}
	}
}
