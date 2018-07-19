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

			Point result = points[0];
			for (int i = 1; i < points.Length; i++)
			{
				if (points[i].IsEmpty)
					continue;

				result.X += points[i].X;
				result.Y += points[i].Y;
			}

			return result;
		}
		public static Point SubtractPoints(params Point[] points)
		{
			if (points.Length == 0)
				return Point.Empty;

			Point result = points[0];
			for (int i = 1; i < points.Length; i++)
			{
				if (points[i].IsEmpty)
					continue;

				result.X -= points[i].X;
				result.Y -= points[i].Y;
			}

			return result;
		}
	}
}
