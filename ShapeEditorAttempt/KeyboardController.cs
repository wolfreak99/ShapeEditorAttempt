using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class KeyboardController
	{
		public static bool IsControlDown { get; private set; }

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
			switch (e.KeyCode)
			{
			// Escape from any unsupported keys
			case Keys.None:
			default:
				return;

			case Keys.ControlKey:
				IsControlDown = true;
				break;
			}
			MainForm.Instance.Canvas.Invalidate();
		}

		private static void MainForm_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			// Escape from any unsupported keys
			case Keys.None:
			default:
				return;

			case Keys.ControlKey:
				IsControlDown = false;
				break;
			}
			MainForm.Instance.Canvas.Invalidate();

		}
	}
}
