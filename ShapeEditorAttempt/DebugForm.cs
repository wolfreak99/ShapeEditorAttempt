using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	namespace DebugInternal
	{
		public partial class DebugForm : Form
		{
			public static Color COLOR_INFO { get; private set; }
			public static readonly Color COLOR_WARNING = Color.Yellow;
			public static readonly Color COLOR_ERROR = Color.Red;

			public static DebugForm Instance = new DebugForm();

			public DebugForm()
			{
				Instance = this;
				InitializeComponent();

				COLOR_INFO = logTextBox.ForeColor;
			}

			private void Log(string text, Color color)
			{
				var box = Instance.logTextBox;
				box.SelectionStart = box.TextLength;
				box.SelectionLength = 0;
				box.SelectionColor = color;
				box.AppendText(text);
				box.AppendText("\r\n");
				box.SelectionColor = box.ForeColor;
			}

			internal static void Log(string text, LogType logType = LogType.Info)
			{
				Color color;
				switch (logType)
				{
				case LogType.Warning: color = COLOR_WARNING; break;
				case LogType.Error: color = COLOR_ERROR; break;
				case LogType.Info:
				default: color = COLOR_INFO; break;
				}
				Instance.Log(text, color);
			}

			private void clearLogButton_Click(object sender, EventArgs e)
			{
				Instance.logTextBox.Clear();
			}

			#region Windows Form Designer generated code

			/// <summary>
			/// Required designer variable.
			/// </summary>
			private IContainer components = null;

			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
			protected override void Dispose(bool disposing)
			{
				if (disposing && (components != null))
				{
					components.Dispose();
				}
				base.Dispose(disposing);
			}

			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				logTextBox = new RichTextBox();
				clearLogButton = new Button();
				SuspendLayout();
				// 
				// logTextBox
				// 
				logTextBox.Dock = DockStyle.Top;
				logTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
				logTextBox.Location = new Point(0, 0);
				logTextBox.Name = "logTextBox";
				logTextBox.ReadOnly = true;
				logTextBox.Size = new Size(400, 180);
				logTextBox.TabIndex = 1;
				logTextBox.Text = "";
				// 
				// clearLogButton
				// 
				clearLogButton.Dock = DockStyle.Bottom;
				clearLogButton.Location = new Point(0, 180);
				clearLogButton.Name = "clearLogButton";
				clearLogButton.Size = new Size(400, 23);
				clearLogButton.TabIndex = 2;
				clearLogButton.Text = "Clear Log";
				clearLogButton.UseVisualStyleBackColor = true;
				clearLogButton.Click += new EventHandler(clearLogButton_Click);
				// 
				// DebugForm
				// 
				AutoScaleDimensions = new SizeF(6F, 13F);
				AutoScaleMode = AutoScaleMode.Font;
				ClientSize = new Size(400, 203);
				Controls.Add(clearLogButton);
				Controls.Add(logTextBox);
				Name = "DebugForm";
				Text = "DebugForm";
				ResumeLayout(false);

			}

			#endregion
			private RichTextBox logTextBox;
			private Button clearLogButton;
		}
	}

	public enum LogType
	{
		Info,
		Warning,
		Error
	}

	public static class Debug
	{
		public static void Log(string message, LogType logType = LogType.Info)
		{
			DebugInternal.DebugForm.Log(message, logType);
		}
		public static void Show()
		{
			DebugInternal.DebugForm.Instance.Show();
		}
	}
}
