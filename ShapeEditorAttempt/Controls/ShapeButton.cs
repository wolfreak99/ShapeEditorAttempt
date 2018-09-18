using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class ShapeButton : ToggleButton
	{
		private ShapeType m_type;
		public ShapeType Type
		{
			get
			{
				return m_type;
			}
			set
			{
				m_type = value;
			}
		}

		public ShapeButton(ShapeType type = ShapeType.Null) : base()
		{
			this.Type = type;
		}

		public ShapeButton() : this(ShapeType.Null)
		{

		}
	}
}
