using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

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
			if (SelectedColorWidget == null)
			{
				ConstructColorWidget();
			}
			SelectedColorWidget.InitializeComponent();
			Canvas.InitializeComponent();
			KeyboardController.InitializeComponent();
		}

		/// <summary>
		/// The Designer mode for MainForm wipes out SelectedColorWidget. 
		/// This serves as a backup incase that happens.
		/// </summary>
		private void ConstructColorWidget()
		{
			this.SelectedColorWidget = new ShapeEditorAttempt.SelectedColorWidget();

			// 
			// SelectedColorWidget
			// 
			this.SelectedColorWidget.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
			this.SelectedColorWidget.Location = new System.Drawing.Point(SelectedShapeWidget.Right + 6, 13);
			this.SelectedColorWidget.Name = "SelectedColorWidget";
			this.SelectedColorWidget.Size = new System.Drawing.Size(330, 82);
			this.SelectedColorWidget.TabIndex = 1;
			this.SelectedColorWidget.TabStop = false;
			this.SelectedColorWidget.Text = "Selected Color";

			this.Controls.Add(this.SelectedColorWidget);
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

		private void ClearFocus()
		{
			Canvas.Focus();
		}

		private void ButtonClear_Click(object sender, EventArgs e)
		{
			Canvas.Clear();
		}

		private OpenFileDialog openFileDialog = new OpenFileDialog();
		private SaveFileDialog saveFileDialog = new SaveFileDialog();

		private void ButtonLoad_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Canvas.Load(openFileDialog.FileName);
				Canvas.Invalidate();
			}
		}
		
		private void ButtonSave_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Canvas.Save(saveFileDialog.FileName);
				Canvas.Invalidate();
			}
		}

		private void gridSizeSetButton_Click(object sender, EventArgs e)
		{
			int gridSizeInput;
			// If text is empty, set to default size.
			if (string.IsNullOrWhiteSpace(gridSizeTextBox.Text))
			{
				gridSizeTextBox.Text = Grid.DEFAULT_SIZE.ToString();
			}

			if (int.TryParse(gridSizeTextBox.Text, out gridSizeInput))
			{
				Grid.GridSize = new Size(gridSizeInput, gridSizeInput);
				Canvas.Invalidate();
			}
			else
			{
				gridSizeTextBox.Text = Grid.GridSize.Width.ToString();
			}

			ClearFocus();
		}

		private void gridSizeTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				gridSizeSetButton_Click(this, e);
			}
		}

		private Keys m_prevSelectedShapeNameKey = Keys.None;
		private void selectedShapeName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && m_prevSelectedShapeNameKey != e.KeyCode)
			{
				if (ClickData.Shape != null)
				{
					ClickData.Shape.Nickname = selectedShapeNameTextBox.Text;
					ClearFocus();
				}
			}
			m_prevSelectedShapeNameKey = e.KeyCode;
		}

		private void buttonSaveImage_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented");
		}
	}
}
