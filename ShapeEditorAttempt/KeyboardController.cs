using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class KeyboardController
	{
		public static bool IsControlDown { get; private set; }
		public static bool IsShiftDown { get; private set; }
		public static bool IsMoveDown { get; private set; }
		public static bool ShowAllBordersDown { get; internal set; }

		private static Keys m_prevKey = Keys.None;
		private static bool m_prevIsDown = false;

		private static void ProcessKeys(KeyEventArgs e, bool isDown)
		{
			switch (e.KeyCode)
			{
			// Escape from any unsupported keys
			case Keys.None:
			default:
				break;
			case Keys.R:
				if (!isDown && !ClickData.ShapesEmpty() && Canvas.Instance.Focused)
				{
					foreach (var s in ClickData.Shapes)
					{
						s.Height = s.Width;
					}
				}
				break;
			case Keys.PageUp:
				if (!isDown && !ClickData.ShapesEmpty() && Canvas.Instance.Focused)
				{
					foreach (var s in ClickData.Shapes)
					{
						Canvas.Instance.layer.MoveShapeUp(s);
					}
				}
				break;
			case Keys.PageDown:
				if (!isDown && !ClickData.ShapesEmpty() && Canvas.Instance.Focused)
				{
					foreach (var s in ClickData.Shapes)
					{
						Canvas.Instance.layer.MoveShapeDown(s);
					}
				}
				break;
			case Keys.M:
				IsMoveDown = isDown && Canvas.Instance.Focused;
				break;
			case Keys.P:
				if (isDown)
				{
					ToolBase.SwitchToTool(new MainTool());
				}
				break;
			case Keys.E:
				if (isDown)
				{
					ToolBase.SwitchToTool(new EraserTool());
				}
				break;
			case Keys.S:
				if (isDown)
				{
					ToolBase.SwitchToTool(new SelectorTool());
				}
				break;
			case Keys.ControlKey:
				IsControlDown = isDown;
				break;
			case Keys.A:
				ShowAllBordersDown = isDown;
				break;
			case Keys.ShiftKey:
				IsShiftDown = isDown;
				break;
			}

			ToolBase.Current.ProcessKeys(e, isDown);

			// Invalidate Canvas if it was a new key input
			if (!(m_prevKey == e.KeyCode && m_prevIsDown == isDown))
			{
				Canvas.Instance.Invalidate();
			}

			m_prevKey = e.KeyCode;
			m_prevIsDown = isDown;
		}

		internal static void InitializeComponent()
		{
			MainForm.Instance.KeyPreview = true;
			MainForm.Instance.KeyDown += MainForm_KeyDown;
			MainForm.Instance.KeyUp += MainForm_KeyUp;
		}

		internal static void UninitializeComponent()
		{
			MainForm.Instance.KeyPreview = true;
			MainForm.Instance.KeyDown -= MainForm_KeyDown;
			MainForm.Instance.KeyUp -= MainForm_KeyUp;
		}

		private static void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessKeys(e, true);
		}

		private static void MainForm_KeyUp(object sender, KeyEventArgs e)
		{
			ProcessKeys(e, false);
		}
	}
}
