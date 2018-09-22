﻿using System;
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

		private SelectorAction action;

		private Pen OutlinePen;
		
		private Rectangle selectedRectangle = new Rectangle();
		private Shape[] selectedShapes = null;

		private Point mouseDownLocation = new Point();
		private Point mouseDownMovingLocation = new Point();

		public SelectorTool() : base()
		{
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

			selectedRectangle = newRect;
			selectedShapes = Canvas.Instance.GetShapesByRectangle(selectedRectangle);

			// Update borders of selected shapes
			var array = Canvas.Instance.layer.ToArray();
			foreach (var s in array)
			{
				s.BorderVisible = false;
			}
			foreach (var s in selectedShapes)
			{
				s.BorderVisible = true;
			}

			// This may be needed
			//Canvas.Instance.Invalidate();
		}

		private void ClearSelectedRectangle()
		{
			selectedRectangle = Rectangle.Empty;
			mouseDownLocation = Point.Empty;
			action = SelectorAction.None;
		}

		public override void OnMouseDown(object sender, MouseEventArgs e)
		{
			var mouseLocation = e.Location;
			switch (action)
			{
			case SelectorAction.EndSelectionRectangle:
				if (selectedRectangle.Contains(mouseLocation))
				{
					action = SelectorAction.MoveSelectedShapes;
				}
				break;
			case SelectorAction.None:
				if (!MouseWasDown)
				{
					action = SelectorAction.BeginSelectionRectangle;
					mouseDownLocation = mouseLocation;
					SetSelectedRectangle(mouseDownLocation, e.Location);
				}
				break;
			default:
				throw EnumNotImplementedException.Throw(action, ExceptionMessages.MSG_NOT_YET_IMPLEMENTED);
			}
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			switch (action)
			{
			case SelectorAction.BeginSelectionRectangle:
			case SelectorAction.EndSelectionRectangle:
				break;
			case SelectorAction.MoveSelectedShapes:
				break;
			}
			SetSelectedRectangle(mouseDownLocation, e.Location);
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
			case SelectorAction.EndSelectionRectangle:
				break;
			case SelectorAction.MoveSelectedShapes:
				break;
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

			Pen p = new Pen(Brushes.Blue);
			p.Width = 2;
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			p.Color = Color.FromArgb(32, p.Color);
			var inflated = Rectangle.Inflate(selectedRectangle, ((int)p.Width) * -1, ((int)p.Width) * -1);
			e.Graphics.FillRectangle(p.Brush, inflated);

			p.Color = Color.FromArgb(128, p.Color);
			e.Graphics.DrawRectangle(p, selectedRectangle);
		}

		public override void OnProcessKeys(KeyEventArgs e, bool isDown)
		{
			switch (e.KeyCode)
			{
			case Keys.Delete:
				if (selectedShapes != null)
				{
					foreach (var s in selectedShapes)
					{
						SharedActions.RemoveShape(s);
					}
				}
				break;
			}
		}

		public override void OnUnloadTool()
		{
			ClearSelectedRectangle();
			selectedShapes = null;
		}
	}
}
