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
		public const string NAME = "Null";
		public const ShapeType TYPE = ShapeType.Null;
		public const int EDGE_WIDTH = 6;
		virtual public string Name { get { return NAME; } }
		virtual public ShapeType Type { get { return TYPE; } }
		virtual public int EdgeWidth { get { return EDGE_WIDTH; } }

		public Rectangle position;

		public Point clickActionOffset;

		private Color m_color;
		public Color color
		{
			get { return m_color; }
			set
			{
				m_color = value;

				if (m_pen != null)
				{
					m_pen.Dispose();
				}
				m_pen = new Pen(m_color, 1);
			}
		}
		
		internal Pen m_pen;
		/// <summary>
		/// The pen, which is updated whenever "color" is changed.
		/// </summary>
		public Pen pen
		{
			get
			{
				if (m_pen == null)
				{
					throw new NullReferenceException("shape.pen is null (Use shape.color instead)");
				}
				return m_pen;
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

		public virtual void Draw(Canvas sender, Graphics graphics)
		{
			Rectangle pos = (ClickData.Shape == this) ? PreviewOffset(position, ClickData.Action) : position;
			DrawShape(graphics, pos);

			// Create outline
			if (KeyboardController.IsControlDown && ClickData.Shape == this)
			{
				var prevWidth = m_pen.Width;
				var prevColor = color;

				color = Utils.ColorSetHsv(prevColor.GetHue(), prevColor.GetSaturation() + 10, prevColor.GetBrightness() + 10);
				m_pen.Width = EdgeWidth;

				Rectangle borderPos = position.InflatedBy(-EdgeWidth / 2, -EdgeWidth / 2);
				pos = (ClickData.Shape == this) ? PreviewOffset(borderPos, ClickData.Action) : borderPos;
				DrawBorder(graphics, pos);

				color = prevColor;
				m_pen.Width = prevWidth;
			}
		}

		public abstract void DrawShape(Graphics graphics, Rectangle position);
		public abstract void DrawBorder(Graphics graphics, Rectangle position);

		/// <summary>
		/// Helps determine if point is over shape or shape edge, and returns the appropriate action.
		/// </summary>
		public abstract ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point);
		public abstract bool IsPointOverShape(GraphicsPath path, Point point);

		public Rectangle PreviewOffset(Rectangle position, ShapeClickAction action)
		{
			//if (ClickData.Shape != this)
			//	return position;

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
			//if (ClickData.Shape != this)
			//	return;

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
			//if (ClickData.Shape != this)
			//	return;

			clickActionOffset = value;
		}
		
		public override string ToString()
		{
			return Name + "(" + position.ToString() + ", " + color.ToString() + ")";
		}
	}
}
