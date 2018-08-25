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

		private Color m_color;
		public Color color
		{
			get {
				return m_color;
			}
			set {
				m_color = value;
				pen = new Pen(m_color, 1);
			}
		}
		public Pen pen { get; private set; }

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
		public abstract ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point);


		public Rectangle PreviewOffset()
		{
			switch (Form1.clickedShapeAction)
			{
			case ShapeClickAction.Drag: return PreviewDragOffset();
			case ShapeClickAction.Resize: return PreviewResizeOffset();
			default: return position;
			}
		}

		public void ApplyOffset()
		{
			switch (Form1.clickedShapeAction)
			{
			case ShapeClickAction.Drag: ApplyDragOffset(); break;
			case ShapeClickAction.Resize: ApplyResizeOffset(); break;
			}
		}

		public void ApplyDragOffset()
		{
			position.X -= dragOffset.X;
			position.Y -= dragOffset.Y;
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
