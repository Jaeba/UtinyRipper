using System.Collections.Generic;
using uTinyRipper.Classes.InputManagers;

namespace uTinyRipper.Classes
{
	public sealed class InputManager : GlobalGameManager
	{
		public InputManager(AssetInfo assetInfo):
			base(assetInfo)
		{
		}

		public static int ToSerializedVersion(Version version)
		{
			// added some new default Axes
			if (version.IsGreaterEqual(4, 6))
			{
				return 2;
			}
			return 1;
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			Axes = reader.ReadAssetArray<InputAxis>();
		}
		
		private IReadOnlyList<InputAxis> GetAxes(Version version)
		{
			if (ToSerializedVersion(version) >= 2)
			{
				return Axes;
			}

			List<InputAxis> axes = new List<InputAxis>(Axes.Length + 3);
			axes.AddRange(Axes);
			axes.Add(new InputAxis("Submit", "return", "joystick button 0"));
			axes.Add(new InputAxis("Submit", "enter", "space"));
			axes.Add(new InputAxis("Cancel", "escape", "joystick button 1"));
			return axes;
		}

		public InputAxis[] Axes { get; set; }

		public const string AxesName = "m_Axes";
	}
}
