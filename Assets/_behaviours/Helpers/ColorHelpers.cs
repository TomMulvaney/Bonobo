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
	}
}