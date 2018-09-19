using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

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

		private ShapeType GetSelectedShapeType() { return MainForm.Instance.SelectedShapeWidget.Value; }
		private Color GetSelectedColor() { return MainForm.Instance.SelectedColorWidget.Value; }

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

		public void UninitializeComponent() { }
		
		internal void Canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

			Grid.Draw(this, e);
			layer.Draw(this, e);
		}

		private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);

				// Todo: Copy over the moving mechanics a little better.
				var shape = layer.GetShapeByPoint(path, location);
				if (shape == null)
					return;

				if (shape.Type == ShapeType.Triangle)
				{
					Action_TriangleIncrmentAngle(shape);
				}
			}
		}

		internal void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (ClickData.Action == ShapeClickAction.None)
				return;

			ClickData.ShapeApplyOffset();

			// Reset click data
			ClickData.Clear(false);

			Invalidate();
		}

		internal void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				return;

			Focus();
			// Todo: Copy over the moving mechanics a little better.
			ClickData.ShapeUpdateOffset(e.Location);

			Invalidate();
		}

		internal void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			// Only run during initial press
			if (ClickData.Action != ShapeClickAction.None)
				return;

			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);
				var shape = layer.GetShapeByPoint(path, location);

				switch (e.Button)
				{
				case MouseButtons.Right:
					Action_RemoveShape(shape);
					break;
				case MouseButtons.Middle:
					if (shape != null && shape.Type == ShapeType.Triangle)
					{
						Action_TriangleIncrmentAngle(shape);
					}
					break;
				case MouseButtons.Left:
					bool createShape = true;
					if (!KeyboardController.IsShiftDown && shape != null)
					{
						var action = shape.GetShapeActionByPoint(path, location);
						if (action != ShapeClickAction.None)
						{
							ClickData.Set(shape, action);
							createShape = false;
							Canvas_MouseMove(sender, e);
						}
						else
						{
							throw new Exception("Shape was found under Point, but action wasn't" +
								" - This shouldn't happen.");
						}
					}
					if (createShape)
					{
						var newSize = new Size(20, 20);
						var sizeSnapped = Grid.SnapToGrid(newSize, Grid.SnapSizeToGrid);
						GenerateShape(sizeSnapped);
					}
					break;
				}
			}
			Invalidate();
		}

		public void Action_RemoveShape(Shape shape)
		{
			if (shape != null)
			{
				layer.Remove(shape);
				ClickData.Action = ShapeClickAction.Delete;
			}
			ClickData.Shape = null;
		}

		public void Action_TriangleIncrmentAngle(Shape shape)
		{
			if (shape.Type != ShapeType.Triangle)
				return;

			Triangle t = (Triangle)shape;
			t.IncrementAngle();
		}

		private void GenerateShape(Size size)
		{
			Shape shape;
			if (ClickData.Shape != null && KeyboardController.IsControlDown)
			{
				shape = layer.DuplicateShape(ClickData.Shape, ClickData.Origin);
			}
			else
			{
				shape = layer.AddNewShape(ClickData.Origin, size, GetSelectedColor(), GetSelectedShapeType());
			}
			// Force new shape to go into resize mode.
			ClickData.Set(shape, ShapeClickAction.Resize);
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
			shape.Color = color;
			Invalidate();
		}
	}
}
