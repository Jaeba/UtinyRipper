namespace uTinyRipper.Classes.Materials
{
	public struct FastPropertyName : IAssetReadable
	{
		/// <summary>
		/// 2017.3 and greater
		/// </summary>
		private static bool IsPlainString(Version version) => version.IsGreaterEqual(2017, 3);


		public void Read(AssetReader reader)
		{
			Value = reader.ReadString();
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			if(Value == null)
			{
				return base.ToString();
			}
			return Value;
		}

		public string Value { get; set; }

		public const string NameName = "name";
	}
}
