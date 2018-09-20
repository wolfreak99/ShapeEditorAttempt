using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ShapeEditorAttempt
{
	public static class LoadSaveController
	{
		public enum LoadSaveAction
		{
			LoadProject,
			SaveProject,
			ExportToImage
		}

		public static bool IsExportingImage { get; private set; }
		
		public static bool ShowFileDialog(LoadSaveAction action)
		{
			bool result = false;

			FileDialog d;
			Func<string, bool> fileAction;
			switch (action)
			{
			case LoadSaveAction.LoadProject:
				d = SetupFileDialog(new OpenFileDialog(), "Load Project (.shp)", ".shp");
				fileAction = LoadProject;
				break;
			case LoadSaveAction.SaveProject:
				d = SetupFileDialog(new SaveFileDialog(), "Save Project (.shp)", ".shp");
				fileAction = SaveProject;
				break;
			case LoadSaveAction.ExportToImage:
				d = SetupFileDialog(new SaveFileDialog(), "Export to Image (.png,.jpg,.bmp)", ".png");
				fileAction = ExportToImage;
				break;
			default:
				throw new EnumNotImplementedException(action);
			}

			if (d.ShowDialog() == DialogResult.OK)
			{
				result = fileAction(d.FileName);
			}

			d.Dispose();
			d = null;

			return result;
		}

		private static T SetupFileDialog<T>(T dialog, string title, string defaultExt) where T : FileDialog
		{
			dialog.AddExtension = true;
			dialog.DefaultExt = defaultExt;
			dialog.Title = title;

			return dialog;
		}

		private static bool LoadProject(string path)
		{
			Canvas canvas = Canvas.Instance;

			canvas.Clear();

			var serializer = new XmlSerializer(typeof(Shape[]));
			using (var stream = new FileStream(path, FileMode.Open))
			{
				var array = serializer.Deserialize(stream) as Shape[];
				canvas.layer.ImportFromArray(array);
			}

			canvas.Invalidate();

			return true;
		}

		private static bool SaveProject(string path)
		{
			Canvas canvas = Canvas.Instance;

			Shape[] array = canvas.layer.ToArray();

			var serializer = new XmlSerializer(typeof(Shape[]));
			using (var stream = new FileStream(path, FileMode.Create))
			{
				serializer.Serialize(stream, array);
			}

			canvas.Invalidate();
			return true;
		}

		private static bool ExportToImage(string path)
		{
			bool result = true;
			ClickData.Clear();

			Canvas canvas = Canvas.Instance;

			// Hide borders when saving image
			var prevBorder = canvas.BorderStyle;
			canvas.BorderStyle = BorderStyle.None;
			IsExportingImage = true;
			canvas.Invalidate();

			var bounds = canvas.layer.GetAllShapesBoundary();

			ImageFormat imageFormat = Utils.GetImageFormatByExtension(path);
			if (bounds.Width == 0 && bounds.Height == 0)
			{
				MessageBox.Show("Nothing to save. Try placing some shapes before exporting to image.");
				result = false;
			}
			using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
			{
				canvas.DrawToBitmap(bitmap, bounds);
				bitmap.Save(path, imageFormat);
			}

			// Restore border
			canvas.BorderStyle = prevBorder;
			IsExportingImage = false;
			canvas.Invalidate();

			return result;
		}
	}
}
