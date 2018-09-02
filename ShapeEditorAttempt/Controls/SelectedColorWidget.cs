using System.Windows.Forms;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class SelectedColorWidget : GroupBox, IInitializeComponent
	{
		private Color[] colors = new Color[6]{Color.Red, Color.Green, Color.Blue, Color.Black, Color.Gray, Color.White };
		private CheckBox[] colorToggles;
		public Color SelectedColor = Color.Black;

		public MainForm ParentMainForm { get; set; }
		
		public void InitializeComponent(MainForm parentMainForm = null)
		{
			ParentMainForm = parentMainForm;
			int xspacing = 6, yspacing = 16;
			int left = xspacing, top = yspacing, width = 50, height = 50, x = left, y = top, tab = 1;

			colorToggles = new CheckBox[6];
			for (int i = 0; i < colors.Length; i++)
			{
				var c = new CheckBox();
				c.Appearance = Appearance.Button;
				c.Location = new Point(x, y);
				c.Size = new Size(width, height);
				c.BackColor = colors[i];
				c.ForeColor = colors[i];
				c.TabIndex = tab++;
				c.MouseClick += ((s, e) => {
					colorToggle_MouseClick(i, s, e);
				});
				c.Checked = false;
				colorToggles[i] = c;
				this.Controls.Add(c);

				x += width + xspacing;
			}
			this.Size = new Size(x - left + xspacing + xspacing, this.Size.Height);
			//Invalidate();
		}
		
		public void UninitializeComponent() { }

		private void colorToggle_MouseClick(int index, object sender, MouseEventArgs e)
		{
			CheckBox c = (CheckBox)sender;
			SelectedColor = c.ForeColor;
			for (int i = 0; i < colors.Length - 1; i++)
			{
				if (colorToggles[i].ForeColor == c.ForeColor)
					colorToggles[i].Checked = true;
				else
					colorToggles[i].Checked = false;
			}
			
			if (ParentMainForm.Canvas.clickedShape != null)
			{
				ParentMainForm.Canvas.clickedShape.color = SelectedColor;
				ParentMainForm.Canvas.Invalidate();
			}
		}
		
		public SelectedColorWidget() : base() { }

	}
}
