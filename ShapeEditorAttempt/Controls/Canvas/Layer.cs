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

		public Shape DuplicateShape(Shape shape, Point location)
		{
			// Explicitly create triangles to add angle.
			if (shape.Type == ShapeType.Triangle)
			{
				Triangle oldTriangle = (Triangle)shape;
				Triangle newTriangle = (Triangle)AddNewShape(location, shape.Size, shape.Color, shape.Type, false);
				newTriangle.angle = oldTriangle.angle;

				return newTriangle;
			}
			else
			{
				return AddNewShape(location, shape.Size, shape.Color, shape.Type);
			}
		}

		/// <summary>
		/// Creates a shape with the data, adds it to the layer, and returns it.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Shape AddNewShape(Point location, Size size, Color color, ShapeType type, bool stretchTriangle = true)
		{
			int x = location.X - (size.Width / 2),
				y = location.Y - (size.Height / 2),
				w = size.Width,
				h = size.Height;

			Shape shape = ShapeTypeHelper.CreateNewShape(
				x, y, w, h,
				color,
				type, 
				stretchTriangle
			);

			// Add to list and return
			Add(shape);

			return shape;
		}

		public bool Remove(Shape shape)
		{
			return shapes.Remove(shape);
		}

		internal void MoveShapeUp(Shape shape)
		{
			var index = shapes.IndexOf(shape);
			shapes.Swap(index, index - 1);
		}

		internal void MoveShapeDown(Shape shape)
		{
			var index = shapes.IndexOf(shape);
			shapes.Swap(index, index + 1);
		}

		public void Draw(Canvas sender, PaintEventArgs e)
		{
			foreach (Shape s in shapes)
			{
				s.Draw(sender, e.Graphics);
			}
		}

		public Shape GetShapeByPoint(GraphicsPath path, Point point)
		{
			for (int i = shapes.Count - 1; i >= 0; i--)
			{
				var s = shapes[i];
				if (s.IsPointOverShape(path, point))
				{
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
			// If the shape was selected, select replacement shape once created.
			bool wasShapeCLicked = (ClickData.Shape == shape);

			var index = shapes.IndexOf(shape);
			shapes[index] = newShape;

			if (wasShapeCLicked)
			{
				ClickData.Shape = newShape;
			}
		}
	}
}
