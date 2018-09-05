using System;
using System.Drawing;

namespace ShapeEditorAttempt
{
	static public class Extensions
	{
		/// <summary>
		///     Creates and returns an enlarged copy of the specified System.Drawing.Rectangle
		///     structure. The copy is enlarged by the specified amount. The original System.Drawing.Rectangle
		///     structure remains unmodified.
		/// </summary>
		/// <param name="rect">The System.Drawing.Rectangle with which to start. This rectangle is not modified.</param>
		/// <param name="x">The amount to inflate this System.Drawing.Rectangle horizontally.</param>
		/// <param name="y">The amount to inflate this System.Drawing.Rectangle vertically.</param>
		/// <returns>The enlarged System.Drawing.Rectangle.</returns>
		static public Rectangle InflatedBy(this Rectangle rect, int x, int y)
		{
			return Rectangle.Inflate(rect, x, y);
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
	}
}
