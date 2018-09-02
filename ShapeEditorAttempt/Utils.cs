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
		/*
		static public Point AddPoints(params Point[] points)
		{
			if (points.Length == 0)
				return Point.Empty;

			return points.Aggregate((source, next) =>
			{
				source.X += next.X;
				source.Y += next.Y;
				return source;
			});
		}

		static public Point SubtractPoints(params Point[] points)
		{
			if (points.Length == 0)
				return Point.Empty;

			return points.Aggregate((source, next) =>
			{
				source.X -= next.X;
				source.Y -= next.Y;
				return source;
			});
		}

		static public Size AddSizes(params Size[] sizes)
		{
			if (sizes.Length == 0)
				return Size.Empty;

			return sizes.Aggregate((source, next) =>
			{
				source.Width += next.Width;
				source.Height += next.Height;
				return source;
			});
		}

		static public Size SubtractSizes(params Size[] sizes)
		{
			if (sizes.Length == 0)
				return Size.Empty;

			return sizes.Aggregate((source, next) =>
			{
				source.Width -= next.Width;
				source.Height -= next.Height;
				return source;
			});
		}
		*/
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
