using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeEditorAttempt
{
	public class Utils
	{
		static Random random = new Random();

		private static byte ClampValue(float value)
		{
			return (byte)(value * 255.0f);
		}

		public static Color ColorSetHsv(float hue, float saturation, float value)
		{
			int i;
			float f, p, q, t;
			byte r, g, b;

			if (saturation == 0)
			{
				// achromatic (grey)
				r = g = b = ClampValue(value);
				return Color.FromArgb(r, g, b);
			}

			hue /= 60;              // Sector 0-5
			i = (int)Math.Floor(hue);
			f = hue - i;
			p = value * (1 - saturation);
			q = value * (1 - saturation * f);
			t = value * (1 - saturation * (1 - f));

			switch (i)
			{
			case 0:
					return Color.FromArgb(ClampValue(value), ClampValue(t), ClampValue(p));
			case 1:
					return Color.FromArgb(ClampValue(q), ClampValue(t), ClampValue(p));
			case 2:
					return Color.FromArgb(ClampValue(p), ClampValue(value), ClampValue(t));
			case 3:
				return Color.FromArgb(ClampValue(p), ClampValue(q), ClampValue(value));
			case 4:
				return Color.FromArgb(ClampValue(t), ClampValue(p), ClampValue(value));
			default:
				return Color.FromArgb(ClampValue(value), ClampValue(p), ClampValue(q));
			}
		}

		/// <summary>
		/// Returns "EnumType.EnumName"
		/// </summary>
		public static string GetEnumName<T>(T value)
		{
			var t = typeof(T);
			return t.Name + "." + t.GetEnumName(value);
		}

		static private Color m_previousRandomColor = new Color();
		static public Color GetRandomColor()
		{
			Color color;
			do
			{
				// These are modifiable
				int randomSeed = random.Next(100, 1000),
				scale = 2;

				// Core cache content
				int maxValue = 255,
				divider = maxValue / scale,
				saturation = divider;

				Random r = new Random(random.Next(randomSeed));
				color = Color.FromArgb(
					r.Next(maxValue / divider) * saturation,
					r.Next(maxValue / divider) * saturation,
					r.Next(maxValue / divider) * saturation
				);
			}
			while (color == m_previousRandomColor);

			m_previousRandomColor = color;

			return color;
		}


		static public int WrapIncrement(int current, int increment, int min, int max)
		{
			return (current + increment) % max;
		}

		/// <summary>
		/// This is useful for incrementing/decrementing through an enum. 
		/// </summary>
		/// <param name="current"></param>
		/// <param name="increment"></param>
		/// <param name="min">Can be an enum or an int</param>
		/// <param name="max"></param>
		/// <returns></returns>
		static public object WrapIncrement(object current, int increment, object min, object max)
		{
			return WrapIncrement((int)current, (int)increment, (int)min, (int)max);
		}
	}
}
