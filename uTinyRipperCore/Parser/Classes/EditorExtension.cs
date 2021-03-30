using System.Collections.Generic;
using uTinyRipper.Layout;

namespace uTinyRipper.Classes
{
	public abstract class EditorExtension : Object
	{
		protected EditorExtension(AssetLayout layout) :
			base(layout)
		{
		}

		protected EditorExtension(AssetInfo assetInfo):
			base(assetInfo)
		{
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);
		}

		public override void Write(AssetWriter writer)
		{
			base.Write(writer);
		}

		public override IEnumerable<PPtr<Object>> FetchDependencies(DependencyContext context)
		{
			foreach (PPtr<Object> asset in base.FetchDependencies(context))
			{
				yield return asset;
			}
		}

		protected void ReadObject(AssetReader reader)
		{
			base.Read(reader);
		}

		protected void WriteObject(AssetWriter writer)
		{
			base.Write(writer);
		}

		protected IEnumerable<PPtr<Object>> FetchDependenciesObject(DependencyContext context)
		{
			foreach (PPtr<Object> asset in base.FetchDependencies(context))
			{
				yield return asset;
			}
		}


#if UNIVERSAL

#warning TODO: PPtr<EditorExtensionImpl>
		public PPtr<Object> ExtensionPtr;
		public PPtr<EditorExtension> CorrespondingSourceObject;
#else
		private PPtr<Object> ExtensionPtr => default;
		private PPtr<EditorExtension> CorrespondingSourceObject => default;
		private PPtr<Prefab> PrefabAsset => default;
#endif
	}
}
