using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeEditorAttempt
{
	public sealed class ColorsArray
	{
		public class FindColorArrayInfo
		{
			public readonly int PaletteIndex;
			public readonly int ColorIndex;

			protected internal FindColorArrayInfo(int palette, int color)
			{
				PaletteIndex = palette;
				ColorIndex = color;
			}
		}

		public const int COLORS_PER_PALETTE = 15;
		public const int PALLETTE_COUNT = 15;
		public static Color[][] ColorPalettes = new Color[][]
		{
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 204, 0, 0),
				Color.FromArgb(255, 255, 153, 0),
				Color.FromArgb(255, 255, 255, 0),
				Color.FromArgb(255, 0, 153, 0),
				Color.FromArgb(255, 0, 51, 204),
				Color.FromArgb(255, 51, 255, 255),
				Color.FromArgb(255, 0, 0, 153),
				Color.FromArgb(255, 102, 0, 204),
				Color.FromArgb(255, 255, 153, 255),
				Color.FromArgb(255, 255, 204, 204),
				Color.FromArgb(255, 255, 204, 153),
				Color.FromArgb(255, 102, 51, 0),
				Color.FromArgb(255, 153, 102, 0),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 226, 134, 159),
				Color.FromArgb(255, 234, 161, 129),
				Color.FromArgb(255, 253, 223, 99),
				Color.FromArgb(255, 113, 183, 102),
				Color.FromArgb(255, 81, 163, 207),
				Color.FromArgb(255, 34, 164, 161),
				Color.FromArgb(255, 92, 123, 175),
				Color.FromArgb(255, 139, 126, 173),
				Color.FromArgb(255, 246, 217, 225),
				Color.FromArgb(255, 250, 228, 193),
				Color.FromArgb(255, 236, 226, 197),
				Color.FromArgb(255, 255, 250, 177),
				Color.FromArgb(255, 215, 232, 175),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 204, 204, 204),
				Color.FromArgb(255, 153, 153, 153),
				Color.FromArgb(255, 102, 102, 102),
				Color.FromArgb(255, 242, 204, 204),
				Color.FromArgb(255, 191, 153, 153),
				Color.FromArgb(255, 140, 102, 102),
				Color.FromArgb(255, 230, 230, 204),
				Color.FromArgb(255, 179, 179, 153),
				Color.FromArgb(255, 128, 128, 102),
				Color.FromArgb(255, 255, 242, 179),
				Color.FromArgb(255, 204, 191, 128),
				Color.FromArgb(255, 153, 140, 77),
				Color.FromArgb(255, 97, 56, 19),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 85, 0, 0),
				Color.FromArgb(255, 102, 51, 0),
				Color.FromArgb(255, 102, 102, 0),
				Color.FromArgb(255, 0, 34, 0),
				Color.FromArgb(255, 0, 0, 85),
				Color.FromArgb(255, 0, 51, 102),
				Color.FromArgb(255, 0, 0, 34),
				Color.FromArgb(255, 102, 0, 51),
				Color.FromArgb(255, 102, 51, 102),
				Color.FromArgb(255, 187, 187, 187),
				Color.FromArgb(255, 119, 119, 119),
				Color.FromArgb(255, 68, 68, 68),
				Color.FromArgb(255, 17, 17, 17),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 255, 0, 153),
				Color.FromArgb(255, 255, 0, 51),
				Color.FromArgb(255, 255, 102, 0),
				Color.FromArgb(255, 255, 204, 0),
				Color.FromArgb(255, 255, 255, 0),
				Color.FromArgb(255, 0, 255, 0),
				Color.FromArgb(255, 0, 153, 0),
				Color.FromArgb(255, 0, 255, 255),
				Color.FromArgb(255, 0, 153, 255),
				Color.FromArgb(255, 51, 0, 255),
				Color.FromArgb(255, 153, 0, 255),
				Color.FromArgb(255, 255, 0, 255),
				Color.FromArgb(255, 255, 153, 255),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 151, 38, 65),
				Color.FromArgb(255, 225, 140, 71),
				Color.FromArgb(255, 229, 187, 58),
				Color.FromArgb(255, 147, 177, 65),
				Color.FromArgb(255, 98, 126, 51),
				Color.FromArgb(255, 0, 145, 99),
				Color.FromArgb(255, 26, 100, 61),
				Color.FromArgb(255, 55, 107, 137),
				Color.FromArgb(255, 23, 63, 109),
				Color.FromArgb(255, 26, 33, 68),
				Color.FromArgb(255, 197, 196, 119),
				Color.FromArgb(255, 120, 107, 46),
				Color.FromArgb(255, 112, 134, 125),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 204, 0, 0),
				Color.FromArgb(255, 255, 153, 0),
				Color.FromArgb(255, 255, 255, 0),
				Color.FromArgb(255, 0, 153, 0),
				Color.FromArgb(255, 0, 51, 204),
				Color.FromArgb(255, 51, 255, 255),
				Color.FromArgb(255, 0, 0, 153),
				Color.FromArgb(255, 102, 0, 204),
				Color.FromArgb(255, 255, 153, 255),
				Color.FromArgb(255, 255, 204, 204),
				Color.FromArgb(255, 255, 204, 153),
				Color.FromArgb(255, 102, 51, 0),
				Color.FromArgb(255, 153, 102, 0),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 116, 24, 22),
				Color.FromArgb(255, 153, 102, 0),
				Color.FromArgb(255, 178, 152, 84),
				Color.FromArgb(255, 102, 102, 0),
				Color.FromArgb(255, 77, 68, 44),
				Color.FromArgb(255, 102, 102, 102),
				Color.FromArgb(255, 102, 102, 153),
				Color.FromArgb(255, 153, 102, 102),
				Color.FromArgb(255, 204, 102, 51),
				Color.FromArgb(255, 142, 61, 43),
				Color.FromArgb(255, 147, 141, 96),
				Color.FromArgb(255, 142, 115, 58),
				Color.FromArgb(255, 220, 192, 102),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 238, 238, 238),
				Color.FromArgb(255, 220, 220, 220),
				Color.FromArgb(255, 201, 201, 201),
				Color.FromArgb(255, 183, 183, 183),
				Color.FromArgb(255, 164, 164, 164),
				Color.FromArgb(255, 146, 146, 146),
				Color.FromArgb(255, 127, 127, 127),
				Color.FromArgb(255, 109, 109, 109),
				Color.FromArgb(255, 91, 91, 91),
				Color.FromArgb(255, 72, 72, 72),
				Color.FromArgb(255, 54, 54, 54),
				Color.FromArgb(255, 35, 35, 35),
				Color.FromArgb(255, 17, 17, 17),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 249, 223, 207),
				Color.FromArgb(255, 241, 193, 163),
				Color.FromArgb(255, 236, 169, 106),
				Color.FromArgb(255, 167, 110, 91),
				Color.FromArgb(255, 118, 63, 20),
				Color.FromArgb(255, 112, 35, 20),
				Color.FromArgb(255, 68, 68, 68),
				Color.FromArgb(255, 227, 149, 181),
				Color.FromArgb(255, 115, 26, 69),
				Color.FromArgb(255, 153, 0, 0),
				Color.FromArgb(255, 255, 255, 0),
				Color.FromArgb(255, 153, 153, 0),
				Color.FromArgb(255, 0, 102, 204),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 255, 226, 226),
				Color.FromArgb(255, 255, 188, 188),
				Color.FromArgb(255, 255, 151, 151),
				Color.FromArgb(255, 255, 113, 113),
				Color.FromArgb(255, 255, 75, 75),
				Color.FromArgb(255, 255, 38, 38),
				Color.FromArgb(255, 255, 0, 0),
				Color.FromArgb(255, 224, 0, 0),
				Color.FromArgb(255, 193, 0, 0),
				Color.FromArgb(255, 162, 0, 0),
				Color.FromArgb(255, 130, 0, 0),
				Color.FromArgb(255, 99, 0, 0),
				Color.FromArgb(255, 68, 0, 0),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 222, 230, 248),
				Color.FromArgb(255, 185, 200, 241),
				Color.FromArgb(255, 148, 170, 233),
				Color.FromArgb(255, 111, 141, 226),
				Color.FromArgb(255, 74, 111, 219),
				Color.FromArgb(255, 37, 81, 211),
				Color.FromArgb(255, 0, 51, 204),
				Color.FromArgb(255, 0, 43, 181),
				Color.FromArgb(255, 0, 34, 159),
				Color.FromArgb(255, 0, 26, 136),
				Color.FromArgb(255, 0, 17, 113),
				Color.FromArgb(255, 0, 9, 91),
				Color.FromArgb(255, 0, 0, 68),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 208, 236, 208),
				Color.FromArgb(255, 173, 222, 173),
				Color.FromArgb(255, 139, 208, 139),
				Color.FromArgb(255, 104, 195, 104),
				Color.FromArgb(255, 69, 181, 69),
				Color.FromArgb(255, 35, 167, 35),
				Color.FromArgb(255, 0, 153, 0),
				Color.FromArgb(255, 0, 132, 0),
				Color.FromArgb(255, 0, 110, 0),
				Color.FromArgb(255, 0, 89, 0),
				Color.FromArgb(255, 0, 68, 0),
				Color.FromArgb(255, 0, 46, 0),
				Color.FromArgb(255, 0, 25, 0),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 255, 255, 220),
				Color.FromArgb(255, 255, 255, 183),
				Color.FromArgb(255, 255, 255, 147),
				Color.FromArgb(255, 255, 255, 110),
				Color.FromArgb(255, 255, 255, 73),
				Color.FromArgb(255, 255, 255, 37),
				Color.FromArgb(255, 255, 255, 0),
				Color.FromArgb(255, 230, 230, 0),
				Color.FromArgb(255, 204, 204, 0),
				Color.FromArgb(255, 179, 179, 0),
				Color.FromArgb(255, 153, 153, 0),
				Color.FromArgb(255, 128, 128, 0),
				Color.FromArgb(255, 102, 102, 0),
			},
			new Color[]{
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 255, 237, 255),
				Color.FromArgb(255, 255, 206, 247),
				Color.FromArgb(255, 255, 175, 238),
				Color.FromArgb(255, 255, 144, 230),
				Color.FromArgb(255, 255, 113, 221),
				Color.FromArgb(255, 255, 82, 213),
				Color.FromArgb(255, 255, 51, 204),
				Color.FromArgb(255, 226, 43, 174),
				Color.FromArgb(255, 196, 34, 144),
				Color.FromArgb(255, 167, 26, 114),
				Color.FromArgb(255, 137, 17, 83),
				Color.FromArgb(255, 108, 9, 53),
				Color.FromArgb(255, 78, 0, 23),
			},
		};
		public static Color[] FromPalettes()
		{
			List<Color> colors = new List<Color>();
			for (int i = 0; i < ColorPalettes.Length; i++)
			{
				colors.AddRange(ColorPalettes[i]);
			}
			return colors.ToArray();
		}
		public static Color[] FromPalette(int palette_index)
		{
			return ColorPalettes[palette_index];
		}
		public static FindColorArrayInfo FindPaletteContainingColor(Color color)
		{
			for (int i = 0; i < ColorPalettes.Length; i++)
			{
				for (int j = 0; j < COLORS_PER_PALETTE; j++)
				{
					if (ColorPalettes[i][j] == color)
					{
						return new FindColorArrayInfo(i, j);
					}
				}
			}
			return null;
		}
	}
}