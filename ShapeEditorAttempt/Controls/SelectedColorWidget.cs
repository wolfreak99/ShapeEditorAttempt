using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class SelectedColorWidget : GroupBoxPanel, IInitializeComponent
	{
		public const int BUTTON_WIDTH = 40;
		public const int BUTTON_HEIGHT = 50;
		public const int BUTTON_XSPACING = 3;

		public Color Value { get; private set; } = Color.Black;

		private ColorButton[] colorToggles;
		private int PaletteIndex = 0;

		public SelectedColorWidget() : base(MainForm.Instance)
		{

		}

		private T CreateButton<T>(ref ButtonRect btnRect, int tabIndex, string text, 
			MouseEventHandler clickFunc, int colorIndex = -1)
			where T : Control, new()
		{
			Control button = new T()
			{
				Location = new Point(btnRect.x, btnRect.y),
				Size = new Size(btnRect.width, btnRect.height),
				TabIndex = tabIndex,
				Text = text,
			};

			// Only set color info when button is a color button (ignoring palette scroll buttons)
			if (typeof(T) == typeof(ColorButton))
			{
				((ColorButton)button).Text = null;
				((ColorButton)button).Color = ColorsArray.GetColorFromPalette(PaletteIndex, colorIndex);
			}

			button.MouseClick += clickFunc;

			Controls.Add(button);
			btnRect.x += btnRect.width + btnRect.xSpacing;

			return (T)button;
		}

		public void InitializeComponent()
		{
			ButtonRect btnRect = new ButtonRect(BUTTON_WIDTH, BUTTON_HEIGHT, BUTTON_XSPACING, 0);
			btnRect.left = btnRect.x;
			int tab = 2;

			// Create an array to store the amount of colors per palette
			colorToggles = new ColorButton[ColorsArray.COLORS_PER_PALETTE];
			
			// Left button to create Scrolling
			CreateButton<Button>(ref btnRect, tab++, "<", PreviousPaletteButton_Click);

			// Create enough color buttons to match the colors per palette
			for (int i = 0; i < ColorsArray.COLORS_PER_PALETTE; i++)
			{
				var button = CreateButton<ColorButton>(ref btnRect, tab++, null, ColorButton_Click, i);
				colorToggles[i] = button;
			}
			
			// Right button to create Scrolling
			CreateButton<Button>(ref btnRect, tab++, ">", NextPaletteButton_Click);
			
			// Clamp the panel's maximum size to fit the color buttons + left/right buttons
			const int buttonCount = ColorsArray.COLORS_PER_PALETTE + 2;
			int rightOffset = btnRect.xSpacing * 2;
			this.MaximumSize = new Size(
				btnRect.left + (buttonCount * (btnRect.width + btnRect.xSpacing)) + rightOffset, 
				this.Size.Height
			);
		}

		public void UninitializeComponent()
		{
			// Unsubscribe clicks from all colorToggles.
			foreach (var c in colorToggles)
			{
				c.MouseClick -= ColorButton_Click;
			}
		}

		private void SwitchToPalette(int paletteIndex)
		{
			if (paletteIndex >= ColorsArray.PALETTE_COUNT || paletteIndex < 0)
				throw new IndexOutOfRangeException("paletteIndex out of range");

			this.PaletteIndex = paletteIndex;

			for (int i = 0; i < ColorsArray.COLORS_PER_PALETTE; i++)
			{
				colorToggles[i].Color = ColorsArray.GetColorFromPalette(this.PaletteIndex, i);
			}
		}

		private void PreviousPaletteButton_Click(object sender, System.EventArgs e)
		{
			var newPaletteIndex = Utils.WrapIncrement(PaletteIndex, -1, 0, ColorsArray.PALETTE_COUNT);

			SwitchToPalette(newPaletteIndex);
		}

		private void NextPaletteButton_Click(object sender, System.EventArgs e)
		{
			var newPaletteIndex = Utils.WrapIncrement(PaletteIndex, 1, 0, ColorsArray.PALETTE_COUNT);

			SwitchToPalette(newPaletteIndex);
		}

		private void SelectColor(ColorButton button, bool setSelectedShapeColor)
		{
			// If button was already selected, change selected shape
			if (!setSelectedShapeColor && Value == button.Color)
			{
				setSelectedShapeColor = true;
			}

			// Set color to selected color button
			Value = button.Color;
			
			// Mark selected color button
			foreach (var b in colorToggles)
			{
				b.Checked = (b.Color == Value);
			}

			// Set selected shape to color
			if (setSelectedShapeColor)
			{
				foreach (Shape s in ClickData.Shapes)
				{
					Canvas.Instance.SetShapeColor(s, Value);
				}
			}
		}

		private void ColorButton_Click(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			bool setToColor = false;

			// Set color if control was held down
			if (KeyboardController.IsControlDown)
			{
				setToColor = true;
			}
			
			SelectColor((ColorButton)sender, setToColor);
		}
		
		private class ButtonRect
		{
			public int x { get; internal set; }
			public int y { get; internal set; }
			public int width { get; private set; }
			public int height { get; private set; }
			public int xSpacing { get; private set; }
			public int ySpacing { get; private set; }
			public int left { get; internal set; }
			public int top { get; internal set; }
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
