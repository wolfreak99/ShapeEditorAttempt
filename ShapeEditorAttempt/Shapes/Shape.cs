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

		public Rectangle Position;
		/// <summary>
		/// Shortcode for Position.Location
		/// </summary>
		public Point Location { get { return Position.Location; } set { Position.Location = value; } }
		/// <summary>
		/// Shortcode for Position.Size
		/// </summary>
		public Size Size { get { return Position.Size; } set { Position.Size = value; } }
		public Point clickActionOffset;

		private Color m_color;
		public Color Color
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
		/// The Pen, which is updated whenever "Color" is changed.
		/// </summary>
		public Pen Pen
		{
			get
			{
				if (m_pen == null)
				{
					throw new NullReferenceException("shape.Pen is null (Have you set shape.Color yet?)");
				}
				return m_pen;
			}
		}

		public Shape(int x, int y, int width, int height, Color color)
		{
			this.Position = new Rectangle(x, y, width, height);
			this.Color = color;
		}

		public Shape(Rectangle position, Color color)
		{
			this.Position = position;
			this.Color = color;
		}

		public virtual void Draw(Canvas sender, Graphics graphics)
		{
			Rectangle pos = (ClickData.Shape == this) ? PreviewOffset(Position, ClickData.Action) : Position;
			DrawShape(graphics, pos);

			// Create outline
			if (KeyboardController.IsControlDown && ClickData.Shape == this)
			{
				var prevWidth = m_pen.Width;
				var prevColor = Color;

				Color = Utils.ColorSetHsv(prevColor.GetHue(), prevColor.GetSaturation() + 10, prevColor.GetBrightness() + 10);
				m_pen.Width = EdgeWidth;

				Rectangle borderPos = Position.InflatedBy(-EdgeWidth / 2, -EdgeWidth / 2);
				pos = (ClickData.Shape == this) ? PreviewOffset(borderPos, ClickData.Action) : borderPos;
				DrawBorder(graphics, pos);

				Color = prevColor;
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
			if (ClickData.Shape != this)
			{
				throw new FieldAccessException("PreviewOffset attempted on unselected shape");
			}

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
			if (ClickData.Shape != this)
			{
				throw new FieldAccessException("ApplyOffset attempted on unselected shape");
			}

			Rectangle value = Position;
			switch (action)
			{
			case ShapeClickAction.Drag:
				Position.X -= clickActionOffset.X;
				Position.Y -= clickActionOffset.Y;
				break;
			case ShapeClickAction.Resize:
				Position.Width -= clickActionOffset.X;
				Position.Height -= clickActionOffset.Y;
				break;
			}
		}

		public void UpdateOffset(ShapeClickAction action, Point value)
		{
			if (ClickData.Shape != this)
			{
				throw new FieldAccessException("UpdateOffset attempted on unselected shape");
			}

			clickActionOffset = value;
		}
		
		public override string ToString()
		{
			return Name + "(" + Position.ToString() + ", " + Color.ToString() + ")";
		}
	}
}
