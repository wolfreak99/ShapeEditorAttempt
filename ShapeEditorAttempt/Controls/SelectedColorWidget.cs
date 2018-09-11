using System.Windows.Forms;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class SelectedColorWidget : GroupBoxPanel, IInitializeComponent
	{
		public const int COLOR_BUTTON_WIDTH = 40;
		public const int COLOR_BUTTON_HEIGHT = 50;
		public const int COLOR_BUTTON_XSPACING = 3;

		public Color Value = Color.Black;

		private Color[] colors = ColorsArray.FromPalettes();
		private ColorButton[] colorToggles;

		public void InitializeComponent()
		{
			int xspacing = COLOR_BUTTON_XSPACING, yspacing = 0;
			int left = xspacing, top = yspacing;
			int width = COLOR_BUTTON_WIDTH, height = COLOR_BUTTON_HEIGHT;
			int x = left, y = top;
			int tab = 2;

			colorToggles = new ColorButton[colors.Length];
			for (int i = 0; i < colors.Length; i++)
			{
				var button = new ColorButton()
				{
					Location = new Point(x, y),
					Size = new Size(width, height),
					Color = colors[i],
					TabIndex = tab++,
					Checked = false
				};
				button.MouseClick += colorToggle_MouseClick;
				
				colorToggles[i] = button;

				this.Controls.Add(button);
				
				x += width + xspacing;
			}

			this.MaximumSize = new Size(left + (ColorsArray.COLORS_PER_PALETTE * (width + xspacing)), this.Size.Height);
		}
		public void UninitializeComponent()
		{
			foreach (var c in colorToggles)
			{
				c.MouseClick -= colorToggle_MouseClick;
			}
		}

		private void colorToggle_MouseClick(object sender, MouseEventArgs e)
		{
			ColorButton checkbox = (ColorButton)sender;

			// Set color to selected color button
			Value = checkbox.Color;

			// Mark selected color button
			foreach (var b in colorToggles)
			{
				b.Checked = (b.Color == Value);
			}

			// Change last selected shapes color if control is held.
			if (KeyboardController.IsControlDown)
			{
				MainForm.Instance.Canvas.SetShapeColor(ClickData.Shape, Value);
			}
		}

	}
}
