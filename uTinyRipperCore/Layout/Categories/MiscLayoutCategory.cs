using uTinyRipper.Layout.Misc;

namespace uTinyRipper.Layout
{
	public sealed class MiscLayoutCategory
	{
		public MiscLayoutCategory(LayoutInfo info)
		{
			GUID = new GUIDLayout(info);
		}

		public GUIDLayout GUID { get; }
	}
}
