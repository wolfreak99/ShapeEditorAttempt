using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public partial class MainForm : Form
	{
		public static MainForm Instance;


		public MainForm()
		{
			Instance = this;

			KeyPreview = true;
			InitializeComponent();
			selectedShapeWidget.InitializeComponent();
			selectedColorWidget.InitializeComponent();
			Canvas.InitializeComponent();
			KeyDown += KeyboardController.MainForm_KeyDown;
			KeyUp += KeyboardController.MainForm_KeyUp;
		}

		//private Form
		private Form m_parentForm;
		new public Form ParentForm
		{
			get
			{
				return m_parentForm;
			}
			set
			{
				m_parentForm = value;
			}
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			Canvas.Clear();
		}
		
	}
}
