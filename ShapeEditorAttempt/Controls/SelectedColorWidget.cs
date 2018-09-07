using System.Windows.Forms;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class SelectedColorWidget : GroupBoxPanel, IInitializeComponent
	{
		public Color Value = Color.Black;

		private Color[] colors = ColorsArray.Array;
		private ColorButton[] colorToggles;
		
		public SelectedColorWidget() : base()
		{
			//AutoScroll = true;
			Text = "Test";
		}

		public void InitializeComponent()
		{
			int xspacing = 6, yspacing = 0;
			int left = xspacing, top = yspacing, width = 50, height = 50, x = left, y = top, tab = 2;

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

			//this.Size = new Size((x - left) + xspacing + xspacing, this.Size.Height);
			//Invalidate();
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
