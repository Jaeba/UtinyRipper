using System;
using System.Collections.Generic;
using System.IO;
using uTinyRipper.Classes;
using uTinyRipper.Classes.Misc;
using uTinyRipper.Layout;
using uTinyRipper.Project;
using uTinyRipper.SerializedFiles;

using Object = uTinyRipper.Classes.Object;

namespace uTinyRipper.Converters
{
	public class ProjectAssetContainer : IExportContainer
	{
		public ProjectAssetContainer(ProjectExporter exporter, ExportOptions options, VirtualSerializedFile file, IEnumerable<Object> assets,
			IReadOnlyList<IExportCollection> collections)
		{
			m_exporter = exporter ?? throw new ArgumentNullException(nameof(exporter));
			m_options = options ?? throw new ArgumentNullException(nameof(options));
			VirtualFile = file ?? throw new ArgumentNullException(nameof(file));
			ExportLayout = file.Layout;

			foreach (Object asset in assets)
			{
				switch (asset.ClassID)
				{
					case ClassIDType.ResourceManager:
						AddResources((ResourceManager)asset);
						break;

					case ClassIDType.AssetBundle:
						AddBundleAssets((AssetBundle)asset);
						break;
				}
			}
		}

#warning TODO: get rid of IEnumerable. pass only main asset (issues: prefab, texture with sprites, animatorController)
		public bool TryGetAssetPathFromAssets(IEnumerable<Object> assets, out Object selectedAsset, out string assetPath)
		{
			selectedAsset = null;
			assetPath = string.Empty;
			if (m_pathAssets.Count > 0)
			{
				foreach (Object asset in assets)
				{
					if (m_pathAssets.TryGetValue(asset, out ProjectAssetPath projectPath))
					{
						selectedAsset = asset;
						assetPath = projectPath.SubstituteExportPath(asset);
						return true;
					}
				}
			}

			return false;
		}

		public Object GetAsset(long pathID)
		{
			return File.GetAsset(pathID);
		}

		public Object FindAsset(int fileIndex, long pathID)
		{
			if (fileIndex == VirtualSerializedFile.VirtualFileIndex)
			{
				return VirtualFile.FindAsset(pathID);
			}
			else
			{
				return File.FindAsset(fileIndex, pathID);
			}
		}

		public Object GetAsset(int fileIndex, long pathID)
		{
			if (fileIndex == VirtualSerializedFile.VirtualFileIndex)
			{
				return VirtualFile.GetAsset(pathID);
			}
			else
			{
				return File.GetAsset(fileIndex, pathID);
			}
		}

		public Object FindAsset(ClassIDType classID)
		{
			return File.FindAsset(classID);
		}

		public Object FindAsset(ClassIDType classID, string name)
		{
			return File.FindAsset(classID, name);
		}

		public ClassIDType GetAssetType(long pathID)
		{
			return File.GetAssetType(pathID);
		}

		public long GetExportID(Object asset)
		{
			if (m_assetCollections.TryGetValue(asset.AssetInfo, out IExportCollection collection))
			{
				return collection.GetExportID(asset);
			}

			return ExportCollection.GetMainExportID(asset);
		}

		public AssetType ToExportType(ClassIDType classID)
		{
			return m_exporter.ToExportType(classID);
		}

		public MetaPtr CreateExportPointer(Object asset)
		{
			if (m_assetCollections.TryGetValue(asset.AssetInfo, out IExportCollection collection))
			{
				return collection.CreateExportPointer(asset, collection == CurrentCollection);
			}

			long exportID = ExportCollection.GetMainExportID(asset);
			return new MetaPtr(exportID, UnityGUID.MissingReference, AssetType.Meta);
		}

		private void AddResources(ResourceManager manager)
		{
			foreach (KeyValuePair<string, PPtr<Object>> kvp in manager.Container)
			{
				Object asset = kvp.Value.FindAsset(manager.File);
				if (asset == null)
				{
					continue;
				}

				string resourcePath = kvp.Key;
				if (m_pathAssets.TryGetValue(asset, out ProjectAssetPath projectPath))
				{
					// for paths like "Resources/inner/resources/extra/file" engine creates 2 resource entries
					// "inner/resources/extra/file" and "extra/file"
					if (projectPath.AssetPath.Length >= resourcePath.Length)
					{
						continue;
					}
				}
				m_pathAssets[asset] = new ProjectAssetPath(ResourceFullPath, resourcePath);
			}
		}

		private void AddBundleAssets(AssetBundle bundle)
		{
			string bundleName = AssetBundle.HasAssetBundleName(bundle.File.Version) ? bundle.AssetBundleName : bundle.File.Name;
			string bundleDirectory = bundleName + ObjectUtils.DirectorySeparator;
			string directory = Path.Combine(AssetBundleFullPath, bundleName);
			foreach (KeyValuePair<string, Classes.AssetBundles.AssetInfo> kvp in bundle.Container)
			{
				// skip shared bundle assets, because we need to export them in their bundle directory
				if (kvp.Value.Asset.FileIndex != 0)
				{
					continue;
				}
				Object asset = kvp.Value.Asset.FindAsset(bundle.File);
				if (asset == null)
				{
					continue;
				}

				string assetPath = kvp.Key;
				if (AssetBundle.HasPathExtension(bundle.File.Version))
				{
					// custom names may not has extension
					int extensionIndex = assetPath.LastIndexOf('.');
					if (extensionIndex != -1)
					{
						assetPath = assetPath.Substring(0, extensionIndex);
					}
				}

				if (m_options.KeepAssetBundleContentPath)
				{
					m_pathAssets.Add(asset, new ProjectAssetPath(string.Empty, assetPath));
				}
				else
				{
					if (assetPath.StartsWith(AssetsDirectory, StringComparison.OrdinalIgnoreCase))
					{
						assetPath = assetPath.Substring(AssetsDirectory.Length);
					}
					if (assetPath.StartsWith(bundleDirectory, StringComparison.OrdinalIgnoreCase))
					{
						assetPath = assetPath.Substring(bundleDirectory.Length);
					}
					m_pathAssets.Add(asset, new ProjectAssetPath(directory, assetPath));
				}
			}
#warning TODO: asset bundle may contains more assets than listed in Container. need to export them in AssetBundleFullPath directory if KeepAssetBundleContentPath is false
		}

		public IExportCollection CurrentCollection { get; set; }
		public VirtualSerializedFile VirtualFile { get; }
		public ISerializedFile File => CurrentCollection.File;
		public string Name => File.Name;
		public AssetLayout Layout => File.Layout;
		public Version Version => File.Version;
		public Platform Platform => File.Platform;
		public TransferInstructionFlags Flags => File.Flags;
		public AssetLayout ExportLayout { get; }
		public Version ExportVersion => ExportLayout.Info.Version;
		public Platform ExportPlatform => ExportLayout.Info.Platform;
		public TransferInstructionFlags ExportFlags => ExportLayout.Info.Flags | CurrentCollection.Flags;
		public IReadOnlyList<FileIdentifier> Dependencies => File.Dependencies;

		private const string ResourceKeyword = "Resources";
		private const string AssetBundleKeyword = "AssetBundles";
		private const string AssetsDirectory = Object.AssetsKeyword + ObjectUtils.DirectorySeparator;
		private const string ResourceFullPath = AssetsDirectory + ResourceKeyword;
		private const string AssetBundleFullPath = AssetsDirectory + AssetBundleKeyword;

		private readonly ProjectExporter m_exporter;
		private readonly ExportOptions m_options;
		private readonly Dictionary<AssetInfo, IExportCollection> m_assetCollections = new Dictionary<AssetInfo, IExportCollection>();
		private readonly Dictionary<Object, ProjectAssetPath> m_pathAssets = new Dictionary<Object, ProjectAssetPath>();
	}
}
