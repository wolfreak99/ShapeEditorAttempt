using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class EraserTool : ToolBase
	{
		public static EraserTool Instance = new EraserTool();
		private EraserTool() { }

		public override void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (MouseWasDown)
				return;

			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);
				var shape = Canvas.Instance.layer.GetShapeByPoint(path, location);

				if (e.Button == MouseButtons.Left)
				{
					SharedActions.RemoveShape(shape);
				}
			}
			Canvas.Instance.Invalidate();
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!MouseIsDown)
				return;

			using (GraphicsPath path = new GraphicsPath(FillMode.Alternate))
			{
				var location = e.Location;
				ClickData.Origin = Grid.SnapToGrid(e.Location);
				var shape = Canvas.Instance.layer.GetShapeByPoint(path, location);

				switch (e.Button)
				{
				case MouseButtons.Left:
					SharedActions.RemoveShape(shape);
					break;
				}
			}
			Canvas.Instance.Invalidate();
		}

		public override void OnMouseUp(object sender, MouseEventArgs e)
		{

		}
	}
}
