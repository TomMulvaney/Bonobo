using UnityEngine;
using System.Collections;
using System;

namespace Bonobo
{
    public class BonoboColorNotFoundException : Exception
    {
        public BonoboColorNotFoundException(string message) : base(message){}
    }

	public enum BonoboColor
	{
		Red,
		Green,
		Blue,
		Yellow,
		Pink,
		Orange,
		White,
		Black,
        Gray
	}

	public static class ColorHelpers
	{
        public static BonoboColor GetBonoboColor(string colorName)
        {
            try
            {
                return (BonoboColor)System.Enum.Parse(typeof(BonoboColor), StringHelpers.FirstLetterToUpper(colorName));
            }
            catch
            {
                throw new BonoboColorNotFoundException(colorName + " is not recognised BonoboColor");
            }
        }

		public static Color GetColor(string colorName)
		{
			try
			{
				return GetColor(GetBonoboColor(colorName));
			}
			catch(BonoboColorNotFoundException ex)
			{
				Debug.LogError(ex.Message);
				return Color.white;		
			}
		}

		public static Color GetColor(BonoboColor bonoboColor)
		{
			switch (bonoboColor)
            {
            case BonoboColor.Red:
                return Color.red;
                break;
            case BonoboColor.Green:
                return Color.green;
                break;
            case BonoboColor.Blue:
                return Color.blue;
                break;
            case BonoboColor.Yellow:
                return Color.yellow;
                break;
            case BonoboColor.Pink:
                return new Color(1f, 0.3f, 0.6f);
                break;
            case BonoboColor.Orange:
                return new Color(1f, 0.6f, 0.3f);
                break;
            case BonoboColor.White:
                return Color.white;
                break;
            case BonoboColor.Black:
                return Color.black;
                break;
            case BonoboColor.Gray:
                return Color.gray;
                break;
            default:
                return Color.white;
                break;
            }
		}

        public static Color HexToColor(string hex)
        {
            try
            {
                hex = StringHelpers.OnlyAlphaNum(hex);
                
                string[] hexValues = new string[3];
                hexValues[0] = hex.Substring(0, 2);
                hexValues[1] = hex.Substring(2, 2);
                hexValues[2] = hex.Substring(4, 2);
                
                float[] floatValues = new float[3];
                for(int i = 0; i < floatValues.Length; ++i)
                {
                    int decimalValue = System.Convert.ToInt32(hexValues[i], 16);
                    floatValues[i] = (float)decimalValue / 255f;
                    Mathf.Clamp01(floatValues[i]); //Defensive
                }
                
                return new Color(floatValues[0], floatValues[1], floatValues[2]);
            }
            catch
            {
                throw new Exception(string.Format("Could not convert {0} to color", hex));
            }
        }
	}
}