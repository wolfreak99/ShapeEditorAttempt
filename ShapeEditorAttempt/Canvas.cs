using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class Canvas : PictureBox
	{
		public static List<Shape> ShapeCollection = new List<Shape>(new Shape[]{
			new Square(10, 20, 30, 30, Color.Blue),
			new Square(50, 60, 20, 10, Color.Red)
		});

		public Shapes NewShapeType = Shapes.Square;

		public Shape clickedShape = null;
		Point clickedOrigin = Point.Empty;
		public ShapeClickAction clickedShapeAction { get; private set; }

		internal void Canvas_Paint(object sender, PaintEventArgs e)
		{
			Grid.Draw(this, e);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			foreach (var s in ShapeCollection)
			{
				s.Draw(this, e.Graphics);
			}
		}

		internal void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (clickedShapeAction == ShapeClickAction.None)
				return;

			if (clickedShape != null)
			{
				clickedShape.ApplyOffset(clickedShapeAction);
			}

			// Reset click data
			clickedShape = null;
			clickedOrigin = Point.Empty;
			clickedShapeAction = ShapeClickAction.None;

			Invalidate();
		}

		internal void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			var location = e.Location;

			if (clickedShapeAction == ShapeClickAction.None)
				return;

			// Todo: Copy over the moving mechanics a little better.
			if (clickedShape == null)
				return;

			Point moveTo = new Point(
				clickedOrigin.X - Grid.SnapToGrid(location).X,
				clickedOrigin.Y - Grid.SnapToGrid(location).Y
			);

			clickedShape.UpdateOffset(clickedShapeAction, moveTo);
			Invalidate();
		}

		internal void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			// Only run during initial press
			if (clickedShapeAction != ShapeClickAction.None)
				return;

			GraphicsPath path = new GraphicsPath(FillMode.Alternate);
			var location = e.Location;
			clickedOrigin = Grid.SnapToGrid(e.Location);

			var s = GetShapeByPoint(path, location);
			switch (e.Button)
			{
			case MouseButtons.Right:
				if (s != null)
				{
					ShapeCollection.Remove(s);
					clickedShapeAction = ShapeClickAction.Delete;
				}
				break;
			case MouseButtons.Middle:
				if (s != null && s.GetShapeType() == Shapes.Triangle)
				{
					Triangle t = (Triangle)s;
					t.IncrementAngle();
				}
				break;
			case MouseButtons.Left:
				if (s != null)
				{
					var action = s.GetPointOverShapeAction(path, location);
					if (action != ShapeClickAction.None)
					{
						clickedShapeAction = action;
						clickedShape = s;
						Canvas_MouseMove(sender, e);
						break;
					}
				}
				if (clickedShapeAction == ShapeClickAction.None)
				{
					GenerateShape(20);
				}
				break;
			}

			Invalidate();
		}

		private Shape GetShapeByPoint(GraphicsPath path, Point point)
		{
			for (int i = ShapeCollection.Count - 1; i >= 0; i--)
			{
				var s = ShapeCollection[i];
				if (s.IsPointOverShape(path, point))
				{
					return s;
				}
			}
			return null;
		}

		private void GenerateShape(int size)
		{
			var sizeSnapped = Grid.SnapToGrid(new Size(size, size));
			Shape shape = ShapesHelper.CreateNewShape(
				clickedOrigin.X - (sizeSnapped.Width / 2),
				clickedOrigin.Y - (sizeSnapped.Height / 2),
				sizeSnapped.Width, sizeSnapped.Height, Utils.GetRandomColor(),
				NewShapeType
			);

			// No shapes were found, so create new shape.
			ShapeCollection.Add(shape);

			// Force new shape to go into resize mode.
			clickedShapeAction = ShapeClickAction.Resize;
			clickedShape = shape;

		}

		public void Canvas_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (NewShapeType == Shapes.Triangle)
			{
				NewShapeType = Shapes.Square;
			}
			else
			{
				NewShapeType++;
			}
		}

	}
}
