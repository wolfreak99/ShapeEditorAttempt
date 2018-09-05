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
			this.Canvas = new ShapeEditorAttempt.Canvas();
			this.SelectedShapeWidget = new ShapeEditorAttempt.SelectedShapeWidget();
			this.SelectedColorWidget = new ShapeEditorAttempt.SelectedColorWidget();
			this.ButtonClear = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
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
			// SelectedShapeWidget
			// 
			this.SelectedShapeWidget.Location = new System.Drawing.Point(13, 13);
			this.SelectedShapeWidget.Name = "SelectedShapeWidget";
			this.SelectedShapeWidget.Value = ShapeEditorAttempt.ShapeType.Square;
			this.SelectedShapeWidget.Size = new System.Drawing.Size(331, 80);
			this.SelectedShapeWidget.TabIndex = 1;
			this.SelectedShapeWidget.TabStop = false;
			this.SelectedShapeWidget.Text = "Selected Shape";
			// 
			// SelectedColorWidget
			// 
			this.SelectedColorWidget.Location = new System.Drawing.Point(350, 13);
			this.SelectedColorWidget.Name = "SelectedColorWidget";
			this.SelectedColorWidget.Size = new System.Drawing.Size(302, 80);
			this.SelectedColorWidget.TabIndex = 1;
			this.SelectedColorWidget.TabStop = false;
			this.SelectedColorWidget.Text = "Selected Color";
			// 
			// ButtonClear
			// 
			this.ButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonClear.Location = new System.Drawing.Point(701, 13);
			this.ButtonClear.Name = "ButtonClear";
			this.ButtonClear.Size = new System.Drawing.Size(87, 23);
			this.ButtonClear.TabIndex = 0;
			this.ButtonClear.Text = "Clear Shapes";
			this.ButtonClear.UseVisualStyleBackColor = true;
			this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 560);
			this.Controls.Add(this.Canvas);
			this.Controls.Add(this.SelectedShapeWidget);
			this.Controls.Add(this.SelectedColorWidget);
			this.Controls.Add(this.ButtonClear);
			this.Name = "MainForm";
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public Canvas Canvas;
		public SelectedShapeWidget SelectedShapeWidget;
		public SelectedColorWidget SelectedColorWidget;
		private Button ButtonClear;
	}
}

