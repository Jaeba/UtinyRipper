using uTinyRipper.Classes.Misc;
using uTinyRipper.Project;

namespace uTinyRipper.Classes
{
	public sealed class MetaPtr
	{
		public MetaPtr(long fileID)
		{
			FileID = fileID;
			GUID = default;
			AssetType = default;
		}

		public MetaPtr(long fileID, UnityGUID guid, AssetType assetType)
		{
			FileID = fileID;
			GUID = guid;
			AssetType = assetType;
		}

		public MetaPtr(ClassIDType classID, AssetType assetType) :
			this(ExportCollection.GetMainExportID((uint)classID), UnityGUID.MissingReference, assetType)
		{
		}

		public static MetaPtr NullPtr { get; } = new MetaPtr(0);

		public long FileID { get; }
		public UnityGUID GUID { get; }
		public AssetType AssetType { get; }

		public const string FileIDName = "fileID";
		public const string GuidName = "guid";
		public const string TypeName = "type";
	}
}
