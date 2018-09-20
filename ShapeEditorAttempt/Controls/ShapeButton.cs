using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class ShapeButton : ToggleButton
	{
		public ShapeType Type { get; set; }

		public ShapeButton(ShapeType type = ShapeType.Null) : base()
		{
			this.Type = type;
		}

		public ShapeButton() : this(ShapeType.Null)
		{

		}
	}
}
