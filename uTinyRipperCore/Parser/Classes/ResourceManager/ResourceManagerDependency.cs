using System.Collections.Generic;

namespace uTinyRipper.Classes.ResourceManagers
{
	public struct ResourceManagerDependency : IAssetReadable, IDependent
	{
		public void Read(AssetReader reader)
		{
			Object.Read(reader);
			Dependencies = reader.ReadAssetArray<PPtr<Object>>();
		}

		public IEnumerable<PPtr<Object>> FetchDependencies(DependencyContext context)
		{
			yield return context.FetchDependency(Object, ObjectName);
			foreach (PPtr<Object> asset in context.FetchDependencies(Dependencies, DependenciesName))
			{
				yield return asset;
			}
		}


		public PPtr<Object>[] Dependencies { get; set; }

		public const string ObjectName = "m_Object";
		public const string DependenciesName = "m_Dependencies";

		public PPtr<Object> Object;
	}
}
