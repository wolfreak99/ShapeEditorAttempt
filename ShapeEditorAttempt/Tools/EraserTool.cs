using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class EraserTool : ToolBase
	{
		public EraserTool() : base()
		{

		}

		public override void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (MouseWasDown || (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right))
				return;

			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);
				var shape = Canvas.Instance.layer.GetShapeByPoint(path, location);
				
				SharedActions.RemoveShape(shape);
			}
			Canvas.Instance.Invalidate();
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);
				var shape = Canvas.Instance.layer.GetShapeByPoint(path, location);

				SharedActions.RemoveShape(shape);
			}
			Canvas.Instance.Invalidate();
		}

		public override void OnMouseUp(object sender, MouseEventArgs e)
		{

		}
	}
}
