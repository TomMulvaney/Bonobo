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

		public static bool IsPrime(int number)
		{
			int boundary = Mathf.FloorToInt(Mathf.Sqrt(number));
			
			if (number == 1)
			{
				return false;
			} 
			else if (number == 2)
			{
				return true;
			}
			
			for (int i = 2; i <= boundary; ++i)  
			{
				if (number % i == 0)  
				{
					return false;
				}
			}
			
			return true;        
		}
		
		public static int GetDigitCount(int i)
		{
			return i.ToString().Replace("-", "").Length;
		}

		public static int GetNegative(int i)
		{
			return -1 * Mathf.Abs(i);
		}

        public static bool GreaterThan(float f1, float f2)
        {
            return f1 > f2 && !Mathf.Approximately(f1, f2);
        }
        
        public static bool LessThan(float f1, float f2)
        {
            return f1 < f2 && !Mathf.Approximately(f1, f2);
        }
	}
}
