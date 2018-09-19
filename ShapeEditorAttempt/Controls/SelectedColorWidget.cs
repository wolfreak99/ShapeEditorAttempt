using System.Windows.Forms;
using System.Drawing;
using System;

namespace ShapeEditorAttempt
{
	public class SelectedColorWidget : GroupBoxPanel, IInitializeComponent
	{
		public const int COLOR_BUTTON_WIDTH = 40;
		public const int COLOR_BUTTON_HEIGHT = 50;
		public const int COLOR_BUTTON_XSPACING = 3;

		public Color Value = Color.Black;

		private ColorButton[] colorToggles;
		private int PaletteIndex = 0;

		public SelectedColorWidget() : base(MainForm.Instance) { }

		private T CreateButton<T>(ref ButtonRect btnRect, int tabIndex, string text, 
			EventHandler clickFunc, EventHandler dblClickFunc = null, int colorIndex = -1)
			where T : Control, new()
		{
			Control button = new T()
			{
				Location = new Point(btnRect.x, btnRect.y),
				Size = new Size(btnRect.width, btnRect.height),
				TabIndex = tabIndex,
				Text = text,
			};

			if (typeof(T) == typeof(ColorButton))
			{
				((ColorButton)button).Text = null;
				((ColorButton)button).Color = ColorsArray.GetColorFromPalette(PaletteIndex, colorIndex);
			}

			button.Click += clickFunc;

			Controls.Add(button);
			btnRect.x += btnRect.width + btnRect.xSpacing;

			return (T)button;
		}

		public void InitializeComponent()
		{
			ButtonRect btnRect = new ButtonRect(COLOR_BUTTON_WIDTH, COLOR_BUTTON_HEIGHT, COLOR_BUTTON_XSPACING, 0);
			int left = btnRect.x;
			int tab = 2;

			colorToggles = new ColorButton[ColorsArray.COLORS_PER_PALETTE];
			
			// Left button to create Scrolling
			CreateButton<Button>(ref btnRect, tab++, "<", LeftButton_Click);
			for (int i = 0; i < ColorsArray.COLORS_PER_PALETTE; i++)
			{
				var button = CreateButton<ColorButton>(ref btnRect, tab++, null, colorToggle_Click, colorToggle_DoubleClick, i);
				colorToggles[i] = button;
			}
			// Right button to create Scrolling
			CreateButton<Button>(ref btnRect, tab++, ">", RightButton_Click);
			
			const int buttonCount = ColorsArray.COLORS_PER_PALETTE + 2;
			int rightOffset = btnRect.xSpacing * 2;
			this.MaximumSize = new Size(
				left + (buttonCount * (btnRect.width + btnRect.xSpacing)) + rightOffset, 
				this.Size.Height
			);
		}

		public void UninitializeComponent()
		{
			foreach (var c in colorToggles)
			{
				c.MouseClick -= colorToggle_Click;
			}
		}

		private void SwitchToPalette(int paletteIndex)
		{
			if (paletteIndex >= ColorsArray.PALLETTE_COUNT || paletteIndex < 0)
				throw new IndexOutOfRangeException("paletteIndex out of range");

			this.PaletteIndex = paletteIndex;
			for (int i = 0; i < ColorsArray.COLORS_PER_PALETTE; i++)
			{
				colorToggles[i].Color = ColorsArray.GetColorFromPalette(this.PaletteIndex, i);
			}
		}

		private void LeftButton_Click(object sender, System.EventArgs e)
		{
			var newPaletteIndex = Utils.WrapIncrement(PaletteIndex, -1, 0, ColorsArray.PALLETTE_COUNT);

			SwitchToPalette(newPaletteIndex);
			PaletteIndex = newPaletteIndex;
		}

		private void RightButton_Click(object sender, System.EventArgs e)
		{
			var newPaletteIndex = Utils.WrapIncrement(PaletteIndex, 1, 0, ColorsArray.PALLETTE_COUNT);

			SwitchToPalette(newPaletteIndex);
			PaletteIndex = newPaletteIndex;
		}

		private void SelectColor(ColorButton button, bool setSelectedShapeColor)
		{
			// Set color to selected color button
			if (button.Color == Value)
			{
				Canvas.Instance.SetShapeColor(ClickData.Shape, Value);
			}

			Value = button.Color;

			// Mark selected color button
			foreach (var b in colorToggles)
			{
				b.Checked = (b.Color == Value);
			}
			// Set selected shape to color
			if (setSelectedShapeColor)
			{
				Canvas.Instance.SetShapeColor(ClickData.Shape, Value);
			}
		}

		private void colorToggle_Click(object sender, System.EventArgs e)
		{
			SelectColor((ColorButton)sender, KeyboardController.IsControlDown);
		}

		private void colorToggle_DoubleClick(object sender, EventArgs e)
		{
			SelectColor((ColorButton)sender, true);
		}

		private class ButtonRect
		{
			public int x;
			public int y;
			public int width { get; private set; }
			public int height { get; private set; }
			public int xSpacing { get; private set; }
			public int ySpacing { get; private set; }
			public int left { get; private set; }
			public int top { get; private set; }
			public ButtonRect(int width, int height, int xSpacing, int ySpacing)
			{
				this.width = width;
				this.height = height;
				this.xSpacing = xSpacing;
				this.ySpacing = ySpacing;
				this.x = xSpacing;
				this.y = ySpacing;
			}
		}
	}
}
