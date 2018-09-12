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
			this.ButtonClear = new System.Windows.Forms.Button();
			this.ButtonLoad = new System.Windows.Forms.Button();
			this.ButtonSave = new System.Windows.Forms.Button();
			this.gridPropertiesGroupBox = new System.Windows.Forms.GroupBox();
			this.gridSizeSetButton = new System.Windows.Forms.Button();
			this.gridSizeTextBox = new System.Windows.Forms.TextBox();
			this.selectedShapeNameTextBox = new System.Windows.Forms.TextBox();
			this.buttonSaveImage = new System.Windows.Forms.Button();
			this.Canvas = new ShapeEditorAttempt.Canvas();
			this.SelectedShapeWidget = new ShapeEditorAttempt.SelectedShapeWidget();
			this.gridPropertiesGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
			this.SuspendLayout();
			// 
			// ButtonClear
			// 
			this.ButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonClear.Location = new System.Drawing.Point(701, 13);
			this.ButtonClear.Name = "ButtonClear";
			this.ButtonClear.Size = new System.Drawing.Size(90, 24);
			this.ButtonClear.TabIndex = 0;
			this.ButtonClear.Text = "Clear Shapes";
			this.ButtonClear.UseVisualStyleBackColor = true;
			this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
			// 
			// ButtonLoad
			// 
			this.ButtonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonLoad.Location = new System.Drawing.Point(701, 37);
			this.ButtonLoad.Name = "ButtonLoad";
			this.ButtonLoad.Size = new System.Drawing.Size(90, 24);
			this.ButtonLoad.TabIndex = 0;
			this.ButtonLoad.Text = "Load Shapes";
			this.ButtonLoad.UseVisualStyleBackColor = true;
			this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
			// 
			// ButtonSave
			// 
			this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSave.Location = new System.Drawing.Point(701, 61);
			this.ButtonSave.Name = "ButtonSave";
			this.ButtonSave.Size = new System.Drawing.Size(90, 24);
			this.ButtonSave.TabIndex = 0;
			this.ButtonSave.Text = "Save Shapes";
			this.ButtonSave.UseVisualStyleBackColor = true;
			this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// gridPropertiesGroupBox
			// 
			this.gridPropertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPropertiesGroupBox.Controls.Add(this.gridSizeSetButton);
			this.gridPropertiesGroupBox.Controls.Add(this.gridSizeTextBox);
			this.gridPropertiesGroupBox.Location = new System.Drawing.Point(596, 13);
			this.gridPropertiesGroupBox.Name = "gridPropertiesGroupBox";
			this.gridPropertiesGroupBox.Size = new System.Drawing.Size(99, 80);
			this.gridPropertiesGroupBox.TabIndex = 2;
			this.gridPropertiesGroupBox.TabStop = false;
			this.gridPropertiesGroupBox.Text = "Grid Properties";
			// 
			// gridSizeSetButton
			// 
			this.gridSizeSetButton.Location = new System.Drawing.Point(7, 47);
			this.gridSizeSetButton.Name = "gridSizeSetButton";
			this.gridSizeSetButton.Size = new System.Drawing.Size(86, 23);
			this.gridSizeSetButton.TabIndex = 1;
			this.gridSizeSetButton.Text = "Set Size";
			this.gridSizeSetButton.UseVisualStyleBackColor = true;
			this.gridSizeSetButton.Click += new System.EventHandler(this.gridSizeSetButton_Click);
			// 
			// gridSizeTextBox
			// 
			this.gridSizeTextBox.Location = new System.Drawing.Point(7, 20);
			this.gridSizeTextBox.Name = "gridSizeTextBox";
			this.gridSizeTextBox.Size = new System.Drawing.Size(86, 20);
			this.gridSizeTextBox.TabIndex = 0;
			this.gridSizeTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gridSizeTextBox_PreviewKeyDown);
			// 
			// selectedShapeNameTextBox
			// 
			this.selectedShapeNameTextBox.Location = new System.Drawing.Point(13, 100);
			this.selectedShapeNameTextBox.Name = "selectedShapeNameTextBox";
			this.selectedShapeNameTextBox.Size = new System.Drawing.Size(238, 20);
			this.selectedShapeNameTextBox.TabIndex = 3;
			this.selectedShapeNameTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.selectedShapeName_PreviewKeyDown);
			// 
			// buttonSaveImage
			// 
			this.buttonSaveImage.Location = new System.Drawing.Point(701, 85);
			this.buttonSaveImage.Name = "buttonSaveImage";
			this.buttonSaveImage.Size = new System.Drawing.Size(90, 23);
			this.buttonSaveImage.TabIndex = 4;
			this.buttonSaveImage.Text = "Save Image";
			this.buttonSaveImage.UseVisualStyleBackColor = true;
			this.buttonSaveImage.Click += new System.EventHandler(this.buttonSaveImage_Click);
			// 
			// Canvas
			// 
			this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Canvas.Cursor = System.Windows.Forms.Cursors.Cross;
			this.Canvas.Location = new System.Drawing.Point(13, 126);
			this.Canvas.Name = "Canvas";
			this.Canvas.Size = new System.Drawing.Size(775, 422);
			this.Canvas.TabIndex = 0;
			this.Canvas.TabStop = false;
			// 
			// SelectedShapeWidget
			// 
			this.SelectedShapeWidget.Location = new System.Drawing.Point(13, 13);
			this.SelectedShapeWidget.Name = "SelectedShapeWidget";
			this.SelectedShapeWidget.Size = new System.Drawing.Size(238, 80);
			this.SelectedShapeWidget.TabIndex = 1;
			this.SelectedShapeWidget.TabStop = false;
			this.SelectedShapeWidget.Text = "Selected Shape";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 560);
			this.Controls.Add(this.buttonSaveImage);
			this.Controls.Add(this.selectedShapeNameTextBox);
			this.Controls.Add(this.gridPropertiesGroupBox);
			this.Controls.Add(this.Canvas);
			this.Controls.Add(this.SelectedShapeWidget);
			this.Controls.Add(this.ButtonClear);
			this.Controls.Add(this.ButtonLoad);
			this.Controls.Add(this.ButtonSave);
			this.Name = "MainForm";
			this.Text = "Shape Designer (Alpha)";
			this.gridPropertiesGroupBox.ResumeLayout(false);
			this.gridPropertiesGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public Canvas Canvas;
		public SelectedShapeWidget SelectedShapeWidget;
		public SelectedColorWidget SelectedColorWidget;
		private Button ButtonClear;
		private Button ButtonLoad;
		private Button ButtonSave;
		private GroupBox gridPropertiesGroupBox;
		private Button gridSizeSetButton;
		public TextBox gridSizeTextBox;
		public TextBox selectedShapeNameTextBox;
		private Button buttonSaveImage;
	}
}

