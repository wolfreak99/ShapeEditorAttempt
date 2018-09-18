using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	/// <summary>
	/// ColorButton
	/// </summary>
	public class ColorButton : ToggleButton
	{
		private Color m_color;
		public Color Color
		{
			get
			{
				return m_color;
			}
			set
			{
				m_color = value;
				BackColor = value;
				ForeColor = value;
			}
		}

		public ColorButton(Color color) : base()
		{
			Color = color;
			Checked = false;
		}

		public ColorButton() : this(Color.Black)
		{

		}
	}
}
