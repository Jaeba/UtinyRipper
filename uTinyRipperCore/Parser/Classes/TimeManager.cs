namespace uTinyRipper.Classes
{
	public sealed class TimeManager : GlobalGameManager
	{
		public TimeManager(AssetInfo assetInfo):
			base(assetInfo)
		{
		}

		/// <summary>
		/// 3.0.0 and greater
		/// </summary>
		public static bool HasMaximumAllowedTimestep(Version version) => version.IsGreaterEqual(3);
		/// <summary>
		/// 5.5.0 and greater
		/// </summary>
		public static bool HasMaximumParticleTimestep(Version version) => version.IsGreaterEqual(5, 5);

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			FixedTimestep = reader.ReadSingle();
			if (HasMaximumAllowedTimestep(reader.Version))
			{
				MaximumAllowedTimestep = reader.ReadSingle();
			}
			TimeScale = reader.ReadSingle();
			if (HasMaximumParticleTimestep(reader.Version))
			{
				MaximumParticleTimestep = reader.ReadSingle();
			}
		}

		private float GetMaximumAllowedTimestep(Version version)
		{
			return HasMaximumAllowedTimestep(version) ? MaximumAllowedTimestep : 1.0f / 3.0f;
		}
		private float GetMaximumParticleTimestep(Version version)
		{
			return HasMaximumParticleTimestep(version) ? MaximumParticleTimestep : 0.03f;
		}

		public float FixedTimestep { get; set; }
		public float MaximumAllowedTimestep { get; set; }
		public float TimeScale { get; set; }
		public float MaximumParticleTimestep { get; set; }

		public const string FixedTimestepName = "Fixed Timestep";
		public const string MaximumAllowedTimestepName = "Maximum Allowed Timestep";
		public const string TimeScaleName = "m_TimeScale";
		public const string MaximumParticleTimestepName = "Maximum Particle Timestep";
	}
}
