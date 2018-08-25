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

		public static Point AddPoints(params Point[] points)
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

		public static Point SubtractPoints(params Point[] points)
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

		public static Size AddSizes(params Size[] sizes)
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

		public static Size SubtractSizes(params Size[] sizes)
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

		public static Color GetRandomColor()
		{
			// These are modifiable
			int randomSeed = random.Next(100, 1000),
				scale = 2;

			// Core cache content
			int maxValue = 255,
				divider = maxValue / scale,
				saturation = divider;

			Random r = new Random(random.Next(randomSeed));
			return Color.FromArgb(
				r.Next(maxValue / divider) * saturation,
				r.Next(maxValue / divider) * saturation,
				r.Next(maxValue / divider) * saturation
			);
		}
	}
}
