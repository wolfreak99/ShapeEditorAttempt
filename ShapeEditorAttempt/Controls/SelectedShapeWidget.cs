using System.Windows.Forms;
using System.Drawing;
using System;

namespace ShapeEditorAttempt
{
	public class SelectedShapeWidget : GroupBox, IInitializeMainFormComponent
	{
		private ShapeButton[] ShapeButtons;

		public ShapeType Value = ShapeType.Square;

		public MainForm ParentMainForm { get; set; }

		public SelectedShapeWidget() : base() { }
		
		public void InitializeComponent(MainForm parentMainForm)
		{
			ParentMainForm = parentMainForm;

			int xspacing = 6, yspacing = 16;
			int left = xspacing, top = yspacing, width = 50, height = 50, x = left, y = top, tab = 2;

			ShapeButtons = new ShapeButton[3];
			for (int i = 0; i < ShapeButtons.Length; i++)
			{
				var button = new ShapeButton()
				{
					Location = new Point(x, y),
					Size = new Size(width, height),
					Text = ShapeTypeHelper.GetShapeName((ShapeType)i),
					TabIndex = tab++,
					Checked = (Value == (ShapeType)i),
					Type = (ShapeType)i
				};
				button.MouseClick += CheckBox_MouseClick;

				ShapeButtons[i] = button;
				this.Controls.Add(button);

				x += width + xspacing;
			}
			this.Size = new Size(x - left + xspacing + xspacing, this.Size.Height);
		}

		private void CheckBox_MouseClick(object sender, MouseEventArgs e)
		{
			ShapeButton button = (ShapeButton)sender;

			// Set color to selected color button
			Value = (ShapeType)button.Type;

			// Mark selected color button
			foreach (var b in ShapeButtons)
			{
				b.Checked = (Value == b.Type);
			}

			// Change last selected shapes color if control is held.
			if (KeyboardController.IsControlDown)
			{
				ParentMainForm.Canvas.SetShapeType(ClickData.Shape, Value);
			}
		}

		public void UninitializeComponent()
		{
			foreach (ShapeButton s in ShapeButtons)
			{
				s.MouseClick -= CheckBox_MouseClick;
			}
		}
		
	}
}
