using System.Collections.Generic;

namespace uTinyRipper.Classes.Materials
{
	public struct UnityPropertySheet : IAssetReadable, IDependent
	{
		public static int ToSerializedVersion(Version version)
		{
			if (version.IsGreaterEqual(2017, 3))
			{
				return 3;
			}
			// min version is 2
			return 2;
		}

		public void Read(AssetReader reader)
		{
			m_texEnvs = new Dictionary<FastPropertyName, UnityTexEnv>();
			m_floats = new Dictionary<FastPropertyName, float>();
			m_colors = new Dictionary<FastPropertyName, ColorRGBAf>();

			m_texEnvs.Read(reader);
			m_floats.Read(reader);
			m_colors.Read(reader);
		}
		
		public IEnumerable<PPtr<Object>> FetchDependencies(DependencyContext context)
		{
			foreach (PPtr<Object> asset in context.FetchDependencies(TexEnvs.Values, TexEnvsName))
			{
				yield return asset;
			}
		}

		public IReadOnlyDictionary<FastPropertyName, UnityTexEnv> TexEnvs => m_texEnvs;
		public IReadOnlyDictionary<FastPropertyName, float> Floats => m_floats;
		public IReadOnlyDictionary<FastPropertyName, ColorRGBAf> Colors => m_colors;

		public const string TexEnvsName = "m_TexEnvs";
		public const string FloatsName = "m_Floats";
		public const string ColorsName = "m_Colors";

		private const string HDRPostfixName = "_HDR";
		private const string STPostfixName = "_ST";
		private const string TexelSizePostfixName = "_TexelSize";

		private Dictionary<FastPropertyName, UnityTexEnv> m_texEnvs;
		private Dictionary<FastPropertyName, float> m_floats;
		private Dictionary<FastPropertyName, ColorRGBAf> m_colors;
	}
}
