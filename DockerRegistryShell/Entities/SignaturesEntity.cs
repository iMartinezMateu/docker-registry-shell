using Newtonsoft.Json;

namespace DockerRegistryShell
{
	public class SignaturesEntity
	{
		public HeaderEntity header;
		public string signature;
		[JsonProperty("proteced")]
		public string protectedHash;

		internal HeaderEntity Header
		{
			get
			{
				return header;
			}

			set
			{
				header = value;
			}
		}

		public string Signature
		{
			get
			{
				return signature;
			}

			set
			{
				signature = value;
			}
		}

		public string ProtectedHash
		{
			get
			{
				return protectedHash;
			}

			set
			{
				protectedHash = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[SignaturesEntity: header={0}, signature={1}, protectedHash={2}]", header, signature, protectedHash);
		}
	}
}