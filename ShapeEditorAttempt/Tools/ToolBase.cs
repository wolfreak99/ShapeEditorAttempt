using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public abstract class ToolBase
	{
		public static ToolBase Current { get; private set; } = new MainTool();

		public bool MouseIsDown { get; private set; } = false;
		public bool MouseWasDown { get; private set; } = false;

		internal static void SwitchToTool(ToolBase newTool)
		{
			Current.UnloadTool();
			Current = newTool;
		}

		public ToolBase()
		{

		}

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

		public void ProcessKeys(KeyEventArgs e, bool isDown)
		{
			OnProcessKeys(e, isDown);
		}

		public void UnloadTool()
		{
			OnUnloadTool();
		}

		public abstract void OnMouseUp(object sender, MouseEventArgs e);
		public abstract void OnMouseDown(object sender, MouseEventArgs e);
		public abstract void OnMouseMove(object sender, MouseEventArgs e);

		public virtual void OnMouseDoubleClick(object sender, MouseEventArgs e) { }

		public virtual void OnPaint(object sender, PaintEventArgs e) { }
		public virtual void OnProcessKeys(KeyEventArgs e, bool isDown) { }

		public virtual void OnUnloadTool() { }
	}
}
