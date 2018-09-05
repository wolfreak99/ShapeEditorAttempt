using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class Layer
	{
		private List<Shape> shapes;

		public Layer()
		{
			shapes = new List<Shape>();
		}

		public void Add(Shape shape)
		{
			// Insert at top of list.
			shapes.Add(shape);
		}

		/// <summary>
		/// Creates a shape with the data, adds it to the layer, and returns it.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Shape AddNewShape(Point location, Size size, Color color, ShapeType type)
		{
			int x = location.X - (size.Width / 2),
				y = location.Y - (size.Height / 2),
				h = size.Width,
				w = size.Height;

			Shape shape = ShapeTypeHelper.CreateNewShape(
				x, y, w, h,
				color,
				type
			);

			Add(shape);
			return shape;
		}

		public bool Remove(Shape shape)
		{
			return shapes.Remove(shape);
		}

		public void Draw(Canvas sender, PaintEventArgs e)
		{
			foreach (Shape s in shapes) {
				s.Draw(sender, e.Graphics);
			}
		}

		public Shape GetShapeByPoint(GraphicsPath path, Point point)
		{
			for (int i = shapes.Count - 1; i >= 0; i--)
			{
				var s = shapes[i];
				if (s.IsPointOverShape(path, point)) {
					return s;
				}
			}

			return null;
		}

		public void Clear()
		{
			shapes.Clear();
		}

		internal void Replace(Shape shape, Shape newShape)
		{
			var index = shapes.IndexOf(shape);
			shapes.Insert(index, newShape);
			shapes.Remove(shape);
			ClickData.Shape = newShape;
		}
	}
}
