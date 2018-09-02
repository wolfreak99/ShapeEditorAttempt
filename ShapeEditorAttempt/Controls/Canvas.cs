using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class Canvas : PictureBox, IInitializeComponent
	{
		static public List<Shape> ShapeCollection = new List<Shape>(new Shape[]{
			new Square(10, 20, 30, 30, Color.Blue),
			new Square(50, 60, 20, 10, Color.Red)
		});

		public Shape clickedShape = null;
		Point clickedOrigin = Point.Empty;
		public ShapeClickAction clickedShapeAction { get; private set; }

		private SelectedShapeWidget selectedShapeWidget;
		private SelectedColorWidget selectedColorWidget;

		public MainForm ParentMainForm { get; set; }

		public Canvas() : base()
		{
		}

		public void InitializeComponent(MainForm parentMainForm)
		{
			this.Paint += this.Canvas_Paint;
			this.MouseDown += this.Canvas_MouseDown;
			this.MouseMove += this.Canvas_MouseMove;
			this.MouseUp += this.Canvas_MouseUp;
			this.MouseDoubleClick += Canvas_MouseDoubleClick;

			ParentMainForm = parentMainForm;

			selectedShapeWidget = parentMainForm.selectedShapeWidget;
			selectedColorWidget = parentMainForm.selectedColorWidget;
		}

		public void UninitializeComponent() { }

		private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				clickedOrigin = Grid.SnapToGrid(e.Location);

				var shape = GetShapeByPoint(path, location);

				// Todo: Copy over the moving mechanics a little better.
				if (shape == null)
					return;

				var shapeType = shape.GetShapeType();

				if (shapeType == Shapes.Triangle)
				{
					Triangle t = (Triangle)shape;
					t.IncrementAngle();
					// Trigger a resize
					ParentMainForm.Invalidate(ClientRectangle);
				}
			}

		}

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
			if (e.Button == MouseButtons.None) return;

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

			var shape = GetShapeByPoint(path, location);
			switch (e.Button)
			{
			case MouseButtons.Right:
				if (shape != null)
				{
					ShapeCollection.Remove(shape);
					clickedShapeAction = ShapeClickAction.Delete;
				}
				break;
			case MouseButtons.Middle:
				if (shape != null && shape.GetShapeType() == Shapes.Triangle)
				{
					Triangle t = (Triangle)shape;
					t.IncrementAngle();
				}
				break;
			case MouseButtons.Left:
				if (shape != null)
				{
					var action = shape.GetPointOverShapeAction(path, location);
					if (action != ShapeClickAction.None)
					{
						clickedShapeAction = action;
						clickedShape = shape;
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
			foreach (Shape shape in ShapeCollection)
			{
				if (shape.IsPointOverShape(path, point))
				{
					return shape;
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
				sizeSnapped.Width, sizeSnapped.Height, 
				selectedColorWidget.SelectedColor,
				selectedShapeWidget.SelectedShape
			);
			
			// Insert at top of list.
			ShapeCollection.Insert(0, shape);

			// Force new shape to go into resize mode.
			clickedShapeAction = ShapeClickAction.Resize;
			clickedShape = shape;
		}
	}
}
