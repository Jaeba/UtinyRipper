using System.Collections.Generic;

namespace uTinyRipper.Classes.Shaders
{
	public struct ShaderCompilationInfo : IAssetReadable
	{
		/// <summary>
		/// 5.2.0 adn greater
		/// </summary>
		public static bool HasHasFixedFunctionShaders(Version version) => version.IsGreaterEqual(5, 2);

		public void Read(AssetReader reader)
		{
			m_snippets = new Dictionary<int, ShaderSnippet>();
			m_snippets.Read(reader);
			MeshComponentsFromSnippets = reader.ReadInt32();
			HasSurfaceShaders = reader.ReadBoolean();
			if (HasHasFixedFunctionShaders(reader.Version))
			{
				HasFixedFunctionShaders = reader.ReadBoolean();
			}
		}

		public IReadOnlyDictionary<int, ShaderSnippet> Snippets => m_snippets;
		public int MeshComponentsFromSnippets { get; set; }
		public bool HasSurfaceShaders { get; set; }
		public bool HasFixedFunctionShaders { get; set; }

		public const string SnippetsName = "m_Snippets";
		public const string MeshComponentsFromSnippetsName = "m_MeshComponentsFromSnippets";
		public const string HasSurfaceShadersName = "m_HasSurfaceShaders";
		public const string HasFixedFunctionShadersName = "m_HasFixedFunctionShaders";

		private Dictionary<int, ShaderSnippet> m_snippets;
	}
}
