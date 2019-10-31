using System.Collections.Generic;
using uTinyRipper.Classes.AvatarMasks;
using uTinyRipper.Converters;
using uTinyRipper.YAML;
using uTinyRipper;

namespace uTinyRipper.Classes
{
	public sealed class AvatarMask : NamedObject
	{
		public AvatarMask(AssetInfo assetInfo) :
			base(assetInfo)
		{
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			m_mask = reader.ReadUInt32Array();
			m_elements = reader.ReadAssetArray<TransformMaskElement>();
		}

		protected override YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = base.ExportYAMLRoot(container);
			node.Add(MaskName, Mask.ExportYAML(true));
			node.Add(ElementsName, Elements.ExportYAML(container));
			return node;
		}

		public override string ExportExtension => "mask";

		public IReadOnlyList<uint> Mask => m_mask;
		public IReadOnlyList<TransformMaskElement> Elements => m_elements;

		public const string MaskName = "m_Mask";
		public const string ElementsName = "m_Elements";

		private uint[] m_mask;
		private TransformMaskElement[] m_elements;
	}
}
