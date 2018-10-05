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
			BeginMoveSelectedShapes,
			EndMoveSelectedShapes,
			// ResizeSelectedShapes,
		}

		private SelectorAction m_action = SelectorAction.None;
		private SelectorAction action
		{
			get
			{
				return m_action;
			}
			set
			{
				if (m_action != value)
				{
					Debug.Log(string.Format("SelectorTools Action set from {0} to {1}", Utils.GetEnumName(m_action), Utils.GetEnumName(value)));
					m_action = value;
				}
			}
		}

		public override ToolType GetToolType()
		{
			return ToolType.SelectorTool;
		}

		private static Pen OutlinePen;

		private Rectangle selectedRectangle = new Rectangle();
		private Point selectedRectangleDownLocation = new Point();
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
			selectedRectangleDownLocation = new Point();
			mouseDownLocation = new Point();

			ClickData.Clear(true);
			action = SelectorAction.None;
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
				OnMouseDown_HandleDragSelection(sender, e, mouseLocation, snappedLocation, true);
				break;
			case SelectorAction.EndMoveSelectedShapes:
				OnMouseDown_HandleDragSelection(sender, e, mouseLocation, snappedLocation, false);
				break;
			case SelectorAction.None:
				// This should only get triggered on the initial mouse down,
				// But in case, just clear the selected rectangle info and MouseDown with BeginSelectionRectangle
				ClearSelectedRectangle();
				action = SelectorAction.BeginSelectionRectangle;

				OnMouseDown(sender, e);
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
				SetSelectedRectangle(mouseDownLocation, snappedLocation);
				break;
			case SelectorAction.BeginMoveSelectedShapes:
				Canvas.Instance.Focus();

				ClickData.Set(ShapeClickAction.Drag);
				ClickData.ShapeUpdateOffset(snappedLocation);
				
				Point moveTo = new Point(
					snappedLocation.X - mouseDownLocation.X,
					snappedLocation.Y - mouseDownLocation.Y
				);

				selectedRectangle.X = selectedRectangleDownLocation.X + moveTo.X;
				selectedRectangle.Y = selectedRectangleDownLocation.Y + moveTo.Y;
				break;
			default:
				throw EnumNotImplementedException.Throw(action, ExceptionMessages.MSG_NOT_YET_IMPLEMENTED);
			}

			Canvas.Instance.Invalidate();
		}

		public override void OnMouseUp(object sender, MouseEventArgs e)
		{
			var mouseLocation = e.Location;
			var snappedLocation = Grid.SnapToGrid(mouseLocation);

			switch (action)
			{
			case SelectorAction.BeginSelectionRectangle:
				// Redirect to MouseUp:EndSelectionRectangle
				action = SelectorAction.EndSelectionRectangle;
				OnMouseUp(sender, e);
				break;
			case SelectorAction.EndSelectionRectangle:
				SetSelectedRectangle(mouseDownLocation, mouseLocation);
				break;
			case SelectorAction.BeginMoveSelectedShapes:
				// Redirect to MouseUp:EndMoveSelectedShapes
				action = SelectorAction.EndMoveSelectedShapes;
				OnMouseUp(sender, e);
				break;
			case SelectorAction.EndMoveSelectedShapes:
				ClickData.ShapeApplyOffset();

				// Reset click data
				ClickData.Clear(false);
				selectedRectangleDownLocation = new Point();
				mouseDownLocation = new Point();

				break;
			case SelectorAction.None:
				break;
			default:
				throw EnumNotImplementedException.Throw(action, ExceptionMessages.MSG_NOT_YET_IMPLEMENTED);
			}

			Canvas.Instance.Invalidate();
		}

		private bool IsMouseOverSelectedRectangle(Point location)
		{
			return (!selectedRectangle.IsEmpty && selectedRectangle.OffsetBy(1, 1, -2, -2).Contains(location));
		}

		private void OnMouseDown_HandleDragSelection(object sender, MouseEventArgs e, Point mouseLocation, Point snappedLocation, bool setShapes)
		{
			if (IsMouseOverSelectedRectangle(mouseLocation))
			{
				if (setShapes)
				{
					var shapes = Canvas.Instance.GetShapesByRectangle(selectedRectangle);
					ClickData.Set(shapes);
				}
				mouseDownLocation = snappedLocation;

				ClickData.Set(snappedLocation, ShapeClickAction.Drag);

				selectedRectangleDownLocation = selectedRectangle.Location;
				action = SelectorAction.BeginMoveSelectedShapes;
				OnMouseMove(sender, e);
			}
			else
			{
				if (!MouseWasDown)
				{
					action = SelectorAction.BeginSelectionRectangle;

					mouseDownLocation = new Point();
					selectedRectangleDownLocation = new Point();
					ClickData.Clear(true);

					OnMouseDown(sender, e);
				}
			}
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
			if (action == SelectorAction.EndSelectionRectangle || action == SelectorAction.EndMoveSelectedShapes)
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
		}

		public override void OnUnloadTool()
		{
			ClearSelectedRectangle();
			ClickData.Clear(false);
			action = SelectorAction.None;
			if (OutlinePen != null)
			{
				OutlinePen.Dispose();
			}
		}
	}
}
