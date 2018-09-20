using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	/// <summary>
	/// GroupBox with a Panel integrated inside.
	/// </summary>
	public class GroupBoxPanel : GroupBox
	{
		private static readonly RectOffset GroupToPanelOffset = new RectOffset(4, 12, 4, 2);

		public Panel Panel { get; }

		public GroupBoxPanel(Form form)
		{
			Panel = new Panel();
			Panel.VerticalScroll.Enabled = false;
			Panel.VerticalScroll.Visible = false;
			Panel.HorizontalScroll.Enabled = true;
			Panel.HorizontalScroll.Visible = true;
			AutoScroll = true;

			form.Controls.Add(Panel);
		}

		public new ControlCollection Controls
		{
			get
			{
				return Panel.Controls;
			}
		}

		public bool AutoScroll
		{
			get {
				return Panel.AutoScroll;
			}
			set
			{
				Panel.AutoScroll = value;
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
				Panel.Location = new Point(
					value.X + GroupToPanelOffset.Left, 
					value.Y + GroupToPanelOffset.Top
				);
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
				Panel.Size = new Size(
					value.Width - (GroupToPanelOffset.AdditiveWidth), 
					value.Height - (GroupToPanelOffset.AdditiveHeight)
				);
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
				Panel.MaximumSize = new Size(
					value.Width - (GroupToPanelOffset.AdditiveWidth), 
					value.Height - (GroupToPanelOffset.AdditiveHeight)
				);
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
				Panel.Anchor = value;
			}
		}
        
		public void ScrollControlIntoView(Control control)
		{
			Panel.ScrollControlIntoView(control);
		}
		
	}
}
