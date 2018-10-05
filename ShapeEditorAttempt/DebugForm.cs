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
			private class LogMessageData
			{
				public static List<LogMessageData> Messages
				{
					get;
					private set;
				} = new List<LogMessageData>();

				private LogType LogType;
				private Color Color;
				private string Message;
				private string Prefix;

				/// <summary>
				/// This will get incremented if a new message 
				/// was about to be added that matched this.
				/// </summary>
				private int Count;

				private LogMessageData(string message, LogType logType)
				{
					switch (logType)
					{
					case LogType.Info:
						Color = SystemColors.ControlText;
						Prefix = "Log: ";
						break;
					case LogType.Warning:
						Color = Color.Yellow;
						Prefix = "Warning: ";
						break;
					case LogType.Error:
						Color = Color.Red;
						Prefix = "Error: ";
						break;
					default:
						throw new NotImplementedException();
					}
					LogType = logType;
					Message = message;
					Count = 1;
				}

				private static void AppendLogToBox(RichTextBox box, LogMessageData msg)
				{
					string msgText = string.Format("{0}{1}{2}\r\n", msg.Prefix, msg.Message,
						msg.Count > 1 ? " (" + msg.Count.ToString() + ")" : "");

					box.SelectionStart = box.TextLength;
					box.SelectionLength = 0;
					box.SelectionColor = msg.Color;
					box.AppendText(msgText);
					box.SelectionColor = box.ForeColor;
				}

				private static bool FindDuplicateMessage(string message, LogType logType, 
					int searchDepth, out int index)
				{
					if (Messages.Count >= 1)
					{
						int min = Math.Max(0, Messages.Count - (searchDepth + 1));
						for (int i = Messages.Count - 1; i > min; i--)
						{
							var m = Messages[i];
							if (m.LogType == logType && m.Message == message)
							{
								index = i;
								return true;
							}
						}
					}
					index = -1;
					return false;
				}

				public static void AppendLog(RichTextBox box, string message, LogType logType)
				{
					int duplicateMessageIndex;
					const int SEARCH_DEPTH = 2;
					if (FindDuplicateMessage(message, logType, SEARCH_DEPTH, 
						out duplicateMessageIndex))
					{
						Messages[duplicateMessageIndex].Count++;

						// Message count was updated. Clear text and rewrite log info.
						box.Clear();
						foreach (var msg in Messages)
						{
							AppendLogToBox(box, msg);
						}
					}
					else
					{
						// No message count updated. Just add new log to textbox.
						var msg = new LogMessageData(message, logType);
						Messages.Add(msg);
						AppendLogToBox(box, msg);
					}
				}

				public static void ClearMessages()
				{
					Messages.Clear();
					Instance.logTextBox.Clear();
				}
			}

			public static DebugForm Instance = new DebugForm();

			public DebugForm()
			{
				Instance = this;
				InitializeComponent();
			}

			internal static void Log(string text, LogType logType = LogType.Info)
			{
				LogMessageData.AppendLog(Instance.logTextBox, text, logType);
			}

			private void clearLogButton_Click(object sender, EventArgs e)
			{
				LogMessageData.ClearMessages();
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

		public static void LogException(Exception e)
		{
			Log(e.Message, LogType.Error);
		}

		public static void Show()
		{
			DebugInternal.DebugForm.Instance.Show();
		}
	}
}
