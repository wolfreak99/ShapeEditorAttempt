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
		/// <summary>
		/// The Singleton instance used to access the MainForm clas and components.
		/// </summary>
		public static MainForm Instance { get; private set; }

		public MainForm()
		{
			Instance = this;

			// Initialize components
			InitializeComponent();
			SelectedShapeWidget.InitializeComponent();
			SelectedColorWidget.InitializeComponent();
			Canvas.InitializeComponent();
			KeyboardController.InitializeComponent();
		}

		// Parent Form
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

		private void ButtonClear_Click(object sender, EventArgs e)
		{
			Canvas.Clear();
		}
		
	}
}
