using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class SelectorTool : ToolBase
	{
		public static SelectorTool Instance = new SelectorTool();
		private Pen OutlinePen;
		private SelectorTool()
		{
			OutlinePen = new Pen(Brushes.Blue);
			OutlinePen.Width = 2;
			OutlinePen.Color = Color.FromArgb(128, OutlinePen.Color.R, OutlinePen.Color.G, OutlinePen.Color.B);
			OutlinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
		}

		public Rectangle selectedRectangle = new Rectangle();
		public Shape[] selectedShapes = null;

		Point mouseDownLocation = new Point();

		void UpdateSelectedRectangle(Point beginPoint, Point endPoint)
		{
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
		}

		public override void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (!MouseWasDown)
				mouseDownLocation = e.Location;

			UpdateSelectedRectangle(mouseDownLocation, e.Location);
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!MouseIsDown)
				return;

			UpdateSelectedRectangle(mouseDownLocation, e.Location);
			Canvas.Instance.Invalidate();
		}

		public override void OnMouseUp(object sender, MouseEventArgs e)
		{
			
		}

		public override void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			selectedRectangle = Rectangle.Empty;
			Canvas.Instance.Invalidate();
		}

		public override void OnPaint(object sender, PaintEventArgs e)
		{
			if (selectedRectangle.IsEmpty)
				return;

			Pen p = new Pen(Brushes.Blue);
			p.Width = 2;
			p.Color = Color.FromArgb(128, p.Color.R, p.Color.G, p.Color.B);
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

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
			selectedRectangle = Rectangle.Empty;
			selectedShapes = null;
		}
	}
}
