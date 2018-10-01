using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{

	public enum LogType
	{
		Info,
		Warning,
		Error
	}

	namespace DebugInternal
	{
		public class DebugForm : Form
		{
			private struct LogTypeFormatData
			{
				private LogType _LogType;
				public Color Color;
				public string Prefix;
				private static LogTypeFormatData[] Datas = new LogTypeFormatData[] {
					new LogTypeFormatData(LogType.Info, SystemColors.ControlText, "Log: "),
					new LogTypeFormatData(LogType.Warning, Color.Yellow, "Warning: "),
					new LogTypeFormatData(LogType.Error, Color.Red, "Error: ")
				};

				private LogTypeFormatData(LogType logType, Color color, string prefix)
				{
					this._LogType = logType;
					this.Color = color;
					this.Prefix = prefix;
				}
				
				public static LogTypeFormatData GetData(LogType type)
				{
					return Datas[(int)type];
				}
			}
			
			public static DebugForm Instance = new DebugForm();

			public DebugForm()
			{
				Instance = this;
				InitializeComponent();
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
				LogTypeFormatData formatData = LogTypeFormatData.GetData(logType);
				Instance.Log(formatData.Prefix + text, formatData.Color);
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
