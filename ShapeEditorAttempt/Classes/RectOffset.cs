
using System;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class RectOffset
	{
		public int Left, Top, Right, Bottom;
		
		public RectOffset(int left, int top, int right, int bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}

		/// <summary>
		/// Right + Left
		/// </summary>
		public int AdditiveWidth
		{
			get { return Right + Left; }
		}
		
		/// <summary>
		/// Bottom + Top
		/// </summary>
		public int AdditiveHeight
		{
			get { return Bottom + Top; }
		}

		/// <summary>
		/// Right - Left
		/// </summary>
		public int Width
		{
			get { return Right - Left; }
		}

		/// <summary>
		/// Bottom - Top
		/// </summary>
		public int Height
		{
			get { return Bottom - Top; }
		}
		
		/// <summary>
		/// return new Rectangle(Left, Top, Right + Left, Bottom + Top);
		/// </summary>
		public Rectangle GetRectangleAdditive()
		{
			return new Rectangle(Left, Top, AdditiveWidth, AdditiveHeight);
		}

		/// <summary>
		/// return new Rectangle(Left, Top, Right - Left, Bottom - Top);
		/// </summary>
		public Rectangle GetRectangle()
		{
			return new Rectangle(Left, Top, Width, Height);
		}
		
		/// <summary>
		/// return Rectangle.FromLTRB(Left, Top, Right, Bottom);
		/// </summary>
		public Rectangle GetRectangleFromLTRB()
		{
			return Rectangle.FromLTRB(Left, Top, Right, Bottom);
		}

	}
}