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

		internal static void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
			{
				IsControlDown = true;
				MainForm.Instance.Canvas.Invalidate();
			}
		}

		internal static void MainForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
			{
				IsControlDown = false;
				MainForm.Instance.Canvas.Invalidate();
			}
		}
	}
}
