using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class SelectedShapeWidget : GroupBox, IInitializeComponent
	{
		public const int BUTTON_WIDTH = 70;
		public const int BUTTON_HEIGHT = 50;
		public const int BUTTON_XSPACING = 6;
		public const int BUTTON_YSPACING = 16;

		public ShapeType Value { get; private set; } = ShapeType.Square;

		private ShapeButton[] ShapeButtons;

		public SelectedShapeWidget() : base()
		{

		}

		private T CreateButton<T>(ref ButtonRect btnRect, int tabIndex, string text,
			MouseEventHandler clickFunc, int shapeIndex = -1)
			where T : Control, new()
		{
			Control button = new T()
			{
				Location = new Point(btnRect.x, btnRect.y),
				Size = new Size(btnRect.width, btnRect.height),
				TabIndex = tabIndex,
				Text = text,
			};

			// Only set type info when button is a shaoe button
			if (typeof(T) == typeof(ShapeButton))
			{
				((ShapeButton)button).Text = ShapeTypeHelper.GetShapeName((ShapeType)shapeIndex);
				((ShapeButton)button).Checked = (Value == (ShapeType)shapeIndex);
				((ShapeButton)button).Type = (ShapeType)shapeIndex;
			}

			button.MouseClick += clickFunc;

			Controls.Add(button);
			btnRect.x += btnRect.width + btnRect.xSpacing;

			return (T)button;
		}

		public void InitializeComponent()
		{
			ButtonRect btnRect = new ButtonRect(BUTTON_WIDTH, BUTTON_HEIGHT, BUTTON_XSPACING, BUTTON_YSPACING);
			btnRect.left = BUTTON_XSPACING;
			btnRect.top = BUTTON_YSPACING;
			int tab = 2;

			ShapeButtons = new ShapeButton[(int)ShapeType.Length];
			for (int i = 0; i < ShapeButtons.Length; i++)
			{
				var button = CreateButton<ShapeButton>(ref btnRect, tab++, null, CheckBox_MouseClick, i);
				ShapeButtons[i] = button;
			}
			this.Size = new Size(btnRect.x - btnRect.left + btnRect.xSpacing, this.Size.Height);
		}

		private void SelectType(ShapeButton button, bool setSelectedShapeType)
		{
			// If button was already selected, change selected shape
			if (!setSelectedShapeType && Value == button.Type)
			{
				setSelectedShapeType = true;
			}
			
			// Set type to selected type button
			Value = button.Type;

			// Mark selected type button
			foreach (var b in ShapeButtons)
			{
				b.Checked = (b.Type == Value);
			}

			// Set selected shape to type
			if (setSelectedShapeType)
			{
				foreach (Shape s in ClickData.Shapes)
				{
					Canvas.Instance.SetShapeType(s, Value);
				}
			}
		}

		private void CheckBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			bool setToType = false;

			// Set type if control was down
			if (KeyboardController.IsControlDown)
			{
				setToType = true;
			}

			SelectType((ShapeButton)sender, setToType);
		}

		public void UninitializeComponent()
		{
			foreach (ShapeButton s in ShapeButtons)
			{
				s.MouseClick -= CheckBox_MouseClick;
			}
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
