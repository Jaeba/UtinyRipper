using System.Collections.Generic;
using uTinyRipper.Classes.Materials;

namespace uTinyRipper.Classes
{
	public sealed class Material : NamedObject
	{
		public Material(AssetInfo assetInfo) :
			base(assetInfo)
		{
		}

		public static int ToSerializedVersion(Version version)
		{
			// TODO:
			return 6;
		}

		/// <summary>
		/// 4.1.0b and greater
		/// </summary>
		public static bool HasKeywords(Version version) => version.IsGreaterEqual(4, 1, 0, VersionType.Beta);
		/// <summary>
		/// Less 5.0.0
		/// </summary>
		public static bool IsKeywordsArray(Version version) => version.IsLess(5);
		/// <summary>
		/// 4.3.0 and greater
		/// </summary>
		public static bool HasCustomRenderQueue(Version version) => version.IsGreaterEqual(4, 3);
		/// <summary>
		/// 5.0.0f1 and greater
		/// </summary>
		public static bool HasLightmapFlags(Version version) => version.IsGreaterEqual(5, 0, 0, VersionType.Final);
		/// <summary>
		/// 5.6.0 and greater
		/// </summary>
		public static bool HasOtherFlags(Version version) => version.IsGreaterEqual(5, 6);
		/// <summary>
		/// 5.1.0 and greater
		/// </summary>
		public static bool HasStringTagMap(Version version) => version.IsGreaterEqual(5, 1);
		/// <summary>
		/// 5.6.0b5 and greater
		/// </summary>
		public static bool HasDisabledShaderPasses(Version version) => version.IsGreaterEqual(5, 6, 0, VersionType.Beta, 5);

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			Shader.Read(reader);
			if (HasKeywords(reader.Version))
			{
				if (IsKeywordsArray(reader.Version))
				{
					ShaderKeywordsArray = reader.ReadStringArray();
				}
				else
				{
					ShaderKeywords = reader.ReadString();
				}
			}

			if (HasLightmapFlags(reader.Version))
			{
				LightmapFlags = reader.ReadUInt32();
				if (HasOtherFlags(reader.Version))
				{
					EnableInstancingVariants = reader.ReadBoolean();
					DoubleSidedGI = reader.ReadBoolean();
					reader.AlignStream();
				}
			}

			if (HasCustomRenderQueue(reader.Version))
			{
				CustomRenderQueue = reader.ReadInt32();
			}

			if (HasStringTagMap(reader.Version))
			{
				StringTagMap = new Dictionary<string, string>();
				StringTagMap.Read(reader);
			}
			if (HasDisabledShaderPasses(reader.Version))
			{
				DisabledShaderPasses = reader.ReadStringArray();
			}

			SavedProperties.Read(reader);
		}

		public override IEnumerable<PPtr<Object>> FetchDependencies(DependencyContext context)
		{
			foreach (PPtr<Object> asset in base.FetchDependencies(context))
			{
				yield return asset;
			}

			yield return context.FetchDependency(Shader, ShaderName);
			foreach (PPtr<Object> asset in context.FetchDependencies(SavedProperties, SavedPropertiesName))
			{
				yield return asset;
			}
		}

		public override string ExportExtension => "mat";

		public string[] ShaderKeywordsArray { get; set; }
		public string ShaderKeywords { get; set; } = string.Empty;
		public int CustomRenderQueue { get; set; }
		public uint LightmapFlags { get; set; }
		public bool EnableInstancingVariants { get; set; }
		public bool DoubleSidedGI { get; set; }
		public string[] DisabledShaderPasses { get; set; }
		public Dictionary<string, string> StringTagMap { get; set; }

		public const string ShaderName = "m_Shader";
		public const string ShaderKeywordsName = "m_ShaderKeywords";
		public const string LightmapFlagsName = "m_LightmapFlags";
		public const string EnableInstancingVariantsName = "m_EnableInstancingVariants";
		public const string DoubleSidedGIName = "m_DoubleSidedGI";
		public const string CustomRenderQueueName = "m_CustomRenderQueue";
		public const string StringTagMapName = "stringTagMap";
		public const string DisabledShaderPassesName = "disabledShaderPasses";
		public const string SavedPropertiesName = "m_SavedProperties";

		public PPtr<Shader> Shader;
		public UnityPropertySheet SavedProperties;
	}
}
