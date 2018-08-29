using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Canvas = new Canvas();
			((System.ComponentModel.ISupportInitialize)this.Canvas).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.Canvas.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
						| AnchorStyles.Left)
						| AnchorStyles.Right)));
			this.Canvas.BorderStyle = BorderStyle.Fixed3D;
			this.Canvas.Cursor = Cursors.Cross;
			this.Canvas.Location = new Point(13, 13);
			this.Canvas.Name = "Canvas";
			this.Canvas.Size = new Size(775, 425);
			this.Canvas.TabIndex = 0;
			this.Canvas.TabStop = false;
			this.Canvas.Paint += new PaintEventHandler(this.Canvas.Canvas_Paint);
			this.Canvas.MouseDown += new MouseEventHandler(this.Canvas.Canvas_MouseDown);
			this.Canvas.MouseMove += new MouseEventHandler(this.Canvas.Canvas_MouseMove);
			this.Canvas.MouseUp += new MouseEventHandler(this.Canvas.Canvas_MouseUp);
			this.KeyPress += new KeyPressEventHandler(this.Canvas.Canvas_KeyPress);

			// 
			// Form1
			// 
			this.AutoScaleDimensions = new SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(800, 450);

			this.Controls.Add(this.Canvas);

			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Canvas Canvas;
	}
}

