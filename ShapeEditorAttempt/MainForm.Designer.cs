using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	partial class MainForm
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
			this.selectedShapeWidget = new SelectedShapeWidget(13, 13, 600, 80);
			this.selectedColorWidget = new SelectedColorWidget(613, 13, 600, 80);

			((ISupportInitialize)(this.Canvas)).BeginInit();
			selectedColorWidget.InitializeComponent(this);
			selectedShapeWidget.InitializeComponent(this);

			this.SuspendLayout();
			// 
			// Canvas
			// 
			this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Canvas.Cursor = System.Windows.Forms.Cursors.Cross;
			this.Canvas.Location = new System.Drawing.Point(13, 100);
			this.Canvas.Name = "Canvas";
			this.Canvas.Size = new System.Drawing.Size(775, 448);
			this.Canvas.TabIndex = 0;
			this.Canvas.TabStop = false;
			
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 560);
			this.Controls.Add(this.selectedShapeWidget);
			this.Controls.Add(this.Canvas);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public Canvas Canvas;
		public SelectedShapeWidget selectedShapeWidget;
		public SelectedColorWidget selectedColorWidget;
	}
}

