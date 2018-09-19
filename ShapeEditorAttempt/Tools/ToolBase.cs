using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public abstract class ToolBase
	{
		public static ToolBase Current = MainTool.Instance;

		public bool MouseIsDown { get; private set; }
		public bool MouseWasDown { get; private set; }

		public void MouseUp(object sender, MouseEventArgs e)
		{
			MouseWasDown = MouseIsDown;
			MouseIsDown = false;
			OnMouseUp(sender, e);
		}
		public void MouseDown(object sender, MouseEventArgs e)
		{
			MouseWasDown = MouseIsDown;
			MouseIsDown = true;
			OnMouseDown(sender, e);
		}
		public void MouseMove(object sender, MouseEventArgs e)
		{
			MouseWasDown = MouseIsDown;
			MouseIsDown = true;
			OnMouseMove(sender, e);
		}
		public void MouseDoubleClick(object sender, MouseEventArgs e)
		{
			OnMouseDoubleClick(sender, e);
		}
		public void Paint(object sender, PaintEventArgs e)
		{
			OnPaint(sender, e);
		}

		public abstract void OnMouseUp(object sender, MouseEventArgs e);
		public abstract void OnMouseDown(object sender, MouseEventArgs e);
		public abstract void OnMouseMove(object sender, MouseEventArgs e);

		public virtual void OnMouseDoubleClick(object sender, MouseEventArgs e) { }

		public virtual void OnPaint(object sender, PaintEventArgs e) { }
	}
}
