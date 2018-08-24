using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public enum ShapeClickAction
	{
		None,
		Resize,
		Drag,
		Delete,		// Todo
		// Duplicate	// Todo
	}

	public partial class Form1 : Form
	{
		public static List<Shape> Shapes = new List<Shape>(new Shape[]{new Square(10, 20, 30, 30, Color.Blue), new Square(50, 60, 20, 10, Color.Red)});

		Shape clickedShape = null;
		Point clickedOrigin = Point.Empty;
		public static ShapeClickAction clickedShapeAction { get; private set; }

		public Form1()
		{
			InitializeComponent();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			Grid.Draw(Canvas, e);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			foreach (var s in Shapes)
			{
				s.Draw(e.Graphics);
			}
		}

		private void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (clickedShapeAction == ShapeClickAction.None)
				return;

			if (clickedShape != null)
			{
				if (clickedShapeAction == ShapeClickAction.Drag)
				{
					clickedShape.ApplyDragOffset();
				}
				else if (clickedShapeAction == ShapeClickAction.Resize)
				{
					clickedShape.ApplyResizeOffset();
				}
				clickedShape = null;
				clickedOrigin = Point.Empty;
			}

			clickedShapeAction = ShapeClickAction.None;

			Canvas.Invalidate();
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (clickedShapeAction == ShapeClickAction.None)
				return;

			// Todo: Copy over the moving mechanics a little better.
			if (Grid.SnapToGrid(e.Location) == Grid.SnapToGrid(clickedOrigin) || clickedShape == null)
				return;

			Point moveTo = Point.Empty;
			moveTo.X = clickedOrigin.X - Grid.SnapToGrid(e.Location).X;
			moveTo.Y = clickedOrigin.Y - Grid.SnapToGrid(e.Location).Y;

			if (moveTo == Point.Empty)
				return;

			if (clickedShapeAction == ShapeClickAction.Drag)
			{
				clickedShape.dragOffset = moveTo;
			}
			else if (clickedShapeAction == ShapeClickAction.Resize)
			{
				clickedShape.resizeOffset = (Size)moveTo;
			}

			Canvas.Invalidate();
		}

		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			// Only run during initial press
			if (clickedShapeAction != ShapeClickAction.None)
				return;

			// Check if right button first..
			if (e.Button == MouseButtons.Right)
			{
				
				for (int i = Shapes.Count - 1; i >= 0; i--)
				{
					var s = Shapes[i];
					var action = s.GetPointOverShapeAction(e.Location);
					if (action != ShapeClickAction.None)
					{
						Shapes.Remove(s);
						clickedShapeAction = ShapeClickAction.Delete;
						return;
					}
				}
				return;
			}

			clickedOrigin = Grid.SnapToGrid(e.Location);
			foreach (Shape s in Shapes)
			{
				var action = s.GetPointOverShapeAction(e.Location);
				if (action != ShapeClickAction.None)
				{
					clickedShapeAction = action;
					clickedShape = s;
					return;
				}
			}

			var size = Grid.SnapToGrid(20);
			var newShape = new Square(clickedOrigin.X - (size / 2), clickedOrigin.Y - (size / 2), size, size, Utils.GetRandomColor());
			Shapes.Add(newShape);
			// Force new shape to go into resize mode.
			clickedShapeAction = ShapeClickAction.Resize;
			clickedShape = newShape;

			Canvas.Invalidate();

		}

	}
}
