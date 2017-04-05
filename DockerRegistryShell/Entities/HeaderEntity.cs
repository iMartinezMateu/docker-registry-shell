namespace DockerRegistryShell
{
	public class HeaderEntity
	{
		public JwkEntity jwk;
		public string alg;

		internal JwkEntity Jwk
		{
			get
			{
				return jwk;
			}

			set
			{
				jwk = value;
			}
		}

		public string Alg
		{
			get
			{
				return alg;
			}

			set
			{
				alg = value;
			}
		}

		public override string ToString()
		{
			return string.Format("[HeaderEntity: jwk={0}, alg={1}]", jwk, alg);
		}
	}
}