using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeEditorAttempt
{
	static public class Extensions
	{
		/// <summary>
		///     Creates and returns an enlarged copy of the specified Rectangle
		///     structure. The copy is enlarged by the specified amount. The original Rectangle
		///     structure remains unmodified.
		/// </summary>
		/// <param name="rect">The Rectangle with which to start. This rectangle is not modified.</param>
		/// <param name="x">The amount to inflate this Rectangle horizontally.</param>
		/// <param name="y">The amount to inflate this Rectangle vertically.</param>
		/// <returns>The enlarged Rectangle.</returns>
		static public Rectangle InflatedBy(this Rectangle rect, int x, int y)
		{
			return Rectangle.Inflate(rect, x, y);
		}

		/// <summary>
		///     Creates and returns an enlarged copy of the specified Rectangle
		///     structure. The copy is enlarged by the specified amount. The original Rectangle
		///     structure remains unmodified.
		/// </summary>
		/// <param name="rect">The Rectangle with which to start. This rectangle is not modified.</param>
		/// <param name="x_y">The amount to inflate this Rectangle horizontally and vertically.</param>
		/// <returns>The enlarged Rectangle.</returns>
		static public Rectangle InflatedBy(this Rectangle rect, int x_y)
		{
			return InflatedBy(rect, x_y, x_y);
		}

		/// <summary>
		/// Returns a copy of the rect passed, with all x, y, width, and height values added to the respective rect values
		/// </summary>
		public static Rectangle OffsetBy(this Rectangle rect, int x, int y, int width, int height)
		{
			var rtn = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
			rtn.X += x;
			rtn.Y += y;
			rtn.Width += width;
			rtn.Height += height;
			return rtn;
		}

		/// <summary>
		/// Returns rectangle.X + (rectangle.Width / 2);
		/// </summary>
		/// <param name="rectangle"></param>
		/// <returns></returns>
		static public Point Center(this Rectangle rectangle)
		{
			return new Point(rectangle.X + (rectangle.Width / 2), rectangle.Y + (rectangle.Height / 2));
		}

		public static void Swap<T>(this List<T> items, int index, int newIndex)
		{
			if (items != null && index != newIndex &&
				index >= 0 && newIndex >= 0 && index < items.Count && newIndex < items.Count)
			{
				T tmp = items[newIndex];
				items[newIndex] = items[index];
				items[index] = tmp;
			}
		}
	}
}
