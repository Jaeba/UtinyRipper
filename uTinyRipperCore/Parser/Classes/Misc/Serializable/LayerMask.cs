using uTinyRipper.Layout;

namespace uTinyRipper.Classes
{
	public struct LayerMask : IAsset
	{
		public void Read(AssetReader reader)
		{
			LayerMaskLayout layout = reader.Layout.Serialized.LayerMask;
			Bits = layout.Is32Bits ? reader.ReadUInt32() : reader.ReadUInt16();
		}

		public void Write(AssetWriter writer)
		{
			LayerMaskLayout layout = writer.Layout.Serialized.LayerMask;
			if (layout.Is32Bits)
			{
				writer.Write(Bits);
			}
			else
			{
				writer.Write((ushort)Bits);
			}
		}

		public uint Bits { get; set; }
	}
}
