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
				case ClassIDType.GameObject:
					return new GameObject(assetInfo);
				case ClassIDType.Transform:
					return new Transform(assetInfo);
				case ClassIDType.TimeManager:
					return new TimeManager(assetInfo);
				case ClassIDType.InputManager:
					return new InputManager(assetInfo);
				case ClassIDType.Camera:
					return new Camera(assetInfo);
				case ClassIDType.Material:
					return new Material(assetInfo);
				case ClassIDType.GraphicsSettings:
					return new GraphicsSettings(assetInfo);
				case ClassIDType.Skybox:
					return new Skybox(assetInfo);
				case ClassIDType.QualitySettings:
					return new QualitySettings(assetInfo);
				case ClassIDType.Shader:
					return new Shader(assetInfo);
				case ClassIDType.TextAsset:
					return new TextAsset(assetInfo);
				case ClassIDType.TagManager:
					return new TagManager(assetInfo);
				case ClassIDType.GUILayer:
					return new GUILayer(assetInfo);
				case ClassIDType.TextMesh:
					return new TextMesh(assetInfo);
				case ClassIDType.Light:
					return new Light(assetInfo);
				case ClassIDType.MonoBehaviour:
					return new MonoBehaviour(assetInfo);
				case ClassIDType.MonoScript:
					return new MonoScript(assetInfo);
				case ClassIDType.MonoManager:
					return new MonoManager(assetInfo);
				case ClassIDType.FlareLayer:
					return new FlareLayer(assetInfo);
				case ClassIDType.Font:
					return new Font(assetInfo);
				case ClassIDType.GUITexture:
					return new GUITexture(assetInfo);
				case ClassIDType.GUIText:
					return new GUIText(assetInfo);
				case ClassIDType.BuildSettings:
					return new BuildSettings(assetInfo);
				case ClassIDType.AssetBundle:
					return new AssetBundle(assetInfo);
				case ClassIDType.ResourceManager:
					return new ResourceManager(assetInfo);
				case ClassIDType.NetworkManager:
					return new NetworkManager(assetInfo);
				case ClassIDType.PreloadData:
					return new PreloadData(assetInfo);
				case ClassIDType.EditorSettings:
					return new EditorSettings(assetInfo);
				case ClassIDType.OffMeshLink:
					return new OffMeshLink(assetInfo);
				case ClassIDType.ShaderVariantCollection:
					return new ShaderVariantCollection(assetInfo);
				case ClassIDType.SortingGroup:
					return new SortingGroup(assetInfo);
				case ClassIDType.ReflectionProbe:
					return new ReflectionProbe(assetInfo);
				case ClassIDType.CanvasRenderer:
					return new CanvasRenderer(assetInfo);
				case ClassIDType.Canvas:
					return new Canvas(assetInfo);
				case ClassIDType.RectTransform:
					return new RectTransform(assetInfo);
				case ClassIDType.CanvasGroup:
					return new CanvasGroup(assetInfo);
				case ClassIDType.ClusterInputManager:
					return new ClusterInputManager(assetInfo);
				case ClassIDType.UnityConnectSettings:
					return new UnityConnectSettings(assetInfo);

				case ClassIDType.PrefabInstance:
					return new PrefabInstance(assetInfo);
				case ClassIDType.DefaultAsset:
					return new DefaultAsset(assetInfo);
				case ClassIDType.DefaultImporter:
					return new DefaultImporter(assetInfo);
				case ClassIDType.SceneAsset:
					return new SceneAsset(assetInfo);
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
