using System.Windows.Forms;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class SelectedColorWidget : GroupBox, IInitializeComponent
	{
		private Color[] colors = new Color[]{Color.Red, Color.Green, Color.Blue, Color.Black };
		private CheckBox[] colorToggles;
		public Color SelectedColor = Color.Black;

		public MainForm ParentMainForm { get; set; }

		public void InitializeComponent(MainForm parentMainForm = null)
		{
			ParentMainForm = parentMainForm;

			int top = Location.Y, left = Location.X, width = 20, height = 20, xspacing = 6, yspacing = 6, x = left, y = top, tab = 1;

			colorToggles = new CheckBox[colors.Length];
			for (int i = 0; i < colors.Length; i++)
			{
				colorToggles[i] = new CheckBox();
				colorToggles[i].Appearance = Appearance.Button;
				colorToggles[i].Location = new Point(x, y);
				colorToggles[i].Size = new Size(width, height);
				colorToggles[i].TabIndex = tab++;
				colorToggles[i].BackColor = colors[i];
				colorToggles[i].ForeColor = colors[i];
				colorToggles[i].MouseClick += colorToggle_MouseClick;
				this.Controls.Add(colorToggles[i]);
				if (i != 0)
				{
					if (i % 2 == 0)
					{
						y += height + yspacing;
					}
					else
					{
						y = top;
						x += width + xspacing;
					}
				}
			}
			this.Size = new Size(x - left + xspacing, y - top + yspacing);
		}

		public void UninitializeComponent() { }

		private void colorToggle_MouseClick(object sender, MouseEventArgs e)
		{
			var c = (CheckBox)sender;
			SelectedColor = c.BackColor;

			
			if (ParentMainForm.Canvas.clickedShape != null)
			{
				ParentMainForm.Canvas.clickedShape.color = SelectedColor;
			}
		}

		public SelectedColorWidget(int x, int y, int width, int height)
		{
			this.Location = new System.Drawing.Point(x, y);
			this.Name = "SelectedColorWidget";
			this.Size = new System.Drawing.Size(width, height);
			this.TabIndex = 1;
			this.TabStop = false;
			this.Text = "Selected Color";
		}
	}
}
