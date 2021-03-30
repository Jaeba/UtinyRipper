﻿using uTinyRipper.Layout;

namespace uTinyRipper.Classes
{
	public sealed class DefaultImporter : AssetImporter
	{
		public DefaultImporter(AssetLayout layout) :
			base(layout)
		{
		}

		public DefaultImporter(AssetInfo assetInfo):
			base(assetInfo)
		{
		}

		public override bool IncludesImporter(Version version)
		{
			return version.IsGreaterEqual(4);
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			PostRead(reader);
		}

		public override void Write(AssetWriter writer)
		{
			base.Write(writer);

			PostWrite(writer);
		}

		public override ClassIDType ClassID => ClassIDType.DefaultImporter;

		protected override bool IncludesIDToName => false;
	}
}
