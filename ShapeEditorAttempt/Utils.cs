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


		static Random random = new Random();
		public static Color GetRandomColor()
		{
			return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));

		}
	}
}
