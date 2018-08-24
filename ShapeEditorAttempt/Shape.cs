using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public abstract class Shape
	{
		public const int EDGE_WIDTH = 6;

		public Rectangle position;
		public Point dragOffset;
		public Size resizeOffset;
		public Color color;

		public Shape(int x, int y, int width, int height, Color color)
		{
			this.position = new Rectangle(x, y, width, height);
			this.color = color;
		}

		public Shape(Rectangle position, Color color)
		{
			this.position = position;
			this.color = color;
		}

		public abstract void Draw(Graphics graphics);

		/// <summary>
		/// Helps determine if point is over shape or shape edge, and returns the appropriate action.
		/// </summary>
		public abstract ShapeClickAction GetPointOverShapeAction(Point point);

		public void ApplyDragOffset()
		{
			position.Location = Utils.SubtractPoints(position.Location, dragOffset);
			dragOffset = Point.Empty;
		}
		
		protected Rectangle PreviewDragOffset()
		{
			Rectangle value = position;
			value.X -= dragOffset.X;
			value.Y -= dragOffset.Y;
			return value;
		}

		public void ApplyResizeOffset()
		{
			position.Width -= resizeOffset.Width;
			position.Height -= resizeOffset.Height;
			resizeOffset = Size.Empty;
		}

		public Rectangle PreviewResizeOffset()
		{
			Rectangle value = position;
			value.Width -= resizeOffset.Width;
			value.Height -= resizeOffset.Height;
			return value;
		}

		public override string ToString()
		{
			return GetShapeName() + "(" + position.ToString() + ", " + color.ToString() + ")";
		}

		/// <summary>
		/// Used inside ToString: returns the name of the shape.
		/// </summary>
		/// <returns>Name of shape (used by ToString)</returns>
		protected virtual string GetShapeName()
		{
			return this.GetType().Name;
		}

	}
}
