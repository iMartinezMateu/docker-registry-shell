namespace DockerRegistryShell
{
	public class HistoryEntity
	{
		public string v1Compatibility;

		public string V1Compatibility
		{
			get
			{
				return v1Compatibility;
			}

			set
			{
				v1Compatibility = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[HistoryEntity: v1Compatibility={0}]", v1Compatibility);
		}
	}
}

