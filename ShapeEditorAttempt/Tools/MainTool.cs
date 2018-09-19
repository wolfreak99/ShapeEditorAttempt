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
	public class MainTool : ToolBase
	{
		public static MainTool Instance = new MainTool();
		private MainTool() { }

		public override void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);

				// Todo: Copy over the moving mechanics a little better.
				var shape = Canvas.Instance.layer.GetShapeByPoint(path, location);
				if (shape == null)
					return;

				if (shape.Type == ShapeType.Triangle)
				{
					Action_TriangleIncrmentAngle(shape);
				}
			}
		}

		public override void OnMouseDown(object sender, MouseEventArgs e)
		{
			// Only run during initial press
			if (ClickData.Action != ShapeClickAction.None)
				return;

			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);
				var shape = Canvas.Instance.layer.GetShapeByPoint(path, location);

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
							OnMouseMove(sender, e);
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
			Canvas.Instance.Invalidate();

		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
				return;

			// Focus();		// Not sure if this is needed or not

			// Todo: Copy over the moving mechanics a little better.
			ClickData.ShapeUpdateOffset(e.Location);

			Canvas.Instance.Invalidate();
		}

		public override void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (ClickData.Action == ShapeClickAction.None)
				return;

			ClickData.ShapeApplyOffset();

			// Reset click data
			ClickData.Clear(false);

			Canvas.Instance.Invalidate();
		}

		private void Action_TriangleIncrmentAngle(Shape shape)
		{
			if (shape.Type == ShapeType.Triangle)
			{
				Triangle t = (Triangle)shape;
				t.IncrementAngle();
			}
			Canvas.Instance.Invalidate();
		}
		
		public void Action_RemoveShape(Shape shape)
		{
			if (shape != null)
			{
				Canvas.Instance.layer.Remove(shape);
				ClickData.Action = ShapeClickAction.Delete;
			}
			ClickData.Shape = null;
		}
		
		private void GenerateShape(Size size)
		{
			var layer = Canvas.Instance.layer;

			Shape shape;
			if (ClickData.Shape != null && KeyboardController.IsControlDown)
			{
				shape = layer.DuplicateShape(ClickData.Shape, ClickData.Origin);
			}
			else
			{
				shape = layer.AddNewShape(ClickData.Origin, size, 
					Canvas.Instance.GetSelectedColor(),
					Canvas.Instance.GetSelectedShapeType());
			}

			// Force new shape to go into resize mode.
			ClickData.Set(shape, ShapeClickAction.Resize);
		}
	}
}
