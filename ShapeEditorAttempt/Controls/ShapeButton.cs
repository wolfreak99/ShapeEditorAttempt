using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class ShapeButton : CheckBox
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

		public ShapeButton(ShapeType type) : base()
		{
			Appearance = Appearance.Button;
			this.Type = type;
		}

		public ShapeButton() : this(ShapeType.Null)
		{

		}
	}
}
