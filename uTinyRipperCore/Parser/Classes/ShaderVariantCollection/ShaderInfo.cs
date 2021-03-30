namespace uTinyRipper.Classes.ShaderVariantCollections
{
	public struct ShaderInfo : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			Variants = reader.ReadAssetArray<VariantInfo>();
		}

		/// <summary>
		/// It's a HashSet actually
		/// </summary>
		public VariantInfo[] Variants { get; set; }

		public const string VariantsName = "variants";
	}
}
