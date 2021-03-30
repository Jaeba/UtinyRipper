using System;
using System.Collections.Generic;

namespace uTinyRipper.Layout
{
	public sealed class AssetLayout
	{
		public AssetLayout(LayoutInfo info)
		{
			Info = info;

			IsAlign = info.Version.IsGreaterEqual(2, 1);
			IsAlignArrays = info.Version.IsGreaterEqual(2017);
			IsStructSerializable = info.Version.IsGreaterEqual(4, 5);

			PPtr = new PPtrLayout(info);

			Misc = new MiscLayoutCategory(info);
			Serialized = new SerializedLayoutCategory(info);

			EditorExtension = new EditorExtensionLayout(info);

			NamedObject = new NamedObjectLayout(info);
			Object = new ObjectLayout(info);

			ClassNames = CreateClassNames();
		}

		private Dictionary<ClassIDType, string> CreateClassNames()
		{
			Dictionary<ClassIDType, string> names = new Dictionary<ClassIDType, string>();
			ClassIDType[] classTypes = (ClassIDType[])Enum.GetValues(typeof(ClassIDType));
			foreach (ClassIDType classType in classTypes)
			{
				names[classType] = classType.ToString();
			}
			return names;
		}

		public LayoutInfo Info { get; }

		/// <summary>
		/// 2.1.0 and greater
		/// The alignment concept was first introduced only in v2.1.0
		/// </summary>
		public bool IsAlign { get; }
		/// <summary>
		/// 2017.1 and greater
		/// </summary>
		public bool IsAlignArrays { get; }
		/// <summary>
		/// 4.5.0 and greater
		/// </summary>
		public bool IsStructSerializable { get; }

		public IReadOnlyDictionary<ClassIDType, string> ClassNames { get; }

		public PPtrLayout PPtr { get; }

		public MiscLayoutCategory Misc { get; }
		public SerializedLayoutCategory Serialized { get; }
		public EditorExtensionLayout EditorExtension { get; }
		public NamedObjectLayout NamedObject { get; }
		public ObjectLayout Object { get; }

		public string TypelessdataName => "_typelessdata";
	}
}
