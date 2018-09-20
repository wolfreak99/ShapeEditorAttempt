using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class Canvas : PictureBox, IInitializeComponent
	{
		/// <summary>
		/// Shortkey to MainForm.Instance.Canvas
		/// </summary>
		public static Canvas Instance { get { return MainForm.Instance.Canvas; } }

		/// <summary>
		/// The layer containing the shapes.
		/// </summary>
		public Layer layer = new Layer();

		public ShapeType GetSelectedShapeType() { return MainForm.Instance.SelectedShapeWidget.Value; }
		public Color GetSelectedColor() { return MainForm.Instance.SelectedColorWidget.Value; }

		public Canvas() : base()
		{

		}

		public void InitializeComponent()
		{
			MainForm.Instance.gridSizeTextBox.Text = Grid.GridSize.Width.ToString();

			this.Paint += this.Canvas_Paint;
			this.MouseDown += this.Canvas_MouseDown;
			this.MouseMove += this.Canvas_MouseMove;
			this.MouseUp += this.Canvas_MouseUp;
			this.MouseDoubleClick += Canvas_MouseDoubleClick;
		}

		public void UninitializeComponent()
		{

		}
		
		internal void Canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

			Grid.Draw(this, e);
			layer.Draw(this, e);

			ToolBase.Current.Paint(sender, e);
		}

		private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ToolBase.Current.MouseDoubleClick(sender, e);
		}

		internal void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			ToolBase.Current.MouseUp(sender, e);
		}

		internal void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			ToolBase.Current.MouseMove(sender, e);
		}

		internal void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			ToolBase.Current.MouseDown(sender, e);
		}

		/// <summary>
		/// Clears the layers from the canvas and invalidates it
		/// </summary>
		public void Clear()
		{
			layer.Clear();
			Invalidate();
		}

		internal void SetShapeType(Shape shape, ShapeType value)
		{
			if (shape == null)
				return;

			if (shape.Type != value)
			{
				Shape newShape;

				switch (value)
				{
				case ShapeType.Square:
					newShape = new Square(shape.X, shape.Y, shape.Width, shape.Height, shape.Color);
					break;
				case ShapeType.Circle:
					newShape = new Circle(shape.X, shape.Y, shape.Width, shape.Height, shape.Color);
					break;
				case ShapeType.Triangle:
					newShape = new Triangle(shape.X, shape.Y, shape.Width, shape.Height, shape.Color);
					break;
				default:
					throw new EnumNotSupportedException(value);
				}

				layer.Replace(shape, newShape);
				Invalidate();
			}
		}

		internal void SetShapeColor(Shape shape, Color color)
		{
			if (shape == null)
				return;

			shape.Color = color;
			Invalidate();
		}

		internal Shape[] GetShapesByRectangle(Rectangle rectangle)
		{
			return layer.GetShapesByRectangle(rectangle);
		}
	}
}
