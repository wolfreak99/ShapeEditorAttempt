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

		public const string NAME = "Shape";
		public const Shapes TYPE = Shapes.Null;


		public Rectangle position;

		public Point clickActionOffset;

		private Color m_color;
		public Color color
		{
			get
			{
				return m_color;
			}
			set
			{
				m_color = value;
				pen = new Pen(m_color, 1);
			}
		}

		private Pen m_pen;
		/// <summary>
		/// The pen, which is updated whenever "color" is changed.
		/// </summary>
		public Pen pen
		{
			get
			{
				if (m_pen == null)
				{
					throw new NullReferenceException("shape.pen is null because shape.color has not been set.");
				}
				return m_pen;
			}
			private set
			{
				m_pen = value;
			}
		}

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

		public abstract void Draw(Canvas sender, Graphics graphics);

		/// <summary>
		/// Helps determine if point is over shape or shape edge, and returns the appropriate action.
		/// </summary>
		public abstract ShapeClickAction GetPointOverShapeAction(GraphicsPath path, Point point);
		public abstract bool IsPointOverShape(GraphicsPath path, Point point);

		public Rectangle PreviewOffset(Rectangle position, ShapeClickAction action)
		{
			Rectangle value = position;
			switch (action)
			{
			case ShapeClickAction.Drag:
				value.X -= clickActionOffset.X;
				value.Y -= clickActionOffset.Y;
				break;
			case ShapeClickAction.Resize:
				value.Width -= clickActionOffset.X;
				value.Height -= clickActionOffset.Y;
				break;
			}
			return value;
		}

		public void ApplyOffset(ShapeClickAction action)
		{
			switch (action)
			{
			case ShapeClickAction.Drag:
				position.X -= clickActionOffset.X;
				position.Y -= clickActionOffset.Y;
				break;
			case ShapeClickAction.Resize:
				position.Width -= clickActionOffset.X;
				position.Height -= clickActionOffset.Y;
				break;
			}
		}

		public void UpdateOffset(ShapeClickAction action, Point value)
		{
			clickActionOffset = value;
		}
		
		public override string ToString()
		{
			return GetShapeName() + "(" + position.ToString() + ", " + color.ToString() + ")";
		}

		/// <summary>
		/// Used inside ToString: returns the name of the shape.
		/// </summary>
		/// <returns>Name of shape (used by ToString)</returns>
		public virtual string GetShapeName()
		{
			return NAME;
		}

		public virtual Shapes GetShapeType()
		{
			return TYPE;
		}
	}
}
