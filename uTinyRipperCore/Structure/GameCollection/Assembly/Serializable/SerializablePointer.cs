using System.Collections.Generic;
using uTinyRipper.Classes;

using Object = uTinyRipper.Classes.Object;

namespace uTinyRipper.Game.Assembly
{
	public sealed class SerializablePointer : IAsset, IDependent
	{
		public void Read(AssetReader reader)
		{
			Pointer.Read(reader);
		}

		public void Write(AssetWriter writer)
		{
			Pointer.Write(writer);
		}

		public IEnumerable<PPtr<Object>> FetchDependencies(DependencyContext context)
		{
			yield return context.FetchDependency(Pointer, string.Empty);
		}

		public PPtr<Object> Pointer;
	}
}
