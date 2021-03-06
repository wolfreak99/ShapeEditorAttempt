﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class MainTool : ToolBase
	{
		public override ToolType GetToolType()
		{
			return ToolType.MainTool;
		}

		public MainTool() : base()
		{

		}

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
					SharedActions.TriangleIncrementAngle(shape);
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
					SharedActions.RemoveShape(shape);
					break;
				case MouseButtons.Middle:
					if (shape != null && shape.Type == ShapeType.Triangle)
					{
						SharedActions.TriangleIncrementAngle(shape);
					}
					break;
				case MouseButtons.Left:
					bool createShape = true;
					if (!KeyboardController.IsShiftDown && shape != null)
					{
						var action = shape.GetShapeActionByPoint(path, location);
						if (action != ShapeClickAction.None)
						{
							ClickData.Set(action, shape);
							createShape = false;
							OnMouseMove(sender, e);
						}
						else
						{
							throw new InvalidOperationException(
								"Shape was found under Point, but action wasn't - This shouldn't happen."
							);
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
			if (e.Button != MouseButtons.Left)
				return;

			// Needed for KeyboardController
			Canvas.Instance.Focus();

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

		public override void OnProcessKeys(KeyEventArgs e, bool isDown)
		{
			switch (e.KeyCode)
			{
			case Keys.Delete:
				if (!isDown && !ClickData.IsShapesEmpty() && Canvas.Instance.Focused)
				{
					foreach (Shape s in ClickData.Shapes)
					{
						SharedActions.RemoveShape(s);
					}
				}
				break;
			}
		}

		private void GenerateShape(Size size)
		{
			var layer = Canvas.Instance.layer;

			// Generate shape based on either a duplicate or a new shape
			Shape shape;
			if (!ClickData.IsShapesEmpty() && KeyboardController.IsControlDown)
			{
				shape = layer.DuplicateShape(ClickData.Shapes[0], ClickData.Origin);
			}
			else
			{
				shape = layer.AddNewShape(ClickData.Origin, size, 
					Canvas.Instance.GetSelectedColor(),
					Canvas.Instance.GetSelectedShapeType()
				);
			}

			// Force new shape to go into resize mode.
			ClickData.Set(ShapeClickAction.Resize, shape);
		}
	}
}
