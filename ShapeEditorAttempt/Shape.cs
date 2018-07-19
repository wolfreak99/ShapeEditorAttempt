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
		public Rectangle position;
		public Point moveOffset;
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

		public abstract bool IsPointOverShape(Point point);

		public void ApplyMoveOffset()
		{
			position.Location = Utils.SubtractPoints(position.Location, moveOffset);
			moveOffset = Point.Empty;
		}
		
		protected Rectangle PreviewMoveOffset()
		{
			Rectangle value = position;
			value.X -= moveOffset.X;
			value.Y -= moveOffset.Y;
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
