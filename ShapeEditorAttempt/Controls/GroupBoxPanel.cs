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
	/// GroupBox with a Panel integrated inside.
	/// </summary>
	public class GroupBoxPanel : GroupBox
	{
		private Panel m_Panel;
		private static readonly RectOffset GroupToPanelOffset = new RectOffset(4, 12, 4, 2);

		public Panel Panel
		{
			get { return m_Panel; }
		}

		public new ControlCollection Controls
		{
			get
			{
				return m_Panel.Controls;
			}
		}

		public bool AutoScroll
		{
			get {
				return m_Panel.AutoScroll;
			}
			set
			{
				m_Panel.AutoScroll = value;
			}
		}

		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
				m_Panel.Location = new Point(value.X + GroupToPanelOffset.Left, value.Y + GroupToPanelOffset.Top);
			}
		}

		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
				m_Panel.Size = new Size(value.Width - (GroupToPanelOffset.Width), value.Height - (GroupToPanelOffset.Height));
			}
		}

		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
				m_Panel.MaximumSize = new Size(value.Width - (GroupToPanelOffset.Width), value.Height - (GroupToPanelOffset.Height));
			}
		}


		public new AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
				m_Panel.Anchor = value;
			}
		}
            
		public GroupBoxPanel()
		{
			m_Panel = new Panel();
			m_Panel.VerticalScroll.Enabled = false;
			m_Panel.VerticalScroll.Visible = false;
			m_Panel.HorizontalScroll.Enabled = true;
			m_Panel.HorizontalScroll.Visible = true;
			AutoScroll = true;
			
			MainForm.Instance.Controls.Add(m_Panel);
		}
	}
}
