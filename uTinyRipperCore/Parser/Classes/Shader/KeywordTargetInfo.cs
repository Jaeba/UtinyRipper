namespace uTinyRipper.Classes.Shaders
{
	public struct KeywordTargetInfo : IAssetReadable
	{
		public KeywordTargetInfo(string name, int requirements)
		{
			KeywordName = name;
			Requirements = requirements;
		}

		public void Read(AssetReader reader)
		{
			KeywordName = reader.ReadString();
			Requirements = reader.ReadInt32();
		}

		public override string ToString()
		{
			return KeywordName == null ? base.ToString() : $"{KeywordName}:{Requirements}";
		}

		public string KeywordName { get; set; }
		public int Requirements { get; set; }

		public const string KeywordNameName = "keywordName";
		public const string RequirementsName = "requirements";
	}
}
