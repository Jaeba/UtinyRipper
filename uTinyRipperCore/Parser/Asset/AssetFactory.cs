using System;
using System.Collections.Generic;
using uTinyRipper.Classes;

using Object = uTinyRipper.Classes.Object;

namespace uTinyRipper
{
	public class AssetFactory
	{
		public Object CreateAsset(AssetInfo assetInfo)
		{
			if (m_instantiators.TryGetValue(assetInfo.ClassID, out Func<AssetInfo, Object> instantiator))
			{
				return instantiator(assetInfo);
			}
			return DefaultInstantiator(assetInfo);
		}

		public void OverrideInstantiator(ClassIDType classType, Func<AssetInfo, Object> instantiator)
		{
			if (instantiator == null)
			{
				throw new ArgumentNullException(nameof(instantiator));
			}
			m_instantiators[classType] = instantiator;
		}

		private static Object DefaultInstantiator(AssetInfo assetInfo)
		{
			switch (assetInfo.ClassID)
			{
				case ClassIDType.Material:
					return new Material(assetInfo);
				case ClassIDType.Shader:
					return new Shader(assetInfo);
				case ClassIDType.TextAsset:
					return new TextAsset(assetInfo);
				case ClassIDType.AssetBundle:
					return new AssetBundle(assetInfo);
				case ClassIDType.ResourceManager:
					return new ResourceManager(assetInfo);
				case ClassIDType.PreloadData:
					return new PreloadData(assetInfo);
				case ClassIDType.ShaderVariantCollection:
					return new ShaderVariantCollection(assetInfo);

				case ClassIDType.DefaultAsset:
					return new DefaultAsset(assetInfo);
				case ClassIDType.DefaultImporter:
					return new DefaultImporter(assetInfo);
				case ClassIDType.NativeFormatImporter:
					return new NativeFormatImporter(assetInfo);
				case ClassIDType.MonoImporter:
					return new MonoImporter(assetInfo);
				case ClassIDType.DDSImporter:
					return new DDSImporter(assetInfo);
				case ClassIDType.PVRImporter:
					return new PVRImporter(assetInfo);
				case ClassIDType.ASTCImporter:
					return new ASTCImporter(assetInfo);
				case ClassIDType.KTXImporter:
					return new KTXImporter(assetInfo);
				default:
					return null;
			}
		}

		private readonly Dictionary<ClassIDType, Func<AssetInfo, Object>> m_instantiators = new Dictionary<ClassIDType, Func<AssetInfo, Object>>();
	}
}
