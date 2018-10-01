using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class SelectorTool : ToolBase
	{
		private enum SelectorAction
		{
			None = -1,
			BeginSelectionRectangle,
			EndSelectionRectangle,
			MoveSelectedShapes,
			// ResizeSelectedShapes,
		}
		
		private SelectorAction action = SelectorAction.None;

		public override ToolType GetToolType()
		{
			return ToolType.SelectorTool;
		}

		private static Pen OutlinePen;

		private Rectangle selectedRectangle = new Rectangle();
		private Point mouseDownLocation = new Point();

		public SelectorTool() : base()
		{
			if (OutlinePen != null)
			{
				OutlinePen.Dispose();
			}
			OutlinePen = new Pen(Brushes.Blue);
			OutlinePen.Width = 2;
			OutlinePen.Color = Color.FromArgb(128, OutlinePen.Color.R, OutlinePen.Color.G, OutlinePen.Color.B);
			OutlinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
		}

		private void SetSelectedRectangle(Point beginPoint, Point endPoint)
		{
			// Make sure x/y are top-left, and width/height is bottom-right
			Point a = Utils.Min(beginPoint, endPoint);
			Point b = Utils.Max(beginPoint, endPoint);

			var newRect = Rectangle.FromLTRB(a.X, a.Y, b.X, b.Y);

			// Determine if there is a need to update shapes
			if (selectedRectangle == newRect)
				return;
			
			// Update selected rectangle
			selectedRectangle = newRect;

			// Update selected shapes
			var shapes = Canvas.Instance.GetShapesByRectangle(selectedRectangle);
			ClickData.Clear(true);
			ClickData.Set(shapes);

			// Update borders of selected shapes
			// This may be needed
			Canvas.Instance.Invalidate();
		}

		private void ClearSelectedRectangle()
		{
			selectedRectangle = new Rectangle();
			mouseDownLocation = new Point();
			action = SelectorAction.None;
			ClickData.ClearShapes();
		}

		public override void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			var mouseLocation = e.Location;
			var snappedLocation = Grid.SnapToGrid(mouseLocation);

			switch (action)
			{
			case SelectorAction.BeginSelectionRectangle:
				mouseDownLocation = mouseLocation;
				//ClickData.Set(mouseDownLocation, ShapeClickAction.Drag);
				SetSelectedRectangle(mouseDownLocation, mouseLocation);
				break;
			case SelectorAction.EndSelectionRectangle:
				if (!selectedRectangle.IsEmpty && selectedRectangle.Contains(mouseLocation))
				{
					action = SelectorAction.MoveSelectedShapes;
					ClickData.Set(snappedLocation, ShapeClickAction.Drag);
					OnMouseMove(sender, e);
				}
				else
				{
					action = SelectorAction.BeginSelectionRectangle;
					OnMouseDown(sender, e);
				}
				break;
			case SelectorAction.None:
				if (!selectedRectangle.IsEmpty && selectedRectangle.Contains(mouseLocation))
				{
					var shapes = Canvas.Instance.GetShapesByRectangle(selectedRectangle);
					ClickData.Set(shapes);

					action = SelectorAction.MoveSelectedShapes;
					ClickData.Set(Grid.SnapToGrid(mouseLocation), ShapeClickAction.Drag);
					OnMouseMove(sender, e);
				}
				else
				{
					if (!MouseWasDown)
					{
						action = SelectorAction.BeginSelectionRectangle;
						OnMouseDown(sender, e);
					}
				}
				break;
			default:
				throw EnumNotImplementedException.Throw(action, ExceptionMessages.MSG_NOT_YET_IMPLEMENTED);
			}

			Canvas.Instance.Invalidate();
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			var mouseLocation = e.Location;
			var snappedLocation = Grid.SnapToGrid(mouseLocation);

			switch (action)
			{
			case SelectorAction.BeginSelectionRectangle:
			case SelectorAction.EndSelectionRectangle:
				SetSelectedRectangle(mouseDownLocation, mouseLocation);
				break;
			case SelectorAction.MoveSelectedShapes:
			{
				Canvas.Instance.Focus();
				ClickData.ShapeUpdateOffset(snappedLocation);
				ClickData.Set(ShapeClickAction.Drag);
				Point moveTo = new Point(
					mouseDownLocation.X - snappedLocation.X,
					mouseDownLocation.Y - snappedLocation.Y
				);
				selectedRectangle.X = moveTo.X;
				selectedRectangle.Y = moveTo.Y;
				break;
			}
			default:
				throw EnumNotImplementedException.Throw(action, ExceptionMessages.MSG_NOT_YET_IMPLEMENTED);
			}

			Canvas.Instance.Invalidate();
		}

		public override void OnMouseUp(object sender, MouseEventArgs e)
		{
			var mouseLocation = e.Location;

			switch (action)
			{
			case SelectorAction.BeginSelectionRectangle:
				SetSelectedRectangle(mouseDownLocation, mouseLocation);
				action = SelectorAction.EndSelectionRectangle;
				break;
			case SelectorAction.MoveSelectedShapes:
				ClickData.ShapeApplyOffset();

				// Reset click data
				ClickData.Clear(false);
				action = SelectorAction.None;
				Canvas.Instance.Invalidate();
				break;
			case SelectorAction.None:
				break;
			default:
				throw EnumNotImplementedException.Throw(action, ExceptionMessages.MSG_NOT_YET_IMPLEMENTED);
			}

			Canvas.Instance.Invalidate();
		}

		public override void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			ClearSelectedRectangle();

			Canvas.Instance.Invalidate();
		}

		public override void OnPaint(object sender, PaintEventArgs e)
		{
			if (selectedRectangle.IsEmpty)
				return;

			var inflated = Rectangle.Inflate(
				selectedRectangle,
				((int)OutlinePen.Width) * -1,
				((int)OutlinePen.Width) * -1
			);

			OutlinePen.Color = Color.FromArgb(32, OutlinePen.Color);
			e.Graphics.FillRectangle(OutlinePen.Brush, inflated);

			OutlinePen.Color = Color.FromArgb(128, OutlinePen.Color);
			e.Graphics.DrawRectangle(OutlinePen, selectedRectangle);
		}

		public override void OnProcessKeys(KeyEventArgs e, bool isDown)
		{
			switch (e.KeyCode)
			{
			case Keys.Delete:
				foreach (var s in ClickData.Shapes)
				{
					SharedActions.RemoveShape(s);
				}
				break;
			}
		}

		public override void OnUnloadTool()
		{
			ClearSelectedRectangle();
			ClickData.Clear();
			if (OutlinePen != null)
			{
				OutlinePen.Dispose();
			}
		}
	}
}
