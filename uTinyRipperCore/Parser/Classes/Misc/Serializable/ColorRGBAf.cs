using System.Globalization;

namespace uTinyRipper.Classes
{
	public struct ColorRGBAf : IAsset
	{
		public ColorRGBAf(float r, float g, float b, float a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public static explicit operator ColorRGBAf(ColorRGBA32 color32)
		{
			ColorRGBAf color = new ColorRGBAf
			{
				R = ((color32.RGBA & 0x000000FF) >> 0) / 255.0f,
				G = ((color32.RGBA & 0x0000FF00) >> 8) / 255.0f,
				B = ((color32.RGBA & 0x00FF0000) >> 16) / 255.0f,
				A = ((color32.RGBA & 0xFF000000) >> 24) / 255.0f
			};
			return color;
		}

		public void Read(AssetReader reader)
		{
			R = reader.ReadSingle();
			G = reader.ReadSingle();
			B = reader.ReadSingle();
			A = reader.ReadSingle();
		}

		public void Read32(AssetReader reader)
		{
			ColorRGBA32 color32 = new ColorRGBA32();
			color32.Read(reader);
			this = (ColorRGBAf)color32;
		}

		public void Write(AssetWriter writer)
		{
			writer.Write(R);
			writer.Write(G);
			writer.Write(B);
			writer.Write(A);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[R:{0:0.00} G:{1:0.00} B:{2:0.00} A:{3:0.00}]", R, G, B, A);
		}

		public static ColorRGBAf Black => new ColorRGBAf(0.0f, 0.0f, 0.0f, 1.0f);
		public static ColorRGBAf White => new ColorRGBAf(1.0f, 1.0f, 1.0f, 1.0f);

		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }
		public float A { get; set; }
	}
}
