using System;
using uTinyRipper.Classes.Misc;
using DateTime = System.DateTime;

namespace uTinyRipper.Classes
{
	public struct Meta
	{
		public Meta(UnityGUID guid, AssetImporter importer):
			this(guid, true, importer)
		{
		}

		public Meta(UnityGUID guid, bool hasLicense, AssetImporter importer):
			this(guid, hasLicense, false, importer)
		{
		}

		public Meta(UnityGUID guid, bool hasLicense, bool isFolder, AssetImporter importer)
		{
			if (guid.IsZero)
			{
				throw new ArgumentNullException(nameof(guid));
			}

			GUID = guid;
			IsFolderAsset = isFolder;
			HasLicenseData = hasLicense;
			Importer = importer ?? throw new ArgumentNullException(nameof(importer));
		}

		public static int ToFileFormatVersion(Version version)
		{
#warning TODO:
			return 2;
		}


		public UnityGUID GUID { get; }
		public bool IsFolderAsset { get; }
		public bool HasLicenseData { get; }
		public AssetImporter Importer { get; }

		private long CurrentTick => (DateTime.Now.Ticks - 0x089f7ff5f7b58000) / 10000000;

		public const string FileFormatVersionName = "fileFormatVersion";
		public const string GuidName = "guid";
		public const string FolderAssetName = "folderAsset";
		public const string TimeCreatedName = "timeCreated";
		public const string LicenseTypeName = "licenseType";
	}
}
