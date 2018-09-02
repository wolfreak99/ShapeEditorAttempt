using System.Windows.Forms;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class SelectedShapeWidget : GroupBox, IInitializeComponent
	{
		private CheckBox buttonSquare,
					   buttonCircle,
					   buttonTriangle;
		
		public Shapes SelectedShape { get; set; }

		public MainForm ParentMainForm { get; set; }

		public void InitializeComponent(MainForm parentMainForm)
		{
			ParentMainForm = parentMainForm;

			int left = Location.X, top = Location.Y, width = 100, height = 60, xspacing = 6, x = left, tab = 1;
			
			buttonSquare = new CheckBox();
			buttonSquare.Appearance = Appearance.Button;
			buttonSquare.Location = new Point(x, top);
			buttonSquare.Size = new Size(width, height);
			buttonSquare.Text = Square.NAME;
			buttonSquare.TabIndex = tab++;
			buttonSquare.MouseClick += ButtonSquare_MouseClick;
			this.Controls.Add(buttonSquare);

			x += width + xspacing;

			buttonCircle = new CheckBox();
			buttonCircle.Appearance = Appearance.Button;
			buttonCircle.Location = new Point(x, top);
			buttonCircle.Size = new Size(width, height);
			buttonCircle.Text = Circle.NAME;
			buttonCircle.TabIndex = tab++;
			buttonCircle.MouseClick += ButtonCircle_MouseClick;
			this.Controls.Add(buttonCircle);

			x += width + xspacing;

			buttonTriangle = new CheckBox();
			buttonTriangle.Appearance = Appearance.Button;
			buttonTriangle.Location = new Point(x, top);
			buttonTriangle.Size = new Size(width, height);
			buttonTriangle.Text = Triangle.NAME;
			buttonTriangle.TabIndex = tab++;
			buttonTriangle.MouseClick += ButtonTriangle_MouseClick;
			this.Controls.Add(buttonTriangle);

			x += width + xspacing;

			this.Size = new Size(x - left + xspacing + xspacing, this.Size.Height);
		}

		public void UninitializeComponent() { }

		private void ButtonTriangle_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedShape = Shapes.Triangle;
			buttonTriangle.CheckState = CheckState.Checked;
			buttonCircle.CheckState = CheckState.Unchecked;
			buttonSquare.CheckState = CheckState.Unchecked;
		}

		private void ButtonCircle_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedShape = Shapes.Circle;
			buttonCircle.CheckState = CheckState.Checked;
			buttonTriangle.CheckState = CheckState.Unchecked;
			buttonSquare.CheckState = CheckState.Unchecked;
		}

		private void ButtonSquare_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedShape = Shapes.Square;
			buttonSquare.CheckState = CheckState.Checked;
			buttonCircle.CheckState = CheckState.Unchecked;
			buttonTriangle.CheckState = CheckState.Unchecked;
		}

		
		public SelectedShapeWidget() : base() { }

	}
}
