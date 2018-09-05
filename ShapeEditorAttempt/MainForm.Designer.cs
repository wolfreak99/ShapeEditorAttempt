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
			this.selectedShapeWidget = new ShapeEditorAttempt.SelectedShapeWidget();
			this.selectedColorWidget = new ShapeEditorAttempt.SelectedColorWidget();
			this.buttonClear = new System.Windows.Forms.Button();
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
			// selectedShapeWidget
			// 
			this.selectedShapeWidget.Location = new System.Drawing.Point(13, 13);
			this.selectedShapeWidget.Name = "selectedShapeWidget";
			this.selectedShapeWidget.Value = ShapeEditorAttempt.ShapeType.Square;
			this.selectedShapeWidget.Size = new System.Drawing.Size(331, 80);
			this.selectedShapeWidget.TabIndex = 1;
			this.selectedShapeWidget.TabStop = false;
			this.selectedShapeWidget.Text = "Selected Shape";
			// 
			// selectedColorWidget
			// 
			this.selectedColorWidget.Location = new System.Drawing.Point(350, 13);
			this.selectedColorWidget.Name = "selectedColorWidget";
			this.selectedColorWidget.Size = new System.Drawing.Size(302, 80);
			this.selectedColorWidget.TabIndex = 1;
			this.selectedColorWidget.TabStop = false;
			this.selectedColorWidget.Text = "Selected Color";
			// 
			// buttonClear
			// 
			this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClear.Location = new System.Drawing.Point(701, 13);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(87, 23);
			this.buttonClear.TabIndex = 0;
			this.buttonClear.Text = "Clear Shapes";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 560);
			this.Controls.Add(this.Canvas);
			this.Controls.Add(this.selectedShapeWidget);
			this.Controls.Add(this.selectedColorWidget);
			this.Controls.Add(this.buttonClear);
			this.Name = "MainForm";
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public Canvas Canvas;
		public SelectedShapeWidget selectedShapeWidget;
		public SelectedColorWidget selectedColorWidget;
		private Button buttonClear;
	}
}

