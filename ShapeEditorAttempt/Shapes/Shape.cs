using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ShapeEditorAttempt
{
	/// <summary>
	/// Base class for all shapes.
	/// </summary>
	[XmlInclude(typeof(Square)), XmlInclude(typeof(Circle)), XmlInclude(typeof(Triangle)), XmlInclude(typeof(ShapeType))]
	public abstract class Shape
	{
		public const string NAME = "Null";
		public const ShapeType TYPE = ShapeType.Null;
		public const int EDGE_WIDTH = 6;

		virtual public string Name { get { return NAME; } }
		virtual public ShapeType Type { get { return TYPE; } }
		virtual public int EdgeWidth { get { return EDGE_WIDTH; } }

		#region Properties
		public string Nickname = "";
		[XmlIgnore] internal Rectangle Position;

		/// <summary>
		/// Shortcode for Position.Location
		/// </summary>
		[XmlIgnore] public Point Location { get { return Position.Location; } set { Position.Location = value; } }
		/// <summary>
		/// Shortcode for Position.Size
		/// </summary>
		[XmlIgnore] public Size Size { get { return Position.Size; } set { Position.Size = value; } }

		public int X { get { return Position.X; } set { Position.X = value; } }
		public int Y { get { return Position.Y; } set { Position.Y = value; } }
		public int Width { get { return Position.Width; } set { Position.Width = value; } }
		public int Height { get { return Position.Height; } set { Position.Height = value; } }

		[XmlIgnore]
		public int Left { get { return Position.Left; } }
		public int Top { get { return Position.Top; } }
		public int Right { get { return Position.Right; } }
		public int Bottom { get { return Position.Bottom; } }

		internal protected static readonly Color DEFAULT_COLOR = Color.Black;
		private Color m_color;
		[XmlIgnore]
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

		/// <summary>
		/// Used for xml serialization
		/// </summary>
		public string XmlColor
		{
			get
			{
				return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", Color.A, Color.R, Color.G, Color.B);
			}
			set
			{
				string str = value;
				if (string.IsNullOrEmpty(str) || !str.StartsWith("#") || str.Length != 9)
					return;

				System.Globalization.NumberStyles styles = System.Globalization.NumberStyles.HexNumber;
				int a, r, g, b;
				if (!int.TryParse(str.Substring(1, 2), styles, null, out a)) a = 0xFF;
				if (!int.TryParse(str.Substring(3, 2), styles, null, out r)) r = 0x00;
				if (!int.TryParse(str.Substring(5, 2), styles, null, out g)) g = 0x00;
				if (!int.TryParse(str.Substring(7, 2), styles, null, out b)) b = 0x00;
				Color = Color.FromArgb(a, r, g, b);
			}
		}

		[XmlIgnore()]
		public Point clickActionOffset;

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

		[XmlIgnore] public bool BorderVisible;
		#endregion

		#region Constructors
		public Shape()
		{
			this.Position = new Rectangle();
			this.Color = DEFAULT_COLOR;
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
		#endregion

		#region Abstract functions
		public abstract void DrawShape(Graphics graphics, Rectangle position);
		public abstract void DrawBorder(Graphics graphics, Rectangle position);

		/// <summary>
		/// Helps determine if point is over shape or shape edge, and returns the appropriate action.
		/// </summary>
		public abstract ShapeClickAction GetShapeActionByPoint(GraphicsPath path, Point point);
		public abstract bool IsPointOverShape(GraphicsPath path, Point point);
		#endregion

		public bool ShapeIsSelected()
		{
			return ClickData.Shape == this || SelectorTool.ShapeIsSelected(this);
		}

		public void Draw(Canvas sender, Graphics graphics)
		{
			Rectangle pos = ShapeIsSelected() ? PreviewOffset(Position, ClickData.Action) : Position;
			DrawShape(graphics, pos);

			// Create outline
			if (!LoadSaveController.IsExportingImage && 
				(BorderVisible || (KeyboardController.IsControlDown && ShapeIsSelected())))
			{
				var prevWidth = m_pen.Width;
				var prevColor = Color;

				Color = Utils.ColorSetHsv(
					255 - prevColor.GetHue(), 
					255 - prevColor.GetSaturation(), 
					255 - prevColor.GetBrightness(),
					200
				);
				
				m_pen.Width = EdgeWidth;

				Rectangle borderPos = Position.InflatedBy(-EdgeWidth / 2, -EdgeWidth / 2);
				pos = ShapeIsSelected() ? PreviewOffset(borderPos, ClickData.Action) : borderPos;
				DrawBorder(graphics, pos);

				Color = prevColor;
				m_pen.Width = prevWidth;
			}
		}

		#region Offset functions
		public Rectangle PreviewOffset(Rectangle position, ShapeClickAction action)
		{
			if (!ShapeIsSelected())
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
			if (!ShapeIsSelected())
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
			if (!ShapeIsSelected())
			{
				throw new FieldAccessException("UpdateOffset attempted on unselected shape");
			}

			clickActionOffset = value;
		}
		#endregion

		public override string ToString()
		{
			return Name + "(" + Position.ToString() + ", " + Color.ToString() + ")";
		}
	}
}
